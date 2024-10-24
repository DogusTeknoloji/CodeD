namespace CodeD.Domain.Entities;

public class Entity<TId>
{
    public Entity()
    {
    }

    public Entity(TId id, string key)
    {
        Id = id;
        Key = key;
    }

    public TId Id { get; set; }

    public string Key { get; set; }
}
