using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetManagers;

public record GetManagersRequest : IRequest<IResult<GetManagersResponse>>;

