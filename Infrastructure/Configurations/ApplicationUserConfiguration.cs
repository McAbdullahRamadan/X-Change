using Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.City).HasMaxLength(100);
            builder.Property(u => u.Country).HasMaxLength(100);
            builder.Property(u => u.NationalId).HasMaxLength(14);
            builder.Property(u => u.RefreshToken).HasMaxLength(500);

            builder.HasMany(u => u.PasswordHistories)
                .WithOne(ph => ph.User)
                .HasForeignKey(ph => ph.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
