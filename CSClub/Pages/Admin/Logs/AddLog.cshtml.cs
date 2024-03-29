﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using CSClub.ADT;
using CSClub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSClub.Pages.Admin;

public class AddLogModel : PageModel
{
    // Props
    private readonly ApplicationDbContext _context;

    public MemberBinSearchTree MemberBST { get; set; }
    public MemberDynArr Members { get; set; }

    // Ctors
    public AddLogModel(ApplicationDbContext context)
    {
        _context = context;

        MemberBST = new MemberBinSearchTree(_context.Members.ToArray());
        Members = new MemberDynArr(_context.Members.ToArray());
        
    }

    // Methods
    public IActionResult OnGet()
    {
        var adminCookie = HttpContext.Request.Cookies[Constants.ADMIN_COOKIE_NAME];
        if (!string.IsNullOrEmpty(adminCookie))
            return Page();
        return RedirectToPage("/Admin/Login");
    }

    public IActionResult OnPostAddLog()
    {
        var date = Request.Form["date"];
        var entry = Request.Form["entry"];
        var ids = Request.Form["ids"].ToArray();
        Console.WriteLine(ids);

        if (String.IsNullOrEmpty(date))
            return Page();

        using (_context)
        {
            var members = "";

            if (!ids.All(String.IsNullOrEmpty))
                foreach (var id in ids)
                {
                    var member = MemberBST.Search(Convert.ToInt32(id));
                    if (member == null)
                        continue;
                    member.MeetingsAttended++;
                    members+=$"{member.Name} ";
                }

            _context.Logs.Add(new LogModel() { Date = date, Entry = entry, Attendance=members});

            _context.SaveChanges();
        }
        return Page();
    }
}
