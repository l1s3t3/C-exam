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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Trade> TradeList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TradeList = await _context.Trades
                .Include(t => t.Initiator)
                .Include(t => t.OfferedBook)
                .Include(t => t.TargetBook).ToListAsync();
        }
    }
}
