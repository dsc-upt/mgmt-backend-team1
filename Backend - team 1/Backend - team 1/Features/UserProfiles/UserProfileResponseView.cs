using Backend___team_1.Features.Teams;
using Backend___team_1.Features.Users;

namespace Backend___team_1.Features.UserProfiles;

public class UserProfileResponseView
{
    public string Id { get; set; }
    
    public UserResponseView User { get; set; }
    
    public ICollection<TeamResponseView> TeamList { get; set; }
    
    public string Phone { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string PhotoPath { get; set; }
    
    public string FacebookLink { get; set; }
}