using Domain.Entities.BaseEntitiys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.BaseEntities
{
    public interface IBaseEntity
    {
        IReadOnlyCollection<BaseEvent> GetDomainEvents();
        void AddDomainEvent(BaseEvent domainEvent);
        void RemoveDomainEvent(BaseEvent domainEvent);
        void ClearDomainEvents();
    }
    public abstract class BaseEntity<T> : IBaseEntity
    {
        // This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
        // Using non-generic integer types for simplicity
        public T Id { get; set; }
        public byte[] RowVersion { get; set; }

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyCollection<BaseEvent> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }


    }
}
