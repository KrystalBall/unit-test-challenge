using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;
using UnitTestChallenge.Tests.TestHelpers;

namespace UnitTestChallenge.Tests;

[TestClass]
public class RoleAndOrganizationHelperTests
{
    [TestMethod]
    public void ShouldGetCorrectOrganizationRoleUsers()
    {
        Fixture fixture = new();
        var users = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var expectedRoleId = users[0].SystemRole.SystemRoleID;
        var expectedOrganizationId = users[0].Organization.OrganizationID;

        UserList userList = new() { Users = users };

        var returnedUsers = RoleAndOrganizationHelper.GetUsers(userList, expectedOrganizationId, expectedRoleId);

        // Multiple asserts in one test usually isn't ideal, but I think a test like this calls for an exception.
        Assert.AreEqual(expectedRoleId, returnedUsers.Users[0].SystemRole.SystemRoleID);
        Assert.AreEqual(expectedOrganizationId, returnedUsers.Users[0].Organization.OrganizationID);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestData.GetUserCountTestData), typeof(TestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidOrganizationRoleUsers(List<User> users, int expectedCount)
    {
        UserList userList = new() { Users = users };

        var returnedUsers = RoleAndOrganizationHelper.GetUsers(userList, users[0].Organization.OrganizationID, users[0].SystemRole.SystemRoleID);

        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void ReturnEmptyListIfNoOrganizationRoleUserMatch()
    {
        Fixture fixture = new();
        var users = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var expectedRoleId = users[0].SystemRole.SystemRoleID;
        var expectedOrganizationId = users[1].Organization.OrganizationID;

        UserList userList = new() { Users = users };

        var returnedUsers = RoleAndOrganizationHelper.GetUsers(userList, expectedOrganizationId, expectedRoleId);

        Assert.AreEqual(0, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void OrganizationRoleUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<Exception>(() => RoleAndOrganizationHelper.GetUsers(userList, 1, 1));
    }

    [TestMethod]
    public void OrganizationRoleUserIncludesFirstName()
    {
        Fixture fixture = new();
        var users = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var roleId = users[0].SystemRole.SystemRoleID;
        var organizationId = users[0].Organization.OrganizationID;
        var expectedFirstName = users[0].FirstName;

        UserList userList = new() { Users = users };

        var returnedUsers = RoleAndOrganizationHelper.GetUsers(userList, organizationId, roleId);

        Assert.AreEqual(expectedFirstName, returnedUsers.Users[0].FirstName);

        // I would make an additional test for each property that is critical for success, based on business demands. That could be all of them.
        // Since this is only an excerise, I'd like to save time and leave it at this comment.
    }
}
