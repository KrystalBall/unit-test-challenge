using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;
using UnitTestChallenge.Tests.TestHelpers;

namespace UnitTestChallenge.Tests;

[TestClass]
public class OrganizationUserHelperTests
{
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
    [DynamicData(nameof(TestData.GetUserCountTestData), typeof(TestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidOrganizationUsers(List<User> organizationUsers, int expectedCount)
    {
        UserList userList = new() { Users = organizationUsers };

        var returnedUsers = OrganizationUserHelper.GetUsers(userList, organizationUsers[0].Organization.OrganizationID);

        // TotalCount being accurate is a dependency. Looking at the list directly is arguably more reliable.
        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
    public void ReturnEmptyListIfNoOrganizationUserMatch()
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
        Assert.ThrowsException<Exception>(() => OrganizationUserHelper.GetUsers(userList, 1));
    }

    [TestMethod]
    public void OrganizationUserIncludesFirstName()
    {
        Fixture fixture = new();
        var organizationUsers = fixture
            .Build<User>()
            .CreateMany(2)
            .ToList();

        var organizationId = organizationUsers[0].Organization.OrganizationID;
        var expectedName = organizationUsers[0].FirstName;

        UserList userList = new() { Users = organizationUsers };

        var returnedUsers = OrganizationUserHelper.GetUsers(userList, organizationId);

        Assert.AreEqual(expectedName, returnedUsers.Users[0].FirstName);

        // I would make an additional test for each property that is critical for success, based on business demands. That could be all of them.
        // Since this is only an excerise, I'd like to save time and leave it at this comment.
    }
}
