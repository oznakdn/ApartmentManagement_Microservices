namespace Client.WebAdmin;

public class Endpoints
{
    public static class Account
    {
        /// <summary>
        /// POST - BODY: Email, Password
        /// </summary>
        public const string Login = "https://localhost:7001/api/user/login";


        /// <summary>
        /// POST - BODY: Email, Password
        /// </summary>
        public const string RefreshLogin = "https://localhost:7001/api/user/refreshLogin";


        /// <summary>
        /// GET - URL PARAM: RefreshToken
        /// </summary>
        public const string Logout = "https://localhost:7001/api/user/logout";


        /// <summary>
        /// GET - URL PARAM: UserId
        /// </summary>
        public const string GetProfile = "https://localhost:7001/api/user/getProfile";


        /// <summary>
        /// GET - URL PARAM: UserId
        /// </summary>
        public const string GetAccountById = "https://localhost:7001/api/user/getAccountById";


        /// <summary>
        /// GET
        /// </summary>
        public const string GetAccounts = "https://localhost:7001/api/user/getAccounts";


        /// <summary>
        /// PUT - BODY: UserId, PictureUrl
        /// </summary>
        public const string UploadPhoto = "https://localhost:7001/api/user/uploadPhoto";


        /// <summary>
        /// PUT - BODY: UserId
        /// </summary>
        public const string MakeGuardToEmployee = "https://localhost:7001/api/user/makeGuardToEmployee";


        /// <summary>
        /// PUT - BODY: UserId,CurrentPassword,NewPassword
        /// </summary>
        public const string ChangePassword = "https://localhost:7001/api/user/changePassword";


        /// <summary>
        /// PUT - BODY: CurrentEmail,NewEmail
        /// </summary>
        public const string ChangeEmail = "https://localhost:7001/api/user/changeEmail";


        /// <summary>
        /// POST - BODY: FirstName, LastName, Email, Password, PhoneNumber, Address
        /// </summary>
        public const string ManagerRegister = "https://localhost:7001/api/user/managerRegister";


        /// <summary>
        /// GET
        /// </summary>
        public const string GetManagers = "https://localhost:7001/api/user/getManagers";

    }

    public static class Apartment
    {

        /// <summary>
        /// GET - URL PARAM: SiteId
        /// </summary>
        public const string GetSiteById = "https://localhost:7000/api/apartment/site/getSiteById";
    }


}
