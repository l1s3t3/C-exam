using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;
using System.Linq;

namespace WebApp.Pages_BookReviews
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookReview BookReview { get; set; } = default!;

        // GET: /BookReviews/Create?memberId=123
        public IActionResult OnGet(int? memberId)
        {
            if (memberId == null)
            {
                // If no memberId is provided, user can see all books and all members
                ViewData["BookList"] = new SelectList(_context.Books, "Id", "Title");
                ViewData["ReviewerList"] = new SelectList(_context.Members, "Id", "Email");

                return Page();
            }

            // 1) Retrieve this member
            var member = _context.Members.Find(memberId);
            if (member == null)
            {
                // If invalid id, fallback
                ViewData["BookList"] = new SelectList(_context.Books, "Id", "Title");
                ViewData["ReviewerList"] = new SelectList(_context.Members, "Id", "Email");
                return Page();
            }

            // 2) Filter the books to only those owned by this member
            var userBooks = _context.Books
                .Where(b => b.OwnerId == member.Id)
                .ToList();

            // 3) For the reviewer, you can either:
            //    a) Provide only that single member
            //    b) or still provide all members
            var singleReviewerList = new List<Member> { member };

            // 4) Populate the drop-downs
            ViewData["BookList"] = new SelectList(userBooks, "Id", "Title");
            ViewData["ReviewerList"] = new SelectList(singleReviewerList, "Id", "UserName");

            // Optionally set a default on the BookReview
            BookReview = new BookReview
            {
                ReviewerId = member.Id,
                CreatedAt = DateTime.UtcNow
            };

            return Page();
        }

        // POST: /BookReviews/Create
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // BookReview has BookId, ReviewerId, Rating, Comments, CreatedAt
            _context.BookReviews.Add(BookReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
