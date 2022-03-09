using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public class RoleAndOrganizationHelper
{
    public static UserList GetUsers(UserList users, int organizationID, int systemRoleID)
    {
        if (users == null)
            throw new ArgumentNullException("Unable to find users.");

        var organizationUsers = users.Users.Where(x => x.Organization.OrganizationID == organizationID).ToList();
        var organizationRoleUsers = organizationUsers.Where(x => x.SystemRole.SystemRoleID == systemRoleID).ToList();
        return new()
        {
            Users = organizationRoleUsers,
        };
    }
}
