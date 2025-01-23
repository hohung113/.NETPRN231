using Base.Domain.Interfaces.Domain;

namespace Base.Infrastructure.Implementation
{
    public abstract class BaseEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

}
