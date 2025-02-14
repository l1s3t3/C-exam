using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }
    
    public IList<Book> Books { get; set; } = default!;
    public string? SearchString { get; set; }

    public async Task OnGetAsync(string? searchString)
    {
        SearchString = searchString;
        var query = _context.Books
            .Include(b => b.Owner)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(SearchString))
        {
            var lowerSearch = SearchString.ToLower();
            query = query.Where(b =>
                b.Title.ToLower().Contains(lowerSearch) ||
                b.Author.ToLower().Contains(lowerSearch) ||
                b.Description.ToLower().Contains(lowerSearch) ||
                b.Genre.ToLower().Contains(lowerSearch));

        }
        
        Books = await query.ToListAsync();
    }
}