using Shared.Core.Abstracts;

namespace Notification.Domain.Entities;

public class Announcement : Entity
{
    public string SenderManagerId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime PublicationDate { get; private set; } = DateTime.Now;
    public bool IsActive { get; private set; }

    private Announcement() { }

    public Announcement(string senderManagerId, string title, string content)
    {
        SenderManagerId = senderManagerId;
        Title = title;
        Content = content;
    }
}
