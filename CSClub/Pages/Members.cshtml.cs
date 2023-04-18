using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CSClub.Classes;
using CSClub.Data;

namespace CSClub.Pages;

public class MembersModel : PageModel
{
    // Props
    public MemberDynArr Members { get; set; }

    private ApplicationDbContext _context;

    // Ctors
    public MembersModel(ApplicationDbContext context)
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
    public IActionResult OnPost()
    {
        var sortBy = Request.Form["sortBy"];
        var toggle = Request.Form["toggle"];

        Members.Sort(sortBy, toggle);
        return Page();
    }
}   
