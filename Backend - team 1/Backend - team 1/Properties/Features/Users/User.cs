using System.ComponentModel.DataAnnotations;
using Backend___team_1.Properties.Features.Entities;

namespace Backend___team_1.Properties.Features.Users;

public class User : Entity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public string Role { get; set; }
}