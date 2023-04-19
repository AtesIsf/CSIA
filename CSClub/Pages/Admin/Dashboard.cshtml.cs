using System;
using CSClub.Classes;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class DashboardModel : PageModel
{
	// Props
	public MemberDynArr Members { get; set; }

	private readonly ApplicationDbContext _context;

	// Ctors
	public DashboardModel(ApplicationDbContext context)
	{
		_context = context;
		Members = new MemberDynArr();

        try
        {
            foreach (var m in _context.Members)
                Members.Add(m);
        }
        catch
        {
            Members.Add(new ClubMember("Database Connection Error", "X"));
        }
        Members.Compress();
	}

	// Methods
	public IActionResult OnGet()
	{
		if (HttpContext.User.Identity.IsAuthenticated)
			return Page();
		return RedirectToPage("/Admin/Login");
	}
}

