using CSClub.Data;
using CSClub.ADT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Teacher;

public class LogsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<LogModel> Logs { get; set; }

    // Ctors
    public LogsModel(ApplicationDbContext context)
    {
        _context = context;

        try
        {
            Logs = _context.Logs.ToList();
        }
        catch
        {
            Logs = new List<LogModel>()
            {
                new LogModel()
                {
                    Entry = "Database Connection Error",
                    Date = "X",
                    Attendance = "X"
                }
            };
        }
    }

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
