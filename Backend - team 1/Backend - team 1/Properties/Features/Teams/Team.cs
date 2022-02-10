using Backend___team_1.Properties.Features.Entities;
using Backend___team_1.Properties.Features.Users;

namespace Backend___team_1.Properties.Features.Teams;

public class Team : Entity
{
    public User TeamLeader { get; set; }
    
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
}