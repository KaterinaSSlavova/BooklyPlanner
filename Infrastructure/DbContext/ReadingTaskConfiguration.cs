using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class ReadingTaskConfiguration: IEntityTypeConfiguration<ReadingTask>
    {
        public void Configure(EntityTypeBuilder<ReadingTask> entity)
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.CreatedAt)
                 .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(t => t.IsCompleted)
                .HasDefaultValue(false);

            entity.Property(t => t.IsArchived)
                .HasDefaultValue(false);    
        }
    }
}
