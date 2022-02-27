using Backend___team_1.Base.Entities;
using Backend___team_1.Features.UserProfiles;

namespace Backend___team_1.Features.Workshops;

public class Workshop : Entity
{
    public UserProfile Trainer { get; set; }
    
    public string Topic { get; set; }
    
    public string Description { get; set; }
    
    public string CoverImage { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public int Capacity { get; set; }
    
    public string Location { get; set; }
    
    public UserProfile[] Participants { get; set; }

    public string Presentation { get; set; }
}