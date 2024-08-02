using MediatR;

namespace Account.Application.Notifications.CreatedAccount;

public record CreatedAccountEvent(string UserId, string FirstName, string LastName, string Address, string PhoneNumber, string Email, string? Picture, bool? IsManager, bool? IsEmployee, bool? IsResident) : INotification;
