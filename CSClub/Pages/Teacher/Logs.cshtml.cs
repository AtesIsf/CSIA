using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Teacher;

public class LogsModel : PageModel
{
    // Methods
    public IActionResult OnGet()
    {
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        var teacherCookie = HttpContext.Request.Cookies[Constants.TEACHER_COOKIE_NAME];

        if (!string.IsNullOrEmpty(adminCookie) || !string.IsNullOrEmpty(teacherCookie))
            return Page();
        return RedirectToPage("/Teacher/Login");
    }
}
