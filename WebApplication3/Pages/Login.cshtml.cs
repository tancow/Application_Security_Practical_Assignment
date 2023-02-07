using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Login LModel { get; set; } = new();

        private readonly SignInManager<IdentityUser> signInManager;
        public LoginModel(SignInManager<IdentityUser> signInManager)

        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public IActionResult Lockout()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
			if (1==1)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
				LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
					return Redirect("/");
				}
				ModelState.AddModelError(string.Empty, "Username or Password incorrect");

                if (identityResult.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
            }
			return Page();
		}

    }
}
