using Backend___team_1.Features.Clients;
using Backend___team_1.Features.Teams;
using Backend___team_1.Features.Users;

namespace Backend___team_1.Features.Projects;

public class ProjectResponseView
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public UserResponseView Manager { get; set; }
    
    public string Status { get; set; }
    
    public ClientResponseView Client { get; set; }

    private ICollection<TeamResponseView> Teams { get; set; }
}