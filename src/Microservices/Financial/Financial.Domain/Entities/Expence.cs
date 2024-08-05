using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Financial.Domain.Entities;

public class Expence : Entity, IAggregateRoot
{

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalAmount { get; private set; }

    private List<ExpenceItem> _expenceItems = new();
    public IReadOnlyCollection<ExpenceItem> ExpenceItems => _expenceItems.AsReadOnly();


    private Expence() { }

    public Expence(string title, string description, decimal totalAmount)
    {
        Title = title;
        Description = description;
        TotalAmount = totalAmount;
    }

    public bool AddExpenceItem(ExpenceItem expenceItem)
    {
        if (expenceItem == null) return false;
        _expenceItems.Add(expenceItem);
        return true;
    }

    public bool RemoveExpenceItem(ExpenceItem expenceItem)
    {
        if (expenceItem == null) return false;
        _expenceItems.Remove(expenceItem);
        return true;
    }

    public decimal SetTotalAmount()
    {
        TotalAmount = 0;
        foreach (var item in _expenceItems)
        {
            TotalAmount += item.Amount;
        }
        return TotalAmount;
    }




}
