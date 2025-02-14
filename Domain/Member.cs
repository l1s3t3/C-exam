using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Member
{
    public int Id { get; set; }

    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    
    public ICollection<Book> OwnedBooks { get; set; } = new List<Book>();
    
    [InverseProperty("Initiator")]
    public ICollection<Trade> InitiatedTrades { get; set; } = new List<Trade>();
    
    [InverseProperty("Reviewer")]
    public ICollection<MemberReview> GivenMemberReviews { get; set; } = new List<MemberReview>();
    
    [InverseProperty("ReviewedMember")]
    public ICollection<MemberReview> ReceivedMemberReviews { get; set; } = new List<MemberReview>();

    public ICollection<BookReview> GivenBookReviews { get; set; } = new List<BookReview>();
}