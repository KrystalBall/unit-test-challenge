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
    public void ShouldGetOrganizationUsers()
    {
        //Use Fixture to create a list of users in many organizations 
        //Then call OrganizationUserHelper.GetUsers(userList, organizationID)
        //Test needs to verify that only users in the specified organization are returned
    }

    [TestMethod]
    public void ShouldGetRoleUsers()
    {
        //Use Fixture to create a list of users with different roles 
        //Then call RoleHelper.GetUsers(userList, roleID)
        //Test needs to verify that only users in the specified role are returned
    }

    [TestMethod]
    public void ShouldGetOrganizationRoleUsers()
    {
        //Use Fixture to create a list of users with the following specs
        //  many organizations
        //  many roles
        //  role ids must be the same across organizations 
        //Then call RoleHelper.GetUsers(userList, roleID)
        //Test needs to verify that only users in the specified role and organization are returned
    }

    //Based on your experience and analysis of the models, are there any other tests you feel would be useful?
    //If so create them here.
}
