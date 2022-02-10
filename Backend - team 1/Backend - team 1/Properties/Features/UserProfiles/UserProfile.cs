using System.ComponentModel.DataAnnotations;
using System.Net;
using Backend___team_1.Properties.Features.Entities;
using Backend___team_1.Properties.Features.Teams;
using Backend___team_1.Properties.Features.Users;

namespace Backend___team_1.Properties.Features.UserProfiles;

public class UserProfile : Entity
{
    public User User { get; set; }
    
    public ICollection<Team> Teams { get; set; }
    
    public string FacebookLink { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    public DateOnly Birthday { get; set; }
    
    public string Photo { get; set; }
}