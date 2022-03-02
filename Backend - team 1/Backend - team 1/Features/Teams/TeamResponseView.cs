using Backend___team_1.Features.UserProfiles;

namespace Backend___team_1.Features.Teams;

public class TeamResponseView
{
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
    
    public UserProfile TeamLead { get; set; }
}