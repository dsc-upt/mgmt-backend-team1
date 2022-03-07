using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.Projects;

public class ProjectRequestView
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ManagerId { get; set; }

    [Required]
    public string Status { get; set; }
    
    [Required]
    public string ClientId { get; set; }
    
    [Required]
    public ICollection<string> TeamsIds { get; set; }
}