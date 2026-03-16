using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{

    public class SectionConfiguration : BaseEntityConfiguration<Section>
    {
        public override void Configure(EntityTypeBuilder<Section> builder)
        {
            base.Configure(builder);

            builder.ToTable("Sections");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

            builder.HasMany(s => s.Lessons)
                .WithOne(l => l.Section)
                .HasForeignKey(l => l.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
