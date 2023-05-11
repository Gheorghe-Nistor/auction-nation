// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.WebPages;
using Cegeka.Auction.Infrastructure;
using Cegeka.Auction.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cegeka.Auction.WebUI.Server.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly SendGridMailServices _emailSender;

        public ChangePasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            SendGridMailServices emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

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
        [TempData]
        public string StatusMessage { get; set; }

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
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public string GetDeviceType()
        {
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString().ToLower();
            string message, deviceType = "Unknown";

            // Uncomment for testing
            //string userAgent = "mozilla / 5.0(windows nt 10.0; win64; x64) applewebkit / 537.36(khtml, like gecko) chrome / 112.0.0.0 safari / 537.36";                   // Windows
            //string userAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:88.0) Gecko/20100101 Firefox/88.0";                                                            // Linux
            //string userAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";                // Macbook
            //string userAgent = "Mozilla/5.0 (Linux; Android 11; SM-A125U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.120 Mobile Safari/537.36";              // Samsung
            //string userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Mobile/15E148 Safari/604.1"; // iPhone
            //string userAgent = "Mozilla/5.0 (iPad; CPU OS 14_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Mobile/15E148 Safari/604.1";          // iPad

            if (userAgent.Contains("mobile", StringComparison.OrdinalIgnoreCase))
            {
                if (userAgent.Contains("android", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "Android";
                }
                else if (userAgent.Contains("iphone", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "iPhone";
                }
                else if (userAgent.Contains("ipad", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "iPad";
                }
            }
            else
            {
                if (userAgent.Contains("windows", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "Windows";
                }
                else if (userAgent.Contains("linux", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "Linux";
                }
                else if (userAgent.Contains("mac", StringComparison.OrdinalIgnoreCase))
                {
                    deviceType = "Mac";
                }
            }

            message = $"{deviceType} device";

            return message;
        }

        private string CreateMailContent(ApplicationUser user)
        {
            var content = "<p>Dear user,</p>";
            content += $"<p>We are writing to inform you that your password has been changed on <b>{DateTime.Now.ToString("dd'/'MM'/'yyyy' 'HH':'mm':'ss")}</b>. " +
                       $"The password change was made using a <b>{GetDeviceType()}</b>, and we wanted to make you aware of this change.</p>";
            content += "<p> If you did not initiate this password change or believe that your account has been compromised, " +
                       "please contact our support team immediately.</p>";
            content += "<p>Thank you,</p>";
            content += "<p>Auction Nation</p>";

            return content;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _emailSender.SendEmailAsync(user.Email, "Password Change Alert", CreateMailContent(user));

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }
    }
}
