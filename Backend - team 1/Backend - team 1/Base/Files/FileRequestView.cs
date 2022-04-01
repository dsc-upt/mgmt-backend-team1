using System.ComponentModel.DataAnnotations;

namespace Backend___team_1.Base.Files;

public class FileRequestView
{
    [Required]
    public string Name { get; set; }
    
    public string Path { get; set; }
    
    [Required]
    public string Extension { get; set; }
    
    [Required]
    public long Size { get; set; }
}