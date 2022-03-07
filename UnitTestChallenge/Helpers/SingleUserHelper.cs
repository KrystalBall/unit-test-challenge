using UnitTestChallenge.Models;

namespace UnitTestChallenge.Helpers;

public static class SingleUserHelper
{
    public static User GetUser(UserList users, int userID)
    {
        //Refactor this to throw a custom error if no user is found.
        return users.Users.FirstOrDefault(x => x.UserID == userID);
    }
}
