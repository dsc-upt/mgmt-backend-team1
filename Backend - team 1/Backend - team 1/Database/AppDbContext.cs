using Backend___team_1.Features.Teams;
using Backend___team_1.Features.UserProfiles;
using Backend___team_1.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Features.Database;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
}