using Backend___team_1.Database;
using Microsoft.AspNetCore.Mvc;

namespace Backend___team_1.Features.Teams;

[ApiController]
[Route("api/teams")]
public class TeamsController : Controller
{
    private readonly AppDbContext _dbContext;

    public TeamsController(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }
    
    
}