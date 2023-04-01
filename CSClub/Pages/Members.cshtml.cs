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
    public MemberDynArr Members { get; set; }

    public string CurrOrder { get; set; }

    private ApplicationDbContext _context;

    public MembersModel(ApplicationDbContext context)
    {
        _context = context;

        CurrOrder = "Ascending";


        Members = new MemberDynArr();
        foreach (var m in _context.Members)
            Members.Add(m);
        Members.Compress();
    }

    public void OnGet(string? sortBy, string? toggle)
    {
        if (sortBy?.ToLower() == "name")
            Members.SortByName(toggle);

        else if (sortBy?.ToLower() == "grade")
            Members.SortByGrade(toggle);

        else if (sortBy?.ToLower() == "meetings")
            Members.SortByMeetings(toggle);
    }
}   
