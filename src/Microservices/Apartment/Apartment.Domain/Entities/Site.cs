using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Domain.Entities;

public class Site : Entity , IAggregateRoot
{
    public string? ManagerId { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }


    private List<Block> _blocks = new();
    public IReadOnlyCollection<Block> Blocks => _blocks.AsReadOnly();


    private Site() { }

    public Site(string? managerId, string name, string address)
    {
        ManagerId = managerId;
        Name = name;
        Address = address;
    }

    public void AssignManager(string managerId)
    {
        ManagerId = managerId;
    }


    public void AddBlock(Block block)
    {
        _blocks.Add(block);
    }


    public void RemoveBlock(Block block) 
    {
        _blocks.Remove(block);
    }

    public void RemoveManager()
    {
        ManagerId = null;
    }
}
