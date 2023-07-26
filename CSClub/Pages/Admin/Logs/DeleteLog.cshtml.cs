using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin.Logs;

public class DeleteLogModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public LogModel? Log { get; set; }

    // Ctors
    public DeleteLogModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Methods
    public IActionResult OnGet(int? id)
    {
        if (id == null)
            return RedirectToPage("/Teacher/Logs");

        Log = _context.Logs.Find(id);

        if (Log == null)
            return RedirectToPage("/Teacher/Logs");

        return Page();
    }

    public IActionResult OnPostConfirmDelete()
    {
        var selectedLog = _context.Logs.Find(Log.Id);

        if (selectedLog == null)
            return RedirectToPage("/Error");

        _context.Logs.Remove(selectedLog);
        _context.SaveChanges();

        return RedirectToPage("/Teacher/Logs");
    }
}
