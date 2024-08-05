using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Events.UploadedPhoto;

public class UploadedPhotoEventHandler : INotificationHandler<UploadedPhotoEvent>
{
    private readonly QueryDbContext _dbContext;

    public UploadedPhotoEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UploadedPhotoEvent args, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == args.UserId, cancellationToken);

        account!.UploadPicture(args.PictureUrl);

        _dbContext.Users.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
