﻿using CSClub.Data;
using CSClub.ADT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Teacher;

public class LogsModel : PageModel
{
    private readonly PageStack _pageStack;
    private readonly ApplicationDbContext _context;

    public List<LogModel> Logs { get; set; }

    // Ctors
    public LogsModel(ApplicationDbContext context, PageStack pageStack)
    {
        _pageStack = pageStack;
        _context = context;

        Logs = _context.Logs.ToList();
    }

    // Methods
    public IActionResult OnGet()
    {
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        var teacherCookie = HttpContext.Request.Cookies[Constants.TEACHER_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie) || !string.IsNullOrEmpty(teacherCookie))
        {
            _pageStack.Push(Request.Path);
            return Page();
        }
        return RedirectToPage("/Teacher/Login");
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