using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreateExpence;

public record CreateExpenceRequest(string SiteId, string Title, string Description, decimal TotalAmount) : IRequest<IResult>;

