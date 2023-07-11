using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class AddMemberModel : PageModel
{
    // Props
    private readonly ApplicationDbContext _context;

    // Ctors
    public AddMemberModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Methods
    public IActionResult OnGet()
    {
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie))
            return RedirectToPage("/Admin/Dashboard");
        return RedirectToPage("/Admin/Login");
    }

    public IActionResult OnPost()
    {
        var name = Request.Form["name"];
        var grade = Request.Form["grade"];

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(grade))
            return RedirectToPage("/Admin/Dashboard");

        using (_context)
        {
            _context.Members.Add(new ClubMember(name, grade));
            _context.SaveChanges();
        }
        return RedirectToPage("/Admin/Dashboard");
    }
}
