using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public static class RoleHelper
{
    public static UserList GetUsers(UserList users, int systemRoleID)
    {
        if (users == null)
            throw new Exception("Unable to find users.");

        var roleUsers = users.Users.Where(x => x.SystemRole.SystemRoleID == systemRoleID).ToList();
        return new()
        {
            Users = roleUsers,
        };
    }
}
