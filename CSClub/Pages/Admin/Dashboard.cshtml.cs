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
	private readonly PageStack _pageStack;

	// Ctors
	public DashboardModel(ApplicationDbContext context, PageStack pageStack)
	{
		_context = context;
		_pageStack = pageStack;
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
		{
			_pageStack.Push(Request.Path);
			return Page();
		}
		return RedirectToPage("/Admin/Login");
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

