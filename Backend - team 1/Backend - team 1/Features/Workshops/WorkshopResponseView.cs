using Backend___team_1.Features.Users;

namespace Backend___team_1.Features.Workshops;

public class WorkshopResponseView
{
    public string Id { get; set; }
    
    public UserResponseView Trainer { get; set; }
    
    public string Topic { get; set; }
    
    public string Description { get; set; }
    
    public string Coverpath { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public int Capacity { get; set; }
    
    public int MaxCapacity { get; set; }
    
    public string Location { get; set; }
    
    public UserResponseView[] Participants { get; set; }
    
    public string PresentationPath { get; set; }
}