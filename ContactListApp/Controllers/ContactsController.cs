using ContactListApp.Data;
using ContactListApp.Models;
using ContactListApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListApp.Controllers;

[Authorize]
public class ContactsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;

    public ContactsController(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        IWebHostEnvironment env)
    {
        _db = db;
        _userManager = userManager;
        _env = env;
    }

    private string CurrentUserId => _userManager.GetUserId(User)!;

    private IQueryable<Contact> UserContacts =>
        _db.Contacts.Where(c => c.UserId == CurrentUserId);

    public async Task<IActionResult> Index(string? search, string? tag)
    {
        var query = UserContacts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(term) ||
                c.LastName.ToLower().Contains(term) ||
                (c.Email != null && c.Email.ToLower().Contains(term)) ||
                (c.Phone != null && c.Phone.Contains(term)) ||
                (c.City != null && c.City.ToLower().Contains(term)));
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            var t = tag.Trim().ToLower();
            query = query.Where(c => c.Tags != null && c.Tags.ToLower().Contains(t));
        }

        ViewBag.Search = search;
        ViewBag.Tag = tag;
        ViewBag.TotalCount = await UserContacts.CountAsync();

        var contacts = await query
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();

        return View(contacts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var contact = await UserContacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return NotFound();
        return View(contact);
    }

    [HttpGet]
    public IActionResult Create() => View(new ContactFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var contact = new Contact
        {
            UserId = CurrentUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        MapFromViewModel(model, contact);

        if (model.Photo != null)
            contact.PhotoPath = await SavePhotoAsync(model.Photo);

        _db.Contacts.Add(contact);
        await _db.SaveChangesAsync();

        TempData["Success"] = $"{contact.FullName} was added to your contacts.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var contact = await UserContacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return NotFound();
        return View(MapToViewModel(contact));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContactFormViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var contact = await UserContacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return NotFound();

        MapFromViewModel(model, contact);
        contact.UpdatedAt = DateTime.UtcNow;

        if (model.Photo != null)
        {
            DeletePhoto(contact.PhotoPath);
            contact.PhotoPath = await SavePhotoAsync(model.Photo);
        }

        await _db.SaveChangesAsync();

        TempData["Success"] = $"{contact.FullName} was updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var contact = await UserContacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return NotFound();
        return View(contact);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contact = await UserContacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return NotFound();

        var name = contact.FullName;
        DeletePhoto(contact.PhotoPath);
        _db.Contacts.Remove(contact);
        await _db.SaveChangesAsync();

        TempData["Success"] = $"{name} was deleted.";
        return RedirectToAction(nameof(Index));
    }

    private static void MapFromViewModel(ContactFormViewModel vm, Contact contact)
    {
        contact.FirstName = vm.FirstName;
        contact.LastName = vm.LastName;
        contact.Email = vm.Email;
        contact.Phone = vm.Phone;
        contact.Address = vm.Address;
        contact.City = vm.City;
        contact.State = vm.State;
        contact.ZipCode = vm.ZipCode;
        contact.Notes = vm.Notes;
        contact.Tags = vm.Tags;
    }

    private static ContactFormViewModel MapToViewModel(Contact c) => new()
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        Email = c.Email,
        Phone = c.Phone,
        Address = c.Address,
        City = c.City,
        State = c.State,
        ZipCode = c.ZipCode,
        Notes = c.Notes,
        Tags = c.Tags,
        ExistingPhotoPath = c.PhotoPath
    };

    private async Task<string> SavePhotoAsync(IFormFile photo)
    {
        var uploads = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploads);

        var ext = Path.GetExtension(photo.FileName).ToLowerInvariant();
        var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        if (!allowed.Contains(ext)) ext = ".jpg";

        var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(uploads, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await photo.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }

    private void DeletePhoto(string? photoPath)
    {
        if (string.IsNullOrEmpty(photoPath)) return;
        var fullPath = Path.Combine(_env.WebRootPath, photoPath.TrimStart('/'));
        if (System.IO.File.Exists(fullPath))
            System.IO.File.Delete(fullPath);
    }
}
