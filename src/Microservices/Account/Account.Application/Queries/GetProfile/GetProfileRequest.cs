using MediatR;

namespace Account.Application.Queries.GetProfile;

public record GetProfileRequest(string UserId) : IRequest<GetProfileResponse>;
