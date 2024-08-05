using Microsoft.AspNetCore.Identity;

namespace Account.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Address { get; private set; }
    public string? Picture { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpIn { get; private set; }

    public bool IsManager { get; private set; }
    public bool IsResident { get; private set; }
    public bool IsEmployee { get; private set; }

    public string? SiteId { get; private set; }
    public string? UnitId { get; private set; }


    private User() { }

    public User(string firstName, string lastName, string address, string phoneNumber, string email, string password, bool? isManager, bool? isResident, bool? isEmployee)
    {
        Id = Ulid.NewUlid().ToString();
        FirstName = firstName;
        LastName = lastName;
        UserName = email;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = password;
        IsManager = isManager ?? false;
        IsResident = isResident ?? false;
        IsEmployee = isEmployee ?? false;
    }

    public User(string firstName, string lastName, string address, string phoneNumber, string email, string password, string? siteId, string? unitId, bool? isManager, bool? isResident, bool? isEmployee)
    {
        Id = Ulid.NewUlid().ToString();
        FirstName = firstName;
        LastName = lastName;
        UserName = email;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = password;
        SiteId = siteId;
        UnitId = unitId;
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

    public bool ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        Email = email;
        return true;
    }

    public bool ChangePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;
        PasswordHash = password;
        return true;
    }

    public bool ChangePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;
        PhoneNumber = phoneNumber;
        return true;
    }

    public void AssignSite(string siteId)
    {
        SiteId = siteId;
    }

    public void AssignUnit(string unitId)
    {
        UnitId = unitId;
    }


    public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpIn)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpIn = refreshTokenExpIn;
    }

    public void RemoveRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpIn = null;
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
