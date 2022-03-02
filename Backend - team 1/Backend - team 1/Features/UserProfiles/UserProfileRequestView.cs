using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.UserProfiles;

public class UserProfileRequestView
{
    [Required]
    public string UserId { get; set; }
    
    
}