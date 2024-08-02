using FluentValidation;

namespace Notification.Application.Commands.CreateAnnouncement;

public class CreateAnnouncementValidator : AbstractValidator<CreateAnnouncementRequest>
{
    public CreateAnnouncementValidator()
    {
        RuleFor(x => x.ManagerId).NotEmpty().NotNull();
        RuleFor(x => x.Title).NotEmpty().NotNull().Length(5,20);
        RuleFor(x => x.Content).NotEmpty().NotNull().Length(10,50);
    }
}
