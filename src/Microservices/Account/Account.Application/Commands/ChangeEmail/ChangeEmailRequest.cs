using MediatR;
using Shared.Core.Interfaces;


namespace Account.Application.Commands.ChangeEmail;

public record ChangeEmailRequest(string CurrentEmail, string NewEmail) : IRequest<IResult>;
