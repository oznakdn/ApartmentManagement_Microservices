using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetProfile;

public record GetProfileRequest(string UserId) : IRequest<IResult<GetProfileResponse>>;
