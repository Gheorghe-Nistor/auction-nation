using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.Infrastructure.Identity;
using Cegeka.Auction.WebUI.Shared.Authorization;
using System;
using Cegeka.Auction.Domain.Enums;

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
        //await SeedAuctionsAsync();
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
                Permissions = Permissions.Counter | Permissions.ViewUsers
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

        var presentAuditorUser = await _userManager.FindByEmailAsync(auditorUserName);
        if (presentAuditorUser != null)
        {
            await _userManager.DeleteAsync(presentAuditorUser);
        }

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
            new AuctionItem
            {
                PublicId = new Guid("68d0cbb6-09a6-4c05-a50a-c26d0c0e35b2"),
                Title = "Military Watch from WWII",
                Description = "This vintage military watch from the World War II era is a piece of history housed in a small 30-32mm case. Manufactured by renowned American watch companies Elgin, Waltham, and Bulova, it was made according to a standard set by the U.S. military. Don't miss out on the opportunity to own this unique piece of history!",
                Images = new List<string> {"N/A"},
                Category = Category.CollectiblesAndMemorabilia,
                StartingBidAmount = 500,
                StartDate = new DateTime(2023, 4, 1),
                EndDate = new DateTime(2023, 5, 1),
                BuyItNowPrice = 1000,
                ReservePrice = 800,
                DeliveryMethod = DeliveryMethod.DeliveryByCourier,
                Status = Status.New
            },
            new AuctionItem
            {
                PublicId = new Guid("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f"),
                Title = "WWII Era Webley MK IV Revolver",
                Description = "This blued finish revolver is a six-shot fluted cylinder with fixed blade front and fixed rear notch sights. The frame is marked \"War Finish\" designating British acceptance during WWII. Don't miss the chance to own this historic piece of weaponry from one of the most tumultuous times in world history.",
                Images = new List<string> {"N/A"},
                Category = Category.CollectiblesAndMemorabilia,
                StartingBidAmount = 1000,
                StartDate = new DateTime(2023, 5, 1),
                EndDate = new DateTime(2023, 6, 1),
                BuyItNowPrice = 2000,
                ReservePrice = 1800,
                Status = Status.Approved,
                DeliveryMethod = DeliveryMethod.DeliveryByCourier
            },
            new AuctionItem
            {
                PublicId = new Guid("13134658-c68e-4ad2-b40b-9dfc95f76ae7"),
                Title = "Rare 1967 Ford Mustang Fastback",
                Description = "This rare 1967 Ford Mustang Fastback is a true beauty. With a sleek black exterior and a powerful V8 engine, this classic car is sure to turn heads wherever you go. The interior is in excellent condition and features classic Mustang styling. Don't miss the chance to own this piece of automotive history.",
                Images = new List<string> {"N/A"},
                Category = Category.Vehicles,
                StartingBidAmount = 50000,
                StartDate = new DateTime(2023, 5, 15),
                EndDate = new DateTime(2023, 6, 15),
                BuyItNowPrice = 700000,
                ReservePrice = 65000,
                Status = Status.Submitted,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            },
            new AuctionItem
            {
                PublicId = new Guid("26aa77e2-ebd2-417c-9cb8-7cfe695548ab"),
                Title = "Antique Persian Rug",
                Description = "This beautiful antique Persian rug is a hand-knotted wool masterpiece. It features intricate floral and geometric designs in rich, warm colors. The craftsmanship and attention to detail are evident in every knot. Don't miss the opportunity to own this stunning piece of art.",
                Images = new List<string> {"N/A"},
                Category = Category.HomeAndGardening,
                StartingBidAmount = 2000,
                StartDate = new DateTime(2023, 4, 1),
                EndDate = new DateTime(2023, 6, 1),
                BuyItNowPrice = 4000,
                ReservePrice = 3000,
                Status = Status.New,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            },

            new AuctionItem
            {
                PublicId = new Guid("b6fda651-b1c9-44b8-b7b2-3423d6e83c6f"),
                Title = "Vintage Gibson Les Paul Electric Guitar",
                Description = "This vintage Gibson Les Paul electric guitar is a true classic. Made in the USA in the 1970s, it has a beautiful cherry sunburst finish and features dual humbucking pickups. It has been well-maintained and is in excellent playing condition. Don't miss the chance to own this iconic instrument.",
                Images = new List<string> {"N/A"},
                Category = Category.Music,
                StartingBidAmount = 5000,
                StartDate = new DateTime(2023, 5, 25),
                EndDate = new DateTime(2023, 6, 25),
                BuyItNowPrice = 10000,
                ReservePrice = 8000,
                Status = Status.Submitted,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            }
        };

        _context.AuctionItems.AddRange(auctions);
        await _context.SaveChangesAsync();
    }
}
