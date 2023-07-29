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
    private readonly PageStack _pageStack;

    // Ctors
    public MembersModel(ApplicationDbContext context, PageStack pageStack)
    {
        _context = context;
        _pageStack = pageStack;
        
        try
        {
            Members = new MemberDynArr(_context.Members.ToArray());
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
        var last = _pageStack.Peek();
        if (last == Request.Path)
            return Page();
        _pageStack.Push(Request.Path);
        return Page();
    }

    public IActionResult OnPost()
    {
        var sortBy = Request.Form["sortBy"];
        var toggle = Request.Form["toggle"];

        Members.Sort(sortBy, toggle);
        return Page();
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
