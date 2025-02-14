using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<BookReview> BookReviews { get; set; } = default!;
    public DbSet<Member> Members { get; set; } = default!;
    public DbSet<MemberReview> MemberReviews { get; set; } = default!;
    public DbSet<Trade> Trades { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}