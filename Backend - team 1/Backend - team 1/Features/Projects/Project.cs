using Backend___team_1.Base.Entities;
using Backend___team_1.Features.Clients;
using Backend___team_1.Features.Teams;
using Backend___team_1.Features.UserProfiles;

namespace Backend___team_1.Features.Projects;

public class Project : Entity
{
    public string Name { get; set; }

    public UserProfile Manager { get; set; }
    
    public string Status { get; set; }
    
    public Client Client { get; set; }
    
    public ICollection<Team> Teams { get; set; }
}