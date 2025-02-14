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
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public MemberReview MemberReview { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberreview = await _context.MemberReviews.FirstOrDefaultAsync(m => m.Id == id);

            if (memberreview is not null)
            {
                MemberReview = memberreview;

                return Page();
            }

            return NotFound();
        }
    }
}
