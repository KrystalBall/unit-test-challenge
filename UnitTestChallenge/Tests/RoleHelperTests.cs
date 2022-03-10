using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;
using UnitTestChallenge.Tests.TestHelpers;

namespace UnitTestChallenge.Tests;

[TestClass]
public class RoleHelperTests
{
    [TestMethod]
    [TestCategory("Routine")]
    public void ShouldGetCorrectRoleUsers()
    {
        Fixture fixture = new();
        var roleUsers = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var expectedId = roleUsers[0].SystemRole.SystemRoleID;

        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, expectedId);

        Assert.AreEqual(expectedId, returnedUsers.Users[0].SystemRole.SystemRoleID);
    }

    [DataTestMethod]
    [TestCategory("Routine")]
    [DynamicData(nameof(TestData.GetUserCountTestData), typeof(TestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidRoleUsers(List<User> roleUsers, int expectedCount)
    {
        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, roleUsers[0].SystemRole.SystemRoleID);

        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    [TestCategory("Routine")]
    public void ReturnEmptyListIfNoRoleUserMatch()
    {
        Fixture fixture = new();
        var role = fixture.Build<SystemRole>().With(r => r.SystemRoleID, 1).Create();
        var roleUsers = fixture
            .Build<User>()
            .With(r => r.SystemRole, role)
            .CreateMany()
            .ToList();

        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, 2);

        Assert.AreEqual(0, returnedUsers.Users.Count);
    }

    [TestMethod]
    [TestCategory("LowPriority")]
    public void RoleUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<Exception>(() => RoleHelper.GetUsers(userList, 1));
    }

    [TestMethod]
    [TestCategory("Routine")]
    public void RoleUserIncludesFirstName()
    {
        Fixture fixture = new();
        var roleUsers = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var roleId = roleUsers[0].SystemRole.SystemRoleID;
        var expectedFirstName = roleUsers[0].FirstName;

        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, roleId);

        Assert.AreEqual(expectedFirstName, returnedUsers.Users[0].FirstName);

        // I would make an additional test for each property that is critical for success, based on business demands. That could be all of them.
        // Since this is only an excerise, I'd like to save time and leave it at this comment.
    }
}
