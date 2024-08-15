using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.DeleteManager;

public record DeleteManagerRequest(string UserId) : IRequest<IResult>;

