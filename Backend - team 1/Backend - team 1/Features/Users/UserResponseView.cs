namespace Backend___team_1.Features.Users;

public class UserResponseView
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public List<string> Roles { get; set; }
}