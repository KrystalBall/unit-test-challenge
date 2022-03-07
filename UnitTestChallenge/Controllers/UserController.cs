using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UnitTestChallenge.Helpers;
using UnitTestChallenge.Models;

namespace UnitTestChallenge.Controllers;
[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    [Route("{userID}"), AcceptVerbs("GET")]
    public IActionResult GetUser(int userID)
    {
        return Ok(SingleUserHelper.GetUser(ReadSampleJson(), userID));
    }

    [Route("organization/{organizationID}"), AcceptVerbs("GET")]
    public IActionResult GetUsers(int organizationID)
    {
        return Ok(OrganizationUserHelper.GetUsers(ReadSampleJson(), organizationID));
    }

    [Route("role/{systemRoleID}"), AcceptVerbs("GET")]
    public IActionResult GetUsersByRole(int systemRoleID)
    {
        return Ok(RoleHelper.GetUsers(ReadSampleJson(), systemRoleID));
    }

    [Route("organization/{organizationID}/role/{systemRoleID}"), AcceptVerbs("GET")]
    public IActionResult GetOrganizationUsersByRole(int organizationID, int systemRoleID)
    {
        return Ok(RoleAndOrganizationHelper.GetUsers(ReadSampleJson(), organizationID, systemRoleID));
    }

    private static UserList ReadSampleJson()
    {
        return JsonConvert.DeserializeObject<UserList>(System.IO.File.ReadAllText("./sample-data.json"));
    }
}
