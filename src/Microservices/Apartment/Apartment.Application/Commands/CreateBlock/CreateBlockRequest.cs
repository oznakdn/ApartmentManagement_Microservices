using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.CreateBlock;

public record CreateBlockRequest(string Name, string SiteId, int TotalUnits) : IRequest<IResult>;

