// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.Infrastructure.Identity;
using Cegeka.Auction.WebUI.Shared.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cegeka.Auction.WebUI.Server.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "Language")]
            public int? LanguageId { get; set; }

            [Display(Name = "Currency")]
            public int? CurrencyId { get; set; }

            [Display(Name = "TimeZone")]
            public int? TimeZoneId { get; set; }

            [Display(Name = "DisplaySetting")]
            public int? DisplaySettingId { get; set; }

            public static IEnumerable<SelectListItem> Languages { get; set; }

            public static IEnumerable<SelectListItem> Currencies { get; set; }

            public static IEnumerable<SelectListItem> TimeZones { get; set; }

            public static IEnumerable<SelectListItem> DisplaySettings { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var availableLanguages = Enum.GetValues(typeof(Languages))
                                                .Cast<Languages>()
                                                .Select(p => new LookupDto
                                                {
                                                    Id = (int) p,
                                                    Title = p.ToString()
                                                })
                                                .ToList();

            var availableCurrencies = Enum.GetValues(typeof(Currencies))
                                                .Cast<Currencies>()
                                                .Select(p => new LookupDto
                                                {
                                                    Id = (int)p,
                                                    Title = p.ToString()
                                                })
                                                .ToList();

            var availableTimeZones = Enum.GetValues(typeof(TimeZones))
                                                .Cast<TimeZones>()
                                                .Select(p => new LookupDto
                                                {
                                                    Id = (int)p,
                                                    Title = p.ToString()
                                                })
                                                .ToList();

            var availableDisplaySettings = Enum.GetValues(typeof(DisplaySettings))
                                                .Cast<DisplaySettings>()
                                                .Select(p => new LookupDto
                                                {
                                                    Id = (int)p,
                                                    Title = p.ToString()
                                                })
                                                .ToList();

            InputModel.Languages = availableLanguages.Select(x => new SelectListItem() { Text = x.Title , Value = x.Id.ToString() }).ToList();
            InputModel.Currencies = availableCurrencies.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            InputModel.TimeZones = availableTimeZones.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            InputModel.DisplaySettings = availableDisplaySettings.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();

            Input = new InputModel
            {
                Username = user.UserName,
                LanguageId = user.LanguageId ?? -1,
                CurrencyId = user.CurrencyId ?? -1,
                TimeZoneId = user.TimeZoneId ?? -1,
                DisplaySettingId = user.DisplaySettingId ?? -1,
                
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Username != user.UserName) 
            { 
                user.UserName = Input.Username;
            }

            if (Input.LanguageId != user.LanguageId)
            {
                user.LanguageId = Input.LanguageId;
            }

            if (Input.CurrencyId != user.CurrencyId)
            {
                user.CurrencyId = Input.CurrencyId;
            }

            if (Input.TimeZoneId != user.TimeZoneId)
            {
                user.TimeZoneId = Input.TimeZoneId;
            }

            if (Input.DisplaySettingId != user.DisplaySettingId)
            {
                user.DisplaySettingId = Input.DisplaySettingId;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
