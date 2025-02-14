using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages_MemberReviews
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        // This is the review we want to create
        [BindProperty]
        public MemberReview MemberReview { get; set; } = default!;

        // GET: /MemberReviews/Create?memberId=123
        // memberId => the "Reviewer"
        public IActionResult OnGet(int? memberId)
        {
            if (memberId == null)
            {
                return NotFound("No memberId provided.");
            }

            // 1) The user who is creating the review
            var reviewer = _context.Members.Find(memberId.Value);
            if (reviewer == null)
            {
                return NotFound("Reviewer not found.");
            }

            // 2) Pre-set the reviewer in the new review object
            MemberReview = new MemberReview
            {
                ReviewerId = reviewer.Id,
                CreatedAt = DateTime.UtcNow
            };

            // 3) Prepare the dropdown for "ReviewedMemberId" 
            //    with all members except the reviewer themself
            var otherMembers = _context.Members
                .Where(m => m.Id != reviewer.Id)
                .ToList();

            ViewData["ReviewedMemberList"] = new SelectList(
                otherMembers, 
                "Id", 
                "UserName" // or "Email" or whatever you prefer
            );

            return Page();
        }

        // POST: /MemberReviews/Create?memberId=123
        public async Task<IActionResult> OnPostAsync(int? memberId)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the dropdown if there's a validation error
                if (memberId != null)
                {
                    var reviewer = _context.Members.Find(memberId.Value);
                    var otherMembers = _context.Members
                        .Where(m => m.Id != memberId.Value)
                        .ToList();
                    ViewData["ReviewedMemberList"] = new SelectList(otherMembers, "Id", "UserName");
                }
                return Page();
            }

            if (memberId == null)
            {
                return NotFound("No memberId provided.");
            }

            // Force reviewer to be that user
            MemberReview.ReviewerId = memberId.Value;

            // Save
            _context.MemberReviews.Add(MemberReview);
            await _context.SaveChangesAsync();

            // For example, go back to an Index page
            return RedirectToPage("./Index");
        }
    }
}
