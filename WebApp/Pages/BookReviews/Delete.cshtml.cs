using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_BookReviews
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookReview BookReview { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookreview = await _context.BookReviews.FirstOrDefaultAsync(m => m.Id == id);

            if (bookreview is not null)
            {
                BookReview = bookreview;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookreview = await _context.BookReviews.FindAsync(id);
            if (bookreview != null)
            {
                BookReview = bookreview;
                _context.BookReviews.Remove(BookReview);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
