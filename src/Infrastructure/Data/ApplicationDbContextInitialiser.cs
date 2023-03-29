using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.Infrastructure.Identity;
using Cegeka.Auction.WebUI.Shared.Authorization;

namespace Cegeka.Auction.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private const string AdministratorsRole = "Administrators";
    private const string AccountsRole = "Accounts";
    private const string OperationsRole = "Operations";

    private const string DefaultPassword = "Password123!";

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        await InitialiseWithMigrationsAsync();
    }

    private async Task InitialiseWithDropCreateAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    private async Task InitialiseWithMigrationsAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    public async Task SeedAsync()
    {
        await SeedIdentityAsync();
        await SeedTodosAsync();
        await SeedAuctionsAsync();
    }

    private async Task SeedIdentityAsync()
    {
        // Create roles
        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AdministratorsRole,
                NormalizedName = AdministratorsRole.ToUpper(),
                Permissions = Permissions.All
            });

        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AccountsRole,
                NormalizedName = AccountsRole.ToUpper(),
                Permissions =
                    Permissions.ViewUsers |
                    Permissions.Counter
            });

        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = OperationsRole,
                NormalizedName = OperationsRole.ToUpper(),
                Permissions =
                    Permissions.ViewUsers |
                    Permissions.Forecast
            });

        // Ensure admin role has all permissions
        var adminRole = await _roleManager.FindByNameAsync(AdministratorsRole);
        adminRole!.Permissions = Permissions.All;
        await _roleManager.UpdateAsync(adminRole);

        // Create default admin user
        var adminUserName = "admin@localhost";
        var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };

        await _userManager.CreateAsync(adminUser, DefaultPassword);

        adminUser = await _userManager.FindByNameAsync(adminUserName);

        await _userManager.AddToRoleAsync(adminUser!, AdministratorsRole);

        // Create default auditor user
        var auditorUserName = "auditor@localhost";
        var auditorUser = new ApplicationUser { UserName = auditorUserName, Email = auditorUserName };
        await _userManager.CreateAsync(auditorUser, DefaultPassword);

        await _context.SaveChangesAsync();
    }

    private async Task SeedTodosAsync()
    {
        if (await _context.TodoLists.AnyAsync())
        {
            return;
        }

        var list = new TodoList
        {
            Title = "✨ Todo List",
            Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
        };

        _context.TodoLists.Add(list);
        await _context.SaveChangesAsync();
    }
    private async Task SeedAuctionsAsync()
    {
        if (await _context.AuctionItems.AnyAsync())
        {
            return;
        }

        List<AuctionItem> auctions = new List<AuctionItem>()
        {
            new AuctionItem {
                PublicId = new Guid("68d0cbb6-09a6-4c05-a50a-c26d0c0e35b2"),
                Title = "Military Watch from WWW II", 
                Description =  "Housed in a positively diminutive (by today's standards, anyway) 30-32mm case, the A-11 was manufactured by famed American watch companies Elgin, Waltham and Bulova according to a standard from the U.S. military.", 
                StartingBidAmount = 5000
            },
            new AuctionItem {
                PublicId = new Guid("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f"),
                Title = "WWII ERA WEBLEY MK IV REVOLVER.", 
                Description = "Blued finish. 6 shot fluted cylinder. Fixed, blade front fixed rear notch sights. Frame marked \"War Finish\" designating British acceptance during WWII.",
                StartingBidAmount = 10000
            }
        };

        _context.AuctionItems.AddRange(auctions);
        await _context.SaveChangesAsync();
    }
}
