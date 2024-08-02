using FluentValidation;
using MediatR;
using Notification.Domain.Entities;
using Notification.Infrastructure.Contexts;

namespace Notification.Application.Commands.CreateAnnouncement;

public class CreateAnnouncementHandler : IRequestHandler<CreateAnnouncementRequest, CreateAnnouncementResponse>
{
    private readonly MongoDbContext<Announcement> _dbContext;
    private readonly IValidator<CreateAnnouncementRequest> _validator;

    public CreateAnnouncementHandler(IValidator<CreateAnnouncementRequest> validator, MongoDbContext<Announcement> dbContext)
    {
        _validator = validator;
        _dbContext = dbContext;
    }

    public async Task<CreateAnnouncementResponse> Handle(CreateAnnouncementRequest request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            return new CreateAnnouncementResponse(false, Errors: validator.Errors.Select(x => x.ErrorMessage).ToArray());

        var announcement = new Announcement(request.ManagerId, request.Title, request.Content);
        await _dbContext.AddAsync(announcement);
        return new CreateAnnouncementResponse(true,Message:"Announcement created successfully");
    }
}
