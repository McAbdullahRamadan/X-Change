namespace Infrastructure.Configurations
{

    using Domain.Entities.BaseEntities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<int>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
