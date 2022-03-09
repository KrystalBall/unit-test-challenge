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
        try
        {
            return Ok(SingleUserHelper.GetUser(ReadSampleJson(), userID));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("organization/{organizationID}"), AcceptVerbs("GET")]
    public IActionResult GetUsers(int organizationID)
    {
        try
        {
            return Ok(OrganizationUserHelper.GetUsers(ReadSampleJson(), organizationID));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Route("role/{systemRoleID}"), AcceptVerbs("GET")]
    public IActionResult GetUsersByRole(int systemRoleID)
    {
        try
        {
            return Ok(RoleHelper.GetUsers(ReadSampleJson(), systemRoleID));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Route("organization/{organizationID}/role/{systemRoleID}"), AcceptVerbs("GET")]
    public IActionResult GetOrganizationUsersByRole(int organizationID, int systemRoleID)
    {
        try
        {
            return Ok(RoleAndOrganizationHelper.GetUsers(ReadSampleJson(), organizationID, systemRoleID));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    private static UserList ReadSampleJson()
    {
        return JsonConvert.DeserializeObject<UserList>(System.IO.File.ReadAllText("./sample-data.json"));
    }
}
