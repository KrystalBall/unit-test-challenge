using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public static class SingleUserHelper
{
    public static User GetUser(UserList users, int userID)
    {
        if (users == null)
            throw new ArgumentNullException("Unable to find user.");

        var user = users.Users.FirstOrDefault(x => x.UserID == userID);
        if (user == null)
            throw new Exception("User not found.");
        else
            return user;
    }
}
