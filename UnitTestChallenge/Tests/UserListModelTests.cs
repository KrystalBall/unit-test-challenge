using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Models;

namespace UnitTestChallenge.Tests;

[TestClass]
public class UserListModelTests
{
    [TestMethod]
    [TestCategory("LowPriority")]

    public void UserListTotalCountIsZeroWhenUsersNull()
    {
        Assert.AreEqual(0, new UserList().TotalCount);
    }

    [TestMethod]
    [TestCategory("LowPriority")]
    public void UserListTotalCountReturnsUsersCount()
    {
        Fixture fixture = new();
        var users = fixture
            .Build<User>()
            .CreateMany()
            .ToList();

        UserList userList = new() { Users = users };

        Assert.AreEqual(userList.Users.Count, userList.TotalCount);
    }
}
