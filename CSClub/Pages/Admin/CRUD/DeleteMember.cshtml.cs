using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSClub.ADT;

namespace CSClub.Pages.Admin.CRUD;

public class DeleteMemberModel : PageModel
{
    // props
    public MemberBinSearchTree Members { get; set; }
    private readonly ApplicationDbContext _context;

    public DeleteMemberModel(ApplicationDbContext context)
    {
        _context = context;
        Members = new MemberBinSearchTree(_context.Members.ToArray());
    }

    public IActionResult OnGet()
    {
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie))
            return RedirectToPage("/Admin/Dashboard");
        return RedirectToPage("/Admin/Login");
    }

    public IActionResult OnPost()
    {
        var id = Convert.ToInt32(Request.Form["id"]);

        using (_context)
        {
            var member = Members.Search(id);
            if (member == null)
                return RedirectToPage("/Admin/Dashboard");

            _context.Members.Remove(member);
            _context.SaveChanges();
        }
        return RedirectToPage("/Admin/Dashboard");
    }
}
