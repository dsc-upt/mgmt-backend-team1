using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.Teams;

public class TeamRequestView
{
    [Required]
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
    
    [Required]
    public string TeamLeadId { get; set; }
}