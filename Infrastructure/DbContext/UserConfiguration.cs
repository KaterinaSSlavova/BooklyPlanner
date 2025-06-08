using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.Username).IsUnique();

            entity.HasIndex(u => u.Email).IsUnique();

            entity.HasMany(u => u.ReadingTasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
