using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages_Trades
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        public Create(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trade Trade { get; set; } = default!;

        public IActionResult OnGet(int? memberId)
        {
            if (memberId == null)
            {
                // No member ID => maybe redirect to error or fallback
                return NotFound("No memberId provided.");
            }

            var currentUserId = memberId.Value; // we got from the query string

            // Initiator is the current user
            Trade = new Trade
            {
                InitiatorId = currentUserId,
                Status = ETradeStatus.Pending
            };

            // The user’s own books => can offer
            var offeredBooks = _context.Books
                .Where(b => b.OwnerId == currentUserId)
                .ToList();

            // Other members' books => can be target
            var targetBooks = _context.Books
                .Where(b => b.OwnerId != currentUserId)
                .ToList();

            ViewData["OfferedBooks"] = new SelectList(offeredBooks, "Id", "Title");
            ViewData["TargetBooks"]  = new SelectList(targetBooks, "Id", "Title");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? memberId)
        {
            if (!ModelState.IsValid) return Page();

            // Force the status to Pending
            Trade.Status = ETradeStatus.Pending;

            if (memberId == null)
            {
                return NotFound("No memberId provided.");
            }

            // Force the initiator to be that user
            Trade.InitiatorId = memberId.Value;

            _context.Trades.Add(Trade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); 
        }
    }
}
