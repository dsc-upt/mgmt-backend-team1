using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.Users;

public class UserRequestView
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string Role { get; set; }
}