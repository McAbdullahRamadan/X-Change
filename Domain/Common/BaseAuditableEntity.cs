using Domain.Entities.BaseEntities;
using Domain.Entities.System;

namespace Domain.Common
{
    public interface IBaseAuditableEntity : IBaseEntity
    {
        public DateTimeOffset Created { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string LastModifiedByUserId { get; set; }
    }

    public abstract class BaseAuditableEntity<T> : BaseEntity<T>, IBaseAuditableEntity
    {
        public DateTimeOffset Created { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string LastModifiedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
        public virtual ApplicationUser LastModifiedByUser { get; set; }
    }
}
