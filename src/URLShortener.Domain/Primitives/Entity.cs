namespace URLShortener.Domain.Primitives;

[Serializable]
public abstract class Entity<TId>
{
    public virtual TId Id { get; set; }
}