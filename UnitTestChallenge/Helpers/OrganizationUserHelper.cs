using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public static class OrganizationUserHelper
{
    public static UserList GetUsers(UserList users, int organizarionID)
    {
        var organizationUsers = users.Users.Where(x => x.Organization.OrganizationID == organizarionID).ToList();
        return new()
        {
            Users = organizationUsers,
            TotalCount = organizationUsers.Count,
        };
    }
}
