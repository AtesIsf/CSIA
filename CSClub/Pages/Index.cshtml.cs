using CSClub.ADT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IA_CSClub.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly PageStack _pageStack;

    public IndexModel(ILogger<IndexModel> logger, PageStack pageStack)
    {
        _logger = logger;
        _pageStack = pageStack;
    }

    // /Index instead of Request.Path as Request.Path is "/", not "/Index"
    // Methods
    public IActionResult OnGet()
    {
        var last = _pageStack.Peek();
        if (last == "/Index")
            return Page();
        _pageStack.Push("/Index");
        return Page();
    }

    public IActionResult OnPostGoBack()
    {
        var path = _pageStack.Pop();
        if (path == "/Index")
            path = _pageStack.Pop();

        if (string.IsNullOrEmpty(path))
            return Page();
        return RedirectToPage(path);
    }
}

