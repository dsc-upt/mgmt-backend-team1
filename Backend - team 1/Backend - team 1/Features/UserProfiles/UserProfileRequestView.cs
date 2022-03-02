using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.UserProfiles;

public class UserProfileRequestView
{
    [Required]
    public string UserId { get; set; }
    
    public ICollection<string> TeamList { get; set; }
    
    public string FacebookLink { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
    
    [Required]
    public DateOnly Birthday { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
}