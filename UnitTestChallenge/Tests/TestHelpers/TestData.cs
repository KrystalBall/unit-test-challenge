using AutoFixture;
using UnitTestChallenge.Models;

namespace UnitTestChallenge.Tests.TestHelpers;

public static class TestData
{
    public static IEnumerable<object[]> GetUserCountTestData()
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
