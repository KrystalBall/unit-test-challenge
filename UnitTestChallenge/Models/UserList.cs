namespace UnitTestChallenge.Models;

public class UserList
{
    public List<User> Users { get; set; }
    public int TotalCount
    {
        // Normally I'd discuss with a developer first. This way seems DRYer and requires fewer unit tests for coverage.
        get
        {
            if (Users == null)
                return 0;
            else
                return Users.Count;
        }
    }
}
