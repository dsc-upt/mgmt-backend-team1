using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Properties.Database;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options) {}
    
    
}