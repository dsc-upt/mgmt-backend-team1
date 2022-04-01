namespace Backend___team_1.Base.Entities;

public class Entity
{
    public string Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
}