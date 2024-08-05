using Financial.Domain.Entities;
using MediatR;

namespace Financial.Application.Events.CreatedExpenceItems;

public record CreatedExpenceItemsEvent(List<ExpenceItem>ExpenceItems) : INotification;

