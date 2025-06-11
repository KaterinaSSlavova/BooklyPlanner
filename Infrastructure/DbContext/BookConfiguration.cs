using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class BookConfiguration: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Title).HasMaxLength(50);
            entity.Property(b => b.Author).HasMaxLength(50);
            entity.Property(b => b.Image).HasMaxLength(200);

            entity.HasMany(b => b.ReadingTasks)
                .WithOne(t => t.Book)
                .HasForeignKey(b => b.BookId);
        }
    }
}
