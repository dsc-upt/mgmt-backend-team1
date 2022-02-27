using Backend___team_1.Base.Entities;
using Backend___team_1.Features.UserProfiles;

namespace Backend___team_1.Features.Teams;

public class Team : Entity
{
    public UserProfile TeamLeader { get; set; }
    
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
}