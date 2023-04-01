using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using CSClub.Data;
using System.Security.Claims;

namespace CSClub.Pages.Account;

public class LoginModel : PageModel
{
    // Props
    [BindProperty]
    public CredentialModel Credential { get; set; }

    private ApplicationDbContext _context;

    // Ctors
    public LoginModel(ApplicationDbContext context)
    {
        _context = context;
        // TODO: https://www.youtube.com/watch?v=ReVJolYLZLU here
    }

    // Methods
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        if (!ModelState.IsValid) return;

        // Verify Login
        if (_context.Admins.ToList().Where(x => x.Uname == Credential.Username).ToList().Count > 0
                &&
            _context.Admins.ToList().Where(x => x.Pw == Credential.Password).ToList().Count > 0)
        {
            var identity = new ClaimsIdentity("CookieAuth");
        }
    }
}

public class CredentialModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
