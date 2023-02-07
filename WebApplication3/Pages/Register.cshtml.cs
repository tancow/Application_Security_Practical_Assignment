using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }

        private IWebHostEnvironment _environment;

        [BindProperty]
        public Register RModel { get; set; }
        public IFormFile? Upload { get; set; }


        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = RModel.FullName,
                    Email = RModel.Email
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
 
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //if (Upload != null)
                //{
                //    if (Upload.Length > 2 * 1024 * 1024)
                //    {
                //        ModelState.AddModelError("Upload",
                //        "File size cannot exceed 2MB.");
                //        return Page();
                //    }
                //    var uploadsFolder = "uploads";
                //    var imageFile = Guid.NewGuid() + Path.GetExtension(
                //    Upload.FileName);
                //    var imagePath = Path.Combine(_environment.ContentRootPath,
                //    "wwwroot", uploadsFolder, imageFile);
                //    using var fileStream = new FileStream(imagePath,
                //    FileMode.Create);
                //    await Upload.CopyToAsync(fileStream);
                //    Register.ImageURL = string.Format("/{0}/{1}", uploadsFolder,
                //    imageFile);
                //}

            }
            return Page();
        }







    }
}
