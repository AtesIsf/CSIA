using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CSClub.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CSClub.Pages.Account;

public class LoginModel : PageModel
{
    // Props
    [BindProperty]
    public CredentialModel Credential { get; set; }

    private ApplicationDbContext _context;

    // Ctors
    public LoginModel(ApplicationDbContext context)
    {
        _context = context;
        // TODO: https://www.youtube.com/watch?v=ReVJolYLZLU here
    }

    // Methods
    public IActionResult OnGet()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return RedirectToPage("/Admin/Dashboard");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Verify Login
        try
        {
            if (_context.Admins.ToList().Where(x => x.Uname == Credential.Username).ToList().Count > 0
                    &&
                _context.Admins.ToList().Where(x => x.Pw == Credential.Password).ToList().Count > 0)
            {
                var rng = new Random();

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    // I can't believe that this worked
                    new Claim(ClaimTypes.Name, $"{Credential.Username}-{rng.NextInt64()}")
                }, "CookieAuth"));

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                return RedirectToPage("/Admin/Dashboard");
            }
        }
        catch
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }
}

public class CredentialModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
