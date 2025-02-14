using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Review : PageModel
{
    private readonly AppDbContext _context;

    public Review(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] 
    public string? UserName { get; set; }

    public void OnGet()
    {
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            ModelState.AddModelError("UserName", "Please enter a valid UserName.");
            return Page();
        }

        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.UserName == UserName);

        if (member == null)
        {
            ModelState.AddModelError("UserName", "No such user found.");
            return Page();
        }
        
        return RedirectToPage("/BookReviews/Create", new { memberId = member.Id });
    }
}