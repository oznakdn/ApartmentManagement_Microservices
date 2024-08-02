using Shared.Core.Abstracts;

namespace Apartment.Domain.QueryEntities;

public class SiteQuery : Entity
{
    public string SiteId { get; set; }
    public string? ManagerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    private List<BlockQuery> _blocks = new();
    public IReadOnlyCollection<BlockQuery> Blocks => _blocks.AsReadOnly();

    private SiteQuery() { }

    public SiteQuery(string siteId, string? managerId, string name, string address)
    {
        SiteId = siteId;
        ManagerId = managerId;
        Name = name;
        Address = address;
    }

    public void AssignManager(string managerId)
    {
        ManagerId = managerId;
    }


    public void AddBlock(BlockQuery block)
    {
        _blocks.Add(block);
    }


    public void RemoveBlock(BlockQuery block)
    {
        _blocks.Remove(block);
    }

}
