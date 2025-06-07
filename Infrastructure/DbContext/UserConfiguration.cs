using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(b => b.Id);

            entity.HasMany(u => u.ReadingTasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
