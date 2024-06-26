namespace BlitzTech.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}