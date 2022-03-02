using Backend___team_1.Features.Clients;
using Backend___team_1.Features.Projects;
using Backend___team_1.Features.Teams;
using Backend___team_1.Features.UserProfiles;
using Backend___team_1.Features.Users;
using Backend___team_1.Features.Workshops;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Database;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<Client> Clients { get; set; }
    
    public DbSet<Project> Projects { get; set; } 
    
    public DbSet<Workshop> Workshops { get; set; }
}