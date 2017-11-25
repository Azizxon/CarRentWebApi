namespace CarRentWebAPI.Security
{
    public class Claims
    {
        public const string IClaim = "UserId";
        private string roleClaim;
        private string role;

        public static class Roles
        {
            public const string RoleClaim = "Role";
            public const string Admin = "Admin";
            public const string User = "User";

        }
    }
}
