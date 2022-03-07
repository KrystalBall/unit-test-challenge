using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public static class RoleHelper
{
    public static UserList GetUsers(UserList users, int systemRoleID)
    {
        var roleUsers = users.Users.Where(x => x.SystemRole.SystemRoleID == systemRoleID).ToList();
        return new()
        {
            Users = roleUsers,
            TotalCount = roleUsers.Count,
        };
    }
}
