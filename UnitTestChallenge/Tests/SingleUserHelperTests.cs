using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;

namespace UnitTestChallenge.Tests;

[TestClass]
public class SingleUserHelperTests
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
    public void ShouldErrorIfNoSpecifiedUserMatch()
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
        Assert.ThrowsException<Exception>(() => SingleUserHelper.GetUser(userList, 1));
    }
}
