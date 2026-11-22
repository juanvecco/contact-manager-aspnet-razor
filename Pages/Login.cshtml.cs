using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactManager.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Username { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            var adminUsername = _configuration["Admin:Usuario"];
            var adminPassword = _configuration["Admin:Senha"];
            if (Username == adminUsername && Password == adminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToPage("/Contacts/Index");
            }
            ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos.");
            return Page();
        }

        public async Task<IActionResult> OnPostSairAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Contacts/Index");
        }
    }
}
