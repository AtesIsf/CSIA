using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin.CRUD;

public class ModifyMeetingsAttendedModel : PageModel
{
    // Props
    public MemberBinSearchTree Members { get; set; }

    private readonly ApplicationDbContext _context;

    public ModifyMeetingsAttendedModel(ApplicationDbContext context)
    {
        _context = context;
        Members = new MemberBinSearchTree(_context.Members.ToArray());
    }

    public IActionResult OnGet()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return RedirectToPage("/Admin/Dashboard");
        return RedirectToPage("/Admin/Login");
    }

    public IActionResult OnPost()
    {
        var id = Convert.ToInt32(Request.Form["id"]);
        var operation = Convert.ToInt32(Request.Form["operation"]);

        using (_context)
        {
            var member = Members.Search(id);
            if (member == null)
                return RedirectToPage("/Admin/Dashboard");

            if (operation == 1)
                member.MeetingsAttended++;
            else if (operation == 0)
                member.MeetingsAttended--;

            _context.SaveChanges();
        }
        return RedirectToPage("/Admin/Dashboard");
    }
}
