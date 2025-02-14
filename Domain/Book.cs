

namespace Domain;

public class Book
{
    public int Id { get; set; }
    
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Genre { get; set; } = default!;
    
    public int OwnerId { get; set; }
    public Member? Owner { get; set; }
    
    public ICollection<BookReview> Reviews { get; set; } = new List<BookReview>();
}