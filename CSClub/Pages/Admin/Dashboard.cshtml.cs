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

    // TOOD: FIX THIS PART OF THIS PROGRAM
    #region ShouldBeFixed
    public IActionResult OnPostModifyMeetingsAttended()
	{
        int id = Convert.ToInt32(Request.Form["id"]);
		bool inc = Convert.ToBoolean(Request.Form["inc"]);
		bool dec = Convert.ToBoolean(Request.Form["dec"]);

        using (_context)
        {
            var selectedMember = Members.First(x => x.Id == id);

            if (inc)
                selectedMember.MeetingsAttended++;

            else if (dec && (selectedMember.MeetingsAttended > 0))
                selectedMember.MeetingsAttended--;

            _context.SaveChanges();
        }
		return Page();
    }

	public IActionResult OnPostDeleteMember()
	{
		var id = Convert.ToInt32(Request.Form["id"]);

        using (_context)
		{
			var selectedMember = Members.First(x => x.Id == id);

			Members.Remove(selectedMember.Name);
			_context.Remove(selectedMember);

			Members.Sync(_context);
			_context.SaveChanges();
		}
		return Page();
	}

	public IActionResult OnPostAddMember()
	{
		var name = Request.Form["name"];
        var grade = Request.Form["grade"];

        using (_context)
		{
			_context.Members.Add(new ClubMember(name, grade));
			Members.Sync(_context);
			_context.SaveChanges();
		}
		return Page();
	}
    #endregion
}

