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

	// TODO: DEBUG
	public async Task<IActionResult> OnPostAsync()
	{
        string? methodToCall = Request.Form["methodToCall"];
        int memberId = Convert.ToInt32(Request.Form["id"]);
        string? param1 = Request.Form["param1"];
        string? param2 = Request.Form["param2"];

        if (methodToCall == "ModifyMeetingAttended")
			return await ModifyMeetingsAttended(memberId, Boolean.Parse(param1), Boolean.Parse(param2));
		if (methodToCall == "DeleteMember")
			return await DeleteMember(memberId);
		if (methodToCall == "AddMember")
			return await AddMember(param1, param2);
		return Page();
	}

	private async Task<IActionResult> ModifyMeetingsAttended(int id, bool inc, bool dec)
	{
        using (_context)
        {
            var selectedMember = Members.First(x => x.Id == id);

            if (inc)
                selectedMember.MeetingsAttended++;

            else if (dec && (selectedMember.MeetingsAttended > 0))
                selectedMember.MeetingsAttended--;

            await _context.SaveChangesAsync();
        }
		return Page();
    }

	private async Task<IActionResult> DeleteMember(int id)
	{
		using (_context)
		{
			var selectedMember = Members.First(x => x.Id == id);

			Members.Remove(selectedMember.Name);
			_context.Remove(selectedMember);

			Members.Sync(_context);
			await _context.SaveChangesAsync();
		}
		return Page();
	}

	private async Task<IActionResult> AddMember(string name, string grade)
	{
		using (_context)
		{
			_context.Members.Add(new ClubMember(name, grade));
			Members.Sync(_context);
			await _context.SaveChangesAsync();
		}
		return Page();
	}
}

