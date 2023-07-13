using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Teacher;

public class LoginModel : PageModel
{
    // Props
    [BindProperty]
    public CredentialModel Credential { get; set; }

    private readonly ApplicationDbContext _context;
    private readonly PageStack _pageStack;

    // Ctors
    public LoginModel(ApplicationDbContext context, PageStack pageStack)
    {
        _context = context;
        _pageStack = pageStack;
    }

    // Methods
    public IActionResult OnGet()
    {
        // Check if this session has an admin cookie too
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie))
            return RedirectToPage("/CookieError");
        
        var teacherCookie = HttpContext.Request.Cookies[Constants.TEACHER_COOKIE_NAME];
        if (!string.IsNullOrEmpty(teacherCookie))
            return RedirectToPage("/Teacher/Logs");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Verify Login
        try
        {
            if (_context.Teachers.ToList().Where(x => x.Uname == Credential.Username).ToList().Count > 0
                    &&
                _context.Teachers.ToList().Where(x => x.Pw == Credential.Password).ToList().Count > 0)
            {
                var rng = new Random();

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    // I can't believe that this worked
                    new Claim(ClaimTypes.Name, $"{Credential.Username}-{rng.NextInt64()}")
                }, Constants.TEACHER_COOKIE_NAME));

                await HttpContext.SignInAsync(Constants.TEACHER_COOKIE_NAME, claimsPrincipal);

                return RedirectToPage("/Teacher/Logs");
            }
        }
        catch
        {
            return RedirectToPage("/Error");
        }
        
        return Page();
    }

    public IActionResult OnPostGoBack()
    {
        var path = _pageStack.Pop();
        if (path == Request.Path)
            path = _pageStack.Pop();

        if (string.IsNullOrEmpty(path))
            return Page();
        return RedirectToPage(path);
    }
}
