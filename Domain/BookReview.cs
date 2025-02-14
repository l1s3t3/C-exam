namespace Domain;

public class BookReview
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    public Book? Book { get; set; }
    
    public int ReviewerId { get; set; }
    public Member? Reviewer { get; set; }
    
    public int Rating { get; set; }   // Näiteks 1-5
    public string Comments { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}