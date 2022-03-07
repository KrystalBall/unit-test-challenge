using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public class RoleAndOrganizationHelper
{
    public static UserList GetUsers(UserList users, int organizarionID, int systemRoleID)
    {
        var organizationUsers = users.Users.Where(x => x.Organization.OrganizationID == organizarionID).ToList();
        var organizationRoleUsers = organizationUsers.Where(x => x.SystemRole.SystemRoleID == systemRoleID).ToList();
        return new()
        {
            Users = organizationRoleUsers,
            TotalCount = organizationRoleUsers.Count,
        };
    }
}
