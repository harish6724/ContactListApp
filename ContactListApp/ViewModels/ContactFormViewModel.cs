using System.ComponentModel.DataAnnotations;

namespace ContactListApp.ViewModels;

public class ContactFormViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress, MaxLength(200)]
    public string? Email { get; set; }

    [Phone, MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(20)]
    [Display(Name = "Zip Code")]
    public string? ZipCode { get; set; }

    public string? Notes { get; set; }

    [MaxLength(500)]
    [Display(Name = "Tags (comma-separated)")]
    public string? Tags { get; set; }

    [Display(Name = "Profile Photo")]
    public IFormFile? Photo { get; set; }

    public string? ExistingPhotoPath { get; set; }
}
