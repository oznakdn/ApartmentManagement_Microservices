using Shared.Core.Abstracts;

namespace Notification.Domain.Entities;

public class Suggestion : Entity
{
    public string SenderResidentId { get; private set; }
    public string ReceiverManagertId { get; private set; }
    public string Subject { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public bool IsRead { get; private set; } = false;

    private Suggestion() { }
    public Suggestion(string senderResidentId, string receiverManagerId, string subject, string? content)
    {
        SenderResidentId = senderResidentId;
        ReceiverManagertId = receiverManagerId;
        Subject = subject;
        Content = content;
    }
}
