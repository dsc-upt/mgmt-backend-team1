using System.ComponentModel.DataAnnotations;
using Backend___team_1.Base.Entities;
using Backend___team_1.Features.UserProfiles;

namespace Backend___team_1.Features.Clients;

public class Client : Entity
{
    public string Name { get; set; }
    
    public UserProfile ContactPerson { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    
}
