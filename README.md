# MVC.NetMinyaITISummer2026AugustD06

# 🔷 ASP.NET Core MVC – Identity & Role Management (.NET 9)

## 📌 Project Overview

**MVCDemoD06** is an ASP.NET Core MVC application implementing **Identity Authentication and Role-Based Authorization**.  

The project demonstrates:

- ASP.NET Core Identity
- Custom `ApplicationUser` & `ApplicationRole`
- Role-based access control
- Claims-based access
- User registration & login
- Dependency Injection with Identity
- SQL Server with Entity Framework Core
- Razor Pages & MVC Controllers

---

# 🏗 Architecture Overview

```
MVCDemoD06
│
├── Context              → AppDbContext (IdentityDbContext)
├── Models               → ApplicationUser, ApplicationRole
├── ViewModels           → Auth (Login/Register), Role
├── Controllers          → Account, Role, TestClaims, TestRoles
└── Program.cs           → Application startup
```

---

# 🧩 Key Components

## 1️⃣ Identity Models

### ApplicationUser

- Inherits `IdentityUser`
- Additional properties:
  - `FirstName`
  - `LastName`

### ApplicationRole

- Inherits `IdentityRole`
- Additional properties:
  - `Description`

### SystemRoles (Static Class)

```csharp
public static class SystemRoles
{
    public const string User = "User";
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
}
```

---

## 2️⃣ DbContext – `AppDbContext`

- Inherits `IdentityDbContext<ApplicationUser, ApplicationRole, string>`
- Manages Users, Roles, Claims, Logins, and Tokens
- Configured with SQL Server via `DbContextOptions`
- `OnModelCreating()` calls `base.OnModelCreating()` to preserve Identity behavior

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCDay06")));
```

---

## 3️⃣ Authentication & Identity Configuration

Configured in `Program.cs`:

```csharp
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>();
```

- Password requirements simplified for demo
- Email uniqueness enforced
- Identity stores configured with `AppDbContext`

---

## 4️⃣ Controllers

### AccountController

Handles:

- **Register**
- **Login**
- **Logout**
- Assigns default role (`SystemRoles.Admin`) on registration
- Uses `UserManager<ApplicationUser>` and `SignInManager<ApplicationUser>`

### RoleController

Handles:

- Role creation
- Uses `RoleManager<ApplicationRole>`

### TestRolesController

Demonstrates role-based authorization:

```csharp
[Authorize(Roles = SystemRoles.Admin)]
public IActionResult IndexV03() { ... }
```

- Supports multiple roles: `[Authorize(Roles = "Admin,User")]`

### TestClaimsController

Demonstrates reading claims:

```csharp
var name = User.Identity.Name;
var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
```

---

## 5️⃣ ViewModels

### Auth ViewModels

- `RegisterVM`
- `LoginVM`

### Role ViewModel

- `CreateRoleVM`

All include data annotations for validation:

```csharp
[Required]
[EmailAddress]
public string Email { get; set; }
```

---

# 🔑 Features

- User Registration & Login
- Role creation & management
- Role-based authorization for controllers/actions
- Claims-based authorization demo
- Assign default role on user registration
- Secure password storage using Identity
- Middleware authentication & authorization pipeline
- Support for multiple roles per user

---

# 🚀 How To Run

### 1️⃣ Configure Connection String

In `appsettings.json`:

```json
"ConnectionStrings": {
  "SDMVCDay10": "Server=.;Database=MVCDemoD06;Trusted_Connection=True;"
}
```

---

### 2️⃣ Apply Migrations

```powershell
Add-Migration InitialCreate
Update-Database
```

---

### 3️⃣ Run Application

```bash
dotnet run
```

Navigate to `https://localhost:{PORT}`.

---

# 🧠 Authorization Rules

### Role-based:

```csharp
[Authorize(Roles = SystemRoles.Admin)]
```

### Claims-based:

```csharp
var claims = User.Claims.ToList();
```

---

# 🔧 Security Configurations

- Unique emails enforced
- Password requirements simplified for demo
- Email confirmation disabled for demo
- Roles assigned programmatically

---

# 📈 Future Improvements

- Email confirmation on registration
- Password complexity enforcement
- Multi-factor authentication
- Reset Password functionality
- Admin panel for role assignment
- User profile management
- Audit logging for user actions
- Unit testing for controllers & services

---

# 👨‍💻 Author

Mohamed Hatem  
Software Engineer

---