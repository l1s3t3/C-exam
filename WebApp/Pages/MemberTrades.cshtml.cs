using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class MemberTrades : PageModel
{
    private readonly AppDbContext _context;

    public MemberTrades(AppDbContext context)
    {
        _context = context;
    }

    public int MemberId { get; set; }
    public List<Trade> TradeList { get; set; } = new();
    public List<MemberReview> ReviewsAboutUser { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? memberId)
    {
        if (memberId == null)
        {
            return NotFound("No memberId provided.");
        }
        MemberId = memberId.Value;
        
        TradeList = await _context.Trades
            .Include(t => t.Initiator)
            .Include(t => t.OfferedBook).ThenInclude(b => b!.Owner!)
            .Include(t => t.TargetBook).ThenInclude(b => b!.Owner!)
            .Where(t => t.InitiatorId == MemberId ||
                        (t.TargetBook != null && t.TargetBook.OwnerId == MemberId))
            .ToListAsync();
        
        ReviewsAboutUser = await _context.MemberReviews
            .Include(r => r.Reviewer)
            .Include(r => r.ReviewedMember)
            .Where(r => r.ReviewedMemberId == MemberId)
            .ToListAsync();

        return Page();
    }
    
    public async Task<IActionResult> OnPostAcceptAsync(int TradeId)
    {
        var trade = await _context.Trades
            .Include(t => t.TargetBook)
            .Include(t => t.OfferedBook)
            .FirstOrDefaultAsync(t => t.Id == TradeId);

        if (trade == null) return NotFound("Trade not found.");
        if (trade.Status != ETradeStatus.Pending) 
            return BadRequest("Trade is not pending.");
        
        if (trade.TargetBook != null && trade.OfferedBook != null)
        {
            var tmpOwner = trade.TargetBook.OwnerId;
            trade.TargetBook.OwnerId = trade.OfferedBook.OwnerId;
            trade.OfferedBook.OwnerId = tmpOwner;
        }
        else if (trade.TargetBook != null && trade.OfferedBook == null)
        {
            trade.TargetBook.OwnerId = trade.InitiatorId;
        }

        trade.Status = ETradeStatus.Accepted;
        trade.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return RedirectToPage(new { memberId = trade.TargetBook?.OwnerId }); 
    }
    
    public async Task<IActionResult> OnPostDiscardAsync(int TradeId)
    {
        var trade = await _context.Trades.FindAsync(TradeId);
        if (trade == null) return NotFound("Trade not found.");
        if (trade.Status != ETradeStatus.Pending)
            return BadRequest("Trade is not pending.");

        trade.Status = ETradeStatus.Declined;
        await _context.SaveChangesAsync();

        return RedirectToPage(new { memberId = trade.InitiatorId });
    }
}
