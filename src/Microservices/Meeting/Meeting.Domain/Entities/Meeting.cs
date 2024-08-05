using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Meeting.Domain.Entities;

public class Meeting : Entity, IAggregateRoot
{
    public string SiteId { get; private set; }
    public string Subject { get; private set; }
    public DateTime Date { get; private set; }
    public string? Notes { get; private set; }
    public bool HasVoiting { get; private set; }
    public bool? VotingDecision { get; private set; }

    private Meeting() { }
    public Meeting(string siteId, string subject, DateTime date, string? notes, bool hasVoiting, bool? votingDecision)
    {
        SiteId = siteId;
        Subject = subject;
        Date = date;
        Notes = notes;
        HasVoiting = hasVoiting;
        VotingDecision = votingDecision;
    }

    private List<Voting> _votings = new();

    public IReadOnlyCollection<Voting> Votings => _votings.AsReadOnly();

    public void AddVoting(Voting voting)
    {
        _votings.Add(voting);
    }

    public void RemoveVoting(Voting voting)
    {
        _votings.Remove(voting);
    }

    public bool VotingResult()
    {
        var acceptanceCount = Votings.Where(x => x.Vote == true).Count();
        var rejectionCount = Votings.Where(x => x.Vote == false).Count();

        if (acceptanceCount > rejectionCount)
            return true;

        return false;

    }

}
