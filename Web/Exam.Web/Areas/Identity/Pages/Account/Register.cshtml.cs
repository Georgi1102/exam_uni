using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Exam.DataModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Exam.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ExamUser> _signInManager;
        private readonly UserManager<ExamUser> _userManager;

        public RegisterModel(
            UserManager<ExamUser> userManager,
            SignInManager<ExamUser> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }



        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Text)]
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            public string LastName { get; set; }
        }

        public async System.Threading.Tasks.Task OnGetAsync(string returnUrl = null)
        {

        }

        public async Task<IActionResult> OnPostAsync(string isAdmin = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ExamUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = Input.Username,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client");

                    if (isAdmin != "yes") await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(isAdmin != "yes" ? "/" : "/Admin/Users");

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
