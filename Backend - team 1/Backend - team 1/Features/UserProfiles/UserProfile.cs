using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Backend___team_1.Base.Entities;
using Backend___team_1.Base.Files;
using Backend___team_1.Features.Users;
using Backend___team_1.Features.Teams;

namespace Backend___team_1.Features.UserProfiles;

public class UserProfile : Entity
{
    public User User { get; set; }
    
    public ICollection<Team> Teams { get; set; }
    
    public string? FacebookLink { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public FileModel? Photo { get; set; }
}