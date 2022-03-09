﻿using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;
using UnitTestChallenge.Tests.TestHelpers;

namespace UnitTestChallenge.Tests;

[TestClass]
public class RoleHelperTests
{
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
    [DynamicData(nameof(TestData.GetUserCountTestData), typeof(TestData), DynamicDataSourceType.Method)]
    public void ShouldGetAllValidRoleUsers(List<User> roleUsers, int expectedCount)
    {
        UserList userList = new() { Users = roleUsers };

        var returnedUsers = RoleHelper.GetUsers(userList, roleUsers[0].SystemRole.SystemRoleID);

        Assert.AreEqual(expectedCount, returnedUsers.Users.Count);
    }

    [TestMethod]
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
    public void RoleUserShouldErrorIfParameterIsNull()
    {
        UserList userList = null;
        Assert.ThrowsException<Exception>(() => RoleHelper.GetUsers(userList, 1));
    }
}
