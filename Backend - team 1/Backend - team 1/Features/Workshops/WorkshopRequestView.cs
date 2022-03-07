using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Features.Workshops;

public class WorkshopRequestView
{
    [Required]
    public string TrainerId { get; set; }
    
    [Required]
    public string Topic { get; set; }
    
    public string Description { get; set; }
    
    public string CoverId { get; set; }
    
    [Required]
    public DateTime DateStart { get; set; }
    
    [Required]
    public DateTime DateEnd { get; set; }
    
    [Required]
    public int MaxCapacity { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    public string[] UsersIds { get; set; }
    
    public string PresentationId { get; set; }
}