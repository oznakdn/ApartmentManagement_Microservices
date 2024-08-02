using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notification.Application.Commands.CreateAnnouncement;
using Shared.Core.WebSocket.Methods;
using Shared.Core.WebSocket.Models;

namespace Notification.WebSocket.Hubs;

public class AnnouncementHub : Hub
{
    private readonly IMediator _mediator;

    public AnnouncementHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateAnnouncement(CreateAnnouncementModel createAnnouncement)
    {
        var result = await _mediator.Send(new CreateAnnouncementRequest(createAnnouncement.ManagerId, createAnnouncement.Title, createAnnouncement.Content));

        if (result.Success)
        {
            await Clients.Others.SendAsync(AnnouncementMethods.CREATED_ANNOUNCEMENT, createAnnouncement);
            await Clients.Caller.SendAsync(AnnouncementMethods.CREATED_ANNOUNCEMENT, result.Message);
        }
        else
        {
            await Clients.Caller.SendAsync(AnnouncementMethods.CREATED_ANNOUNCEMENT, result.Message);
        }
    }
}
