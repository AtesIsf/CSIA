using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin.CRUD;

public class DeleteMemberModel : PageModel
{
    // Props

    private readonly ApplicationDbContext _context;

    public DeleteMemberModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {

        return RedirectToPage("/Admin/Dashboard");
    }
}
