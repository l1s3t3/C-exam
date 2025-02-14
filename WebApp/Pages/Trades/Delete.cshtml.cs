using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Trades
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trade Trade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trades.FirstOrDefaultAsync(m => m.Id == id);

            if (trade is not null)
            {
                Trade = trade;

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

            var trade = await _context.Trades.FindAsync(id);
            if (trade != null)
            {
                Trade = trade;
                _context.Trades.Remove(Trade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
