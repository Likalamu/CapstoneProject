using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CapstoneTranslator.Areas.Identity.Pages.Account
//{
//    [AllowAnonymous]
//    public class RegisterModel : PageModel
//    {
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly ILogger<RegisterModel> _logger;
//        private readonly IEmailSender _emailSender;
//        RoleManager<IdentityRole> _roleManager;


//        public RegisterModel(
//            UserManager<IdentityUser> userManager,
//            SignInManager<IdentityUser> signInManager,
//            ILogger<RegisterModel> logger,
//            IEmailSender emailSender,
//            RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _logger = logger;
//            _emailSender = emailSender;
//            _roleManager = roleManager;
//        }

//        [BindProperty]
//        public InputModel Input { get; set; }

//        public string ReturnUrl { get; set; }

//        public IList<AuthenticationScheme> ExternalLogins { get; set; }

//        public class InputModel
//        {
//            public string Name { get; set; }

//            [Required(ErrorMessage = "Please Enter First Name..")]
//            [Display(Name = "FirstName")]
//            public string Firstname { get; set; }

//            [Required(ErrorMessage = "Please Enter Last Name..")]
//            [Display(Name = "LastName")]
//            public string Lastname { get; set; }

//            [Required]
//            [EmailAddress]
//            [Display(Name = "Email")]
//            public string Email { get; set; }

//            [Required]
//            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
//            [DataType(DataType.Password)]
//            [Display(Name = "Password")]
//            public string Password { get; set; }

//            [DataType(DataType.Password)]
//            [Display(Name = "Confirm password")]
//            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
//            public string ConfirmPassword { get; set; }

//            [Required(ErrorMessage = "Please Enter Phone Number...")]
//            [Display(Name = "Phone")]
//            public string Phone { get; set; }

//            [Required]
//            [Display(Name = "Please enter your location")]
//            public string Location { get; set; }

//            [Required(ErrorMessage = "Please Enter Preferred Contact Method...")]
//            [Display(Name = "PreferredContact")]
//            public string PreferredContact { get; set; }
//        }

//public void OnGet(string returnUrl = null)
//{
//    // pass the Role List using ViewData
//    ViewData["roles"] = _roleManager.Roles.ToList();
//    ReturnUrl = returnUrl;
//}

//public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//{
//returnUrl = returnUrl ?? Url.Content("~/");
// search role
//var role = _roleManager.FindByIdAsync(Input.Email).Result;
//if (ModelState.IsValid)
//{
//    var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
//    var result = await _userManager.CreateAsync(user, Input.Password);
//    if (result.Succeeded)
//    {
//        _logger.LogInformation("User created a new account with password.");
//        // code for adding user to role
//        await _userManager.AddToRoleAsync(user, role.Name);
//        // ends here
//        await _signInManager.SignInAsync(user, isPersistent: false);
//        return LocalRedirect(returnUrl);
//    }
//    foreach (var error in result.Errors)
//    {
//        ModelState.AddModelError(string.Empty, error.Description);
//    }
//}

//// If we got this far, something failed, redisplay form
//return Page();



//            public async Task OnGetAsync(string returnUrl = null)
//            {
//                ReturnUrl = returnUrl;
//                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
//            }

//            public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//            {
//                returnUrl = returnUrl ?? Url.Content("~/");
//                returnUrl ??= Url.Content("~/stuff/");
//                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
//                if (ModelState.IsValid)
//                {
//                    var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
//                    var roles = await _userManager.GetRolesAsync(userId = Input.Lastname);
//                    var result = await _userManager.CreateAsync(user, Input.Password);
//                    if (result.Succeeded)
//                    {
//                        _logger.LogInformation("User created a new account with password.");

//                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
//                        var callbackUrl = Url.Page(
//                            "/Account/ConfirmEmail",
//                            pageHandler: null,
//                            values: new { area = "Identity", Userid = user.Id, code, returnUrl },
//                            protocol: Request.Scheme);

//                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
//                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

//                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
//                        {
//                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
//                        }
//                        else
//                        {
//                            await _signInManager.SignInAsync(user, isPersistent: false);
//                            return LocalRedirect(returnUrl);
//                        }
//                    }
//                    foreach (var error in result.Errors)
//                    {
//                        ModelState.AddModelError(string.Empty, error.Description);
//                    }
//                }

//                If we got this far, something failed, redisplay form
//                return Page();
//            }
//        }
//    }
//}


{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please Enter First Name..")]
            [Display(Name = "FirstName")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = "Please Enter Last Name..")]
            [Display(Name = "LastName")]
            public string Lastname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Please Enter Phone Number...")]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [Display(Name = "Please enter your location")]
            public string Location { get; set; }

            [Required(ErrorMessage = "Please Enter Preferred Contact Method...")]
            [Display(Name = "PreferredContact")]
            public string PreferredContact { get; set; }
        }

    public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
