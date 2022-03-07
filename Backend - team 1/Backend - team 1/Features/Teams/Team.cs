using Backend___team_1.Base.Entities;
using Backend___team_1.Features.Users;

namespace Backend___team_1.Features.Teams;

public class Team : Entity
{
    public User TeamLeader { get; set; }
    
    public string Name { get; set; }
    
    public string? GitHubLink { get; set; }
}