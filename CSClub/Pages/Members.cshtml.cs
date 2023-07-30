using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CSClub.ADT;
using CSClub.Data;

namespace CSClub.Pages;

public class MembersModel : PageModel
{
    // Props
    public MemberDynArr Members { get; set; }

    private readonly ApplicationDbContext _context;

    // Ctors
    public MembersModel(ApplicationDbContext context)
    {
        _context = context;
        
        try
        {
            Members = new MemberDynArr(_context.Members.ToArray());
        }
        catch
        {
            Members = new MemberDynArr();
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
