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

            entity.HasMany(b => b.ReadingTasks)
                .WithOne(t => t.Book)
                .HasForeignKey(b => b.BookId);
        }
    }
}
