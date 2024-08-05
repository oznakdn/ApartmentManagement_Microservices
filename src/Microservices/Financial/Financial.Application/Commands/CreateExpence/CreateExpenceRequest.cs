using MediatR;

namespace Financial.Application.Commands.CreateExpence;

public record CreateExpenceRequest(string Title, string Description, decimal TotalAmount) : IRequest<CreateExpenceResponse>;

