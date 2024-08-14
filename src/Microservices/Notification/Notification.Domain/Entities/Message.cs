using Shared.Core.Abstracts;

namespace Notification.Domain.Entities;

public class Message : Entity
{
    public string SenderManagerId { get; private set; }
    public string ReceiverResidentId { get; private set; }
    public string Subject { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public bool IsRead { get; private set; } = false;

    private Message() { }
    public Message(string senderManagerId, string receiverResidentId, string subject, string content)
    {
        SenderManagerId = senderManagerId;
        ReceiverResidentId = receiverResidentId;
        Subject = subject;
        Content = content;
    }
}
