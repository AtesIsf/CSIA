using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class SignOutModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToPage("/Index");
    }
}
