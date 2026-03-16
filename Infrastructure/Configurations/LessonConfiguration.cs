namespace Infrastructure.Configurations
{
    using Domain.Entities.Business;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LessonConfiguration : BaseEntityConfiguration<Lesson>
    {
        public override void Configure(EntityTypeBuilder<Lesson> builder)
        {
            base.Configure(builder);

            builder.ToTable("Lessons");

            builder.Property(l => l.Title).IsRequired().HasMaxLength(200);
            builder.Property(l => l.VideoUrl).HasMaxLength(500);
        }
    }
}
