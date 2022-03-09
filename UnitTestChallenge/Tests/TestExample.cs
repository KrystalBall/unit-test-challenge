using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;

namespace UnitTestChallenge.Tests;

[TestClass]
public class TestExample
{
    [TestMethod]
    public void ShouldGetSpecifiedUser()
    {
        Fixture fixture = new();
        var organization = fixture.Build<Organization>().With(o => o.OrganizationID, 1).Create();
        var organizationUsers = fixture
            .Build<User>()
            .With(o => o.Organization, organization)
            .CreateMany()
            .ToList();

        var userToFind = organizationUsers.First();

        UserList userList = new() { Users = organizationUsers };

        var testUser = SingleUserHelper.GetUser(userList, userToFind.UserID);

        Assert.AreEqual(userToFind.UserID, testUser.UserID);
    }

    [TestMethod]
    public void ShouldErrorIfNoSpecifiedUser()
    {
        Fixture fixture = new();
        var organization = fixture.Build<Organization>().With(o => o.OrganizationID, 1).Create();
        var organizationUsers = fixture
            .Build<User>()
            .With(o => o.Organization, organization)
            .CreateMany()
            .ToList();

        var userToFind = organizationUsers.First();

        UserList userList = new() { Users = organizationUsers };

        // Let those silly humans care about the error message. Just check the exception type so that the test is less brittle.
        Assert.ThrowsException<Exception>(() => SingleUserHelper.GetUser(userList, 2));
    }

    [TestMethod]
    public void SpecifiedUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<ArgumentNullException>(() => SingleUserHelper.GetUser(userList, 1));
    }

    [TestMethod]
    public void ShouldGetCorrectOrganizationUser()
    {
        // I decided to double check AutoFixture's trustworthiness for random, unique IDs between multiple instances of complex objects.
        // The fruits of my labor bear a comment. Huzzah!
        // A good starting point for code-gazing for the curious: https://github.com/AutoFixture/AutoFixture/blob/730b47884975d0c1256209073a719a9fe510d8a9/Src/AutoFixture/RandomNumericSequenceGenerator.cs#L142
        
        Fixture fixture = new();
        var organizationUsers = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var expectedId = organizationUsers[0].Organization.OrganizationID;

        UserList userList = new() { Users = organizationUsers };

        var returnedUsers = OrganizationUserHelper.GetUsers(userList, expectedId);

        Assert.AreEqual(expectedId, returnedUsers.Users[0].Organization.OrganizationID);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetUserCountTestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidOrganizationUsers(List<User> organizationUsers, int expectedCount)
    {
        UserList userList = new() { Users = organizationUsers };

        var returnedUsers = OrganizationUserHelper.GetUsers(userList, organizationUsers[0].Organization.OrganizationID);

        // TotalCount being accurate is a dependency. Looking at the list directly is arguably more reliable.
        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void ShouldGetNoOrganizationUserIfNoMatch()
    {
        Fixture fixture = new();
        var organization = fixture.Build<Organization>().With(o => o.OrganizationID, 1).Create();
        var organizationUsers = fixture
            .Build<User>()
            .With(o => o.Organization, organization)
            .CreateMany()
            .ToList();

        UserList userList = new() { Users = organizationUsers };

        var returnedUsers = OrganizationUserHelper.GetUsers(userList, 2);

        Assert.AreEqual(0, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void OrganizationUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<ArgumentNullException>(() => OrganizationUserHelper.GetUsers(userList, 1));
    }

    [TestMethod]
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
    [DynamicData(nameof(GetUserCountTestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidRoleUsers(List<User> roleUsers, int expectedCount)
    {
        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, roleUsers[0].SystemRole.SystemRoleID);

        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void ShouldGetNoRoleUserIfNoMatch()
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
    public void RoleUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<ArgumentNullException>(() => RoleHelper.GetUsers(userList, 1));
    }

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
    [DynamicData(nameof(GetUserCountTestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidOrganizationRoleUsers(List<User> users, int expectedCount)
    {
        UserList userList = new() { Users = users };

        var returnedUsers = RoleAndOrganizationHelper.GetUsers(userList, users[0].Organization.OrganizationID, users[0].SystemRole.SystemRoleID);

        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void ShouldGetNoOrganizationRoleUsersIfNoExactMatch()
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
        Assert.ThrowsException<ArgumentNullException>(() => RoleAndOrganizationHelper.GetUsers(userList, 1, 1));
    }

    [TestMethod]
    public void UserListTotalCountIsZeroWhenUsersNull()
    {
        Assert.AreEqual(0, new UserList().TotalCount);
    }

    [TestMethod]
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

    private static IEnumerable<object[]> GetUserCountTestData()
    {
        var fixture = new Fixture();
        var organization = fixture
            .Build<Organization>()
            .With(o => o.OrganizationID, 1)
            .Create();
        var role = fixture
            .Build<SystemRole>()
            .With(r => r.SystemRoleID, 1)
            .Create();

        yield return new object[] 
        {
            fixture
                .Build<User>()
                .With(o => o.Organization, organization)
                .With(r => r.SystemRole, role)
                .CreateMany(3)
                .ToList(), 
            3 
        };

        yield return new object[] 
        {
            fixture
                .Build<User>()
                .CreateMany(2)
                .ToList(), 
            1 
        };
    }
}
