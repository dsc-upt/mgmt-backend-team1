using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.Clients;

public class ClientRequestView
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ContactPersonId { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
}