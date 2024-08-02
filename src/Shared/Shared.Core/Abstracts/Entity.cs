namespace Shared.Core.Abstracts;

public abstract class Entity
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var entity = (Entity)obj;
        return Id == entity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}