namespace CarRentWebAPI.Security
{
    public interface IJwIssuer
    {
        string IssueJwt(string role, int? id);
    }
}