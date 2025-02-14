using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Trade
{
    public int Id { get; set; }

    public int InitiatorId { get; set; }
    
    [ForeignKey("InitiatorId")]
    public Member? Initiator { get; set;}

    public int TargetBookId { get; set; }
    public Book? TargetBook { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ETradeStatus Status { get; set; } = ETradeStatus.Pending;

    public DateTime? CompletedAt { get; set; }

    public int? OfferedBookId { get; set; }
    public Book? OfferedBook { get; set; }
}