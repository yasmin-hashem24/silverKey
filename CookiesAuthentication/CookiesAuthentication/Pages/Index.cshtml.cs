using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using System.Security.Claims;
namespace CookiesAuthentication.Pages

{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set;}

        [BindProperty]
        public string Password { get; set; }

        public Boolean inCorrectly = false;

        public async Task<IActionResult> OnPost()
        {



            if (ModelState.IsValid)
            {
                if (this.UserName == "intern" &&  this.Password == "summer 2023 july")
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, this.UserName),
                };

                    var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = true,

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    inCorrectly = true;
                }
               

            };

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }

    }
    
}