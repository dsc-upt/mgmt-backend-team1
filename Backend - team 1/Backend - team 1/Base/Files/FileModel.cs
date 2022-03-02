using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Base.Files;

[Keyless]
[NotMapped]
public class FileModel
{
    public string Name { get; set; }
    
    public string Path { get; set; }
    
    public string Extension { get; set; }
    
    public long Size { get; set; }
}