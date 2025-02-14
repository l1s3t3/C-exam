using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Exchange : PageModel
{
    private readonly AppDbContext _context;

    public Exchange(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string? UserName { get; set; }

    [BindProperty]
    public bool ViewYourTrades { get; set; }

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = "Please enter a valid UserName.";
            return Page();
        }

        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.UserName == UserName);

        if (member == null)
        {
            ErrorMessage = "No such user found.";
            return Page();
        }
        
        if (ViewYourTrades)
        {
            return RedirectToPage("/MemberTrades", new { memberId = member.Id });
        }
        return RedirectToPage("/Trades/Create", new { memberId = member.Id });
    }
}