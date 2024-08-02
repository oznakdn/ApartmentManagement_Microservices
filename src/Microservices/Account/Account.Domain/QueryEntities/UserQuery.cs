using Shared.Core.Abstracts;

namespace Account.Domain.QueryEntities;

public class UserQuery : Entity
{
    public string UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public string? Picture { get; private set; }
    public bool IsManager { get; private set; }
    public bool IsResident { get; private set; }
    public bool IsEmployee { get; private set; }

    private UserQuery() { }

    public UserQuery(string userId, string firstName, string lastName, string address, string phoneNumber, string email, string? picture)
    {
        Id = Ulid.NewUlid().ToString();
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Picture = picture;

    }

    public UserQuery(string userId, string firstName, string lastName, string address, string phoneNumber, string email, string? picture, bool? isManager, bool? isResident, bool? isEmployee)
    {
        Id = Ulid.NewUlid().ToString();
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Picture = picture;
        IsManager = isManager ?? false;
        IsResident = isResident ?? false;
        IsEmployee = isEmployee ?? false;

    }

    public bool UploadPicture(string picture)
    {
        if (string.IsNullOrWhiteSpace(picture))
            return false;
        Picture = picture;
        return true;
    }

    public bool ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            return false;
        Email = newEmail;
        return true;
    }

    public void AssignManager()
    {
        IsManager = true;
    }

    public void AssignResident()
    {
        IsResident = true;
    }

    public void AssignEmployee()
    {
        IsEmployee = true;
    }


}
