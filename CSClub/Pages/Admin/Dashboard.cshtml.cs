using System;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class DashboardModel : PageModel
{
	// Props
	public MemberLL Members { get; set; }

	private readonly ApplicationDbContext _context;

	// Ctors
	public DashboardModel(ApplicationDbContext context)
	{
		_context = context;
		Members = new MemberLL();

        try
        {
            foreach (var m in _context.Members)
                Members.Add(m);
        }
        catch
        {
            Members.Add(new ClubMember("Database Connection Error", "X"));
        }
	}

	// Methods
	public IActionResult OnGet()
	{
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
		if (!string.IsNullOrEmpty(adminCookie))
			return Page();
		return RedirectToPage("/Admin/Login");
	}
}

