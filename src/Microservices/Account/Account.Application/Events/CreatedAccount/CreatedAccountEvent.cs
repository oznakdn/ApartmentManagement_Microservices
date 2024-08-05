using MediatR;

namespace Account.Application.Events.CreatedAccount;

public record CreatedAccountEvent(string Id, string FirstName, string LastName, string Address, string PhoneNumber, string Email, string? Picture, bool? IsManager, bool? IsEmployee, bool? IsResident) : INotification;
