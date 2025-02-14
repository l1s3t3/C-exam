using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class MemberReview
{
    public int Id { get; set; }

    public int ReviewerId { get; set; }
    [ForeignKey("ReviewerId")]
    public Member? Reviewer { get; set; }

    public int ReviewedMemberId { get; set; }
    [ForeignKey("ReviewedMemberId")]
    public Member? ReviewedMember { get; set; }

    public int Rating { get; set; }
    public string Comments { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}