using ContactListApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactListApp.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (await db.Contacts.AnyAsync()) return;

        var user = await userManager.FindByEmailAsync("demo@contactlist.com");
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = "demo@contactlist.com",
                Email = "demo@contactlist.com",
                FirstName = "Demo",
                LastName = "User",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Demo1234!");
        }

        var contacts = new List<Contact>
        {
            new() { FirstName = "James",    LastName = "Anderson",  Email = "james.anderson@email.com",   Phone = "314-555-0101", City = "St. Louis",      State = "MO", Address = "123 Oak St",        ZipCode = "63101", Tags = "work, colleague",   Notes = "Met at the 2024 conference." },
            new() { FirstName = "Maria",    LastName = "Garcia",    Email = "maria.garcia@gmail.com",     Phone = "314-555-0102", City = "Clayton",        State = "MO", Address = "456 Elm Ave",       ZipCode = "63105", Tags = "friend, neighbor",  Notes = "Neighbors since 2020." },
            new() { FirstName = "Robert",   LastName = "Johnson",   Email = "r.johnson@company.com",      Phone = "618-555-0103", City = "O'Fallon",       State = "IL", Address = "789 Maple Dr",      ZipCode = "62269", Tags = "work",              Notes = "Project lead on the Apex project." },
            new() { FirstName = "Linda",    LastName = "Martinez",  Email = "linda.m@outlook.com",        Phone = "314-555-0104", City = "Chesterfield",   State = "MO", Address = "321 Pine Rd",       ZipCode = "63017", Tags = "family",            Notes = "Cousin. Birthday: March 12." },
            new() { FirstName = "William",  LastName = "Brown",     Email = "will.brown@webmail.com",     Phone = "636-555-0105", City = "Ballwin",        State = "MO", Address = "654 Cedar Ln",      ZipCode = "63011", Tags = "friend",            Notes = "College roommate." },
            new() { FirstName = "Patricia", LastName = "Davis",     Email = "pat.davis@email.com",        Phone = "314-555-0106", City = "University City",State = "MO", Address = "987 Birch Blvd",    ZipCode = "63130", Tags = "work, manager",     Notes = "Direct manager. Prefers Slack over email." },
            new() { FirstName = "Michael",  LastName = "Wilson",    Email = "mike.wilson@techcorp.com",   Phone = "618-555-0107", City = "Belleville",     State = "IL", Address = "147 Walnut Way",    ZipCode = "62220", Tags = "work, IT",          Notes = "Handles all IT infrastructure." },
            new() { FirstName = "Barbara",  LastName = "Moore",     Email = "barbara.moore@mail.com",     Phone = "314-555-0108", City = "Webster Groves", State = "MO", Address = "258 Ash Court",     ZipCode = "63119", Tags = "friend, neighbor",  Notes = "Book club member." },
            new() { FirstName = "David",    LastName = "Taylor",    Email = "d.taylor@workemail.com",     Phone = "636-555-0109", City = "Wildwood",       State = "MO", Address = "369 Spruce Place",  ZipCode = "63040", Tags = "work",              Notes = "Sales rep. Very responsive." },
            new() { FirstName = "Susan",    LastName = "Thomas",    Email = "sue.thomas@personal.com",    Phone = "314-555-0110", City = "Kirkwood",       State = "MO", Address = "741 Poplar St",     ZipCode = "63122", Tags = "family, sister",    Notes = "Sister. Calls every Sunday." },
            new() { FirstName = "Richard",  LastName = "Jackson",   Email = "rich.jackson@biz.com",       Phone = "618-555-0111", City = "Collinsville",   State = "IL", Address = "852 Willow Ave",    ZipCode = "62234", Tags = "business, client",  Notes = "Key client since 2022." },
            new() { FirstName = "Jessica",  LastName = "White",     Email = "jess.white@email.com",       Phone = "314-555-0112", City = "Maplewood",      State = "MO", Address = "963 Hickory Dr",    ZipCode = "63143", Tags = "friend",            Notes = "Gym buddy. Meets Tuesdays." },
            new() { FirstName = "Thomas",   LastName = "Harris",    Email = "t.harris@consultco.com",     Phone = "636-555-0113", City = "Ladue",          State = "MO", Address = "159 Magnolia Ln",   ZipCode = "63124", Tags = "work, consultant",  Notes = "External consultant, contract ends Dec 2026." },
            new() { FirstName = "Sarah",    LastName = "Martin",    Email = "sarah.martin@gmail.com",     Phone = "314-555-0114", City = "Ferguson",       State = "MO", Address = "357 Sycamore Rd",   ZipCode = "63135", Tags = "friend, college",   Notes = "Friend from Wash U." },
            new() { FirstName = "Charles",  LastName = "Thompson",  Email = "charlie.t@email.net",        Phone = "618-555-0115", City = "Edwardsville",   State = "IL", Address = "468 Chestnut St",   ZipCode = "62025", Tags = "neighbor",          Notes = "HOA president." },
            new() { FirstName = "Karen",    LastName = "Garcia",    Email = "karen.g@workhub.com",        Phone = "314-555-0116", City = "Creve Coeur",    State = "MO", Address = "579 Hawthorn Blvd", ZipCode = "63141", Tags = "work, HR",          Notes = "HR contact for benefits questions." },
            new() { FirstName = "Daniel",   LastName = "Rodriguez", Email = "dan.rodriguez@mail.com",     Phone = "636-555-0117", City = "Manchester",     State = "MO", Address = "681 Dogwood Dr",    ZipCode = "63011", Tags = "friend, sports",    Notes = "Plays on the same softball team." },
            new() { FirstName = "Nancy",    LastName = "Lewis",     Email = "nancy.lewis@outlook.com",    Phone = "314-555-0118", City = "Florissant",     State = "MO", Address = "792 Redbud Ct",     ZipCode = "63031", Tags = "family",            Notes = "Aunt Nancy. Sends birthday cards every year." },
            new() { FirstName = "Paul",     LastName = "Lee",       Email = "paul.lee@devshop.io",        Phone = "618-555-0119", City = "Swansea",        State = "IL", Address = "813 Locust Way",    ZipCode = "62226", Tags = "work, developer",   Notes = "Senior dev on the mobile team." },
            new() { FirstName = "Emily",    LastName = "Walker",    Email = "emily.walker@personal.net",  Phone = "314-555-0120", City = "Brentwood",      State = "MO", Address = "924 Pecan Ave",     ZipCode = "63144", Tags = "friend, yoga",      Notes = "Yoga instructor. Referred by Maria." },
        };

        foreach (var c in contacts)
        {
            c.UserId = user.Id;
            c.CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 365));
            c.UpdatedAt = DateTime.UtcNow;
        }

        db.Contacts.AddRange(contacts);
        await db.SaveChangesAsync();
    }
}
