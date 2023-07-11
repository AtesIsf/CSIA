using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class SignOutModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        // This works for teacher cookies too
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie))
            await HttpContext.SignOutAsync(Constants.ADMIN_COOKIE_NAME);

        var teacherCookie = HttpContext.Request.Cookies[Constants.TEACHER_COOKIE_NAME];
        if (!string.IsNullOrEmpty(teacherCookie))
            await HttpContext.SignOutAsync(Constants.TEACHER_COOKIE_NAME);

        return RedirectToPage("/Index");
    }
}
