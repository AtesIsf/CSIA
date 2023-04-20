using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin.CRUD;

public class ConfirmDeleteModel : PageModel
{
    // Props
    public MemberBinSearchTree Members { get; set; }
    public ClubMember? Member { get; set; }

    private readonly ApplicationDbContext _context;

    // Ctors
    public ConfirmDeleteModel(ApplicationDbContext context)
    {
        _context = context;
        Members = new MemberBinSearchTree(_context.Members.ToArray());
        Member = null;
    }

    // Methods
    public IActionResult OnGet()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return Page();
        return RedirectToPage("/Admin/Login");
    }

    public IActionResult OnPost()
    {
        var id = Convert.ToInt32(Request.Form["id"]);
        Member = Members.Search(id);
        if (Member == null)
            return RedirectToPage("/Admin/Dashboard");
        return Page();
    }
}
