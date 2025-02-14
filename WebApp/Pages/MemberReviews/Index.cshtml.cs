using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_MemberReviews
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<MemberReview> MemberReview { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MemberReview = await _context.MemberReviews
                .Include(m => m.ReviewedMember)
                .Include(m => m.Reviewer).ToListAsync();
        }
    }
}
