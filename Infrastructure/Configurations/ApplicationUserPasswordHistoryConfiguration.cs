using Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{

    public class ApplicationUserPasswordHistoryConfiguration : IEntityTypeConfiguration<ApplicationUserPasswordHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserPasswordHistory> builder)
        {
            builder.ToTable("ApplicationUserPasswordHistories");

            builder.HasKey(ph => ph.Id);

            builder.Property(ph => ph.PasswordHash).IsRequired().HasMaxLength(500);

            builder.HasOne(ph => ph.User)
                .WithMany(u => u.PasswordHistories)
                .HasForeignKey(ph => ph.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
