using Backend___team_1.Base.Entities;

namespace Backend___team_1.Base.Files;

public class FileModel : Entity
{
    public string Name { get; set; }
    
    public string Path { get; set; }
    
    public string Extension { get; set; }

    public long Size { get; set; }
    
}