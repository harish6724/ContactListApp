# ContactList App

A full-featured contact management web application built with **ASP.NET Core 8 MVC**, **Entity Framework Core**, and **ASP.NET Core Identity**.

## Features

- **User Authentication** — Register and login; each user sees only their own contacts
- **Contact CRUD** — Create, view, edit, and delete contacts
- **Search & Filter** — Search by name, email, phone, or city; filter by tag
- **Profile Photos** — Upload and display avatar photos per contact
- **Tags** — Label contacts with comma-separated tags (e.g. `work, family`)
- **Full Address** — Street, city, state, and zip code fields
- **Notes** — Free-text notes on each contact
- **Responsive UI** — Bootstrap 5 with Bootstrap Icons

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 MVC |
| ORM | Entity Framework Core 8 |
| Database | SQL Server / LocalDB |
| Auth | ASP.NET Core Identity |
| UI | Bootstrap 5, Bootstrap Icons |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server Express (LocalDB works out of the box)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/ContactListApp.git
   cd ContactListApp
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations**
   ```bash
   cd ContactListApp
   dotnet ef database update
   ```

4. **Run the app**
   ```bash
   dotnet run
   ```

5. Open your browser at `https://localhost:5001` (or the port shown in the terminal)

### First-time use

Register a new account, then start adding contacts.

## Project Structure

```
ContactListApp/
├── ContactListApp.sln
├── .gitignore
├── README.md
└── ContactListApp/
    ├── Controllers/
    │   ├── AccountController.cs   # Login, register, logout
    │   └── ContactsController.cs  # CRUD, search, photo upload
    ├── Data/
    │   └── ApplicationDbContext.cs
    ├── Models/
    │   ├── ApplicationUser.cs     # Extended Identity user
    │   └── Contact.cs
    ├── ViewModels/
    │   ├── ContactFormViewModel.cs
    │   ├── LoginViewModel.cs
    │   └── RegisterViewModel.cs
    ├── Views/
    │   ├── Account/               # Login, Register
    │   ├── Contacts/              # Index, Details, Create, Edit, Delete
    │   └── Shared/                # Layout, Error
    ├── wwwroot/
    │   ├── css/site.css
    │   ├── js/site.js
    │   └── uploads/               # Uploaded contact photos
    ├── Program.cs
    ├── appsettings.json
    └── ContactListApp.csproj
```

## Database Migrations

To create the initial migration (first time only):

```bash
cd ContactListApp
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Configuration

The default connection string targets **SQL Server LocalDB**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContactListAppDb;Trusted_Connection=True;"
}
```

To use a full SQL Server instance, update the connection string in `appsettings.json`.
