using Account.Application.Queries.GetManagers;
using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetManagerById;

public record GetManagerByIdRequest(string UserId) : IRequest<IResult<GetManagersResponse>>;

