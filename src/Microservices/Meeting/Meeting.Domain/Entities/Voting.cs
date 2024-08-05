using Shared.Core.Abstracts;

namespace Meeting.Domain.Entities;

public class Voting : Entity
{
    public string ResidentId { get; private set; }
    public bool Vote { get; private set; }

    private Voting() { }

    public Voting(string residentId, bool vote)
    {
        ResidentId = residentId;
        Vote = vote;
    }
}
