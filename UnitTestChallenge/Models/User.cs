namespace UnitTestChallenge.Models;

public class User
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public Organization Organization { get; set; }
    public SystemRole SystemRole { get; set; }
}
