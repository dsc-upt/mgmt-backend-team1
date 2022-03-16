using Backend___team_1.Database;
using Backend___team_1.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost]
    public async Task<ActionResult<TeamResponseView>> Post(TeamRequestView teamview)
    {
        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == teamview.TeamLeadId);
        if (teamLead == null)
        {
            return NotFound("Id not found in database");
        }

        var team = new Team
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = teamview.Name,
            GitHubLink = teamview.GitHubLink,
            TeamLeader = teamLead,
        };
        await _dbContext.Teams.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        return Ok(new TeamResponseView
        {
            Id = team.Id,
            Name = team.Name,
            GitHubLink = team.GitHubLink,
            TeamLead = new UserResponseView
            {
                Id = team.TeamLeader.Id,
                FirstName = team.TeamLeader.FirstName,
                LastName = team.TeamLeader.LastName,
                Email = team.TeamLeader.Email,
                Roles = team.TeamLeader.Roles,
            },
        });
    }

    [HttpGet]
    public async Task<ActionResult<List<TeamResponseView>>> Get()
    {
        return Ok(_dbContext.Teams.Select(
            team => new TeamResponseView
            {
                Id = team.Id,
                Name = team.Name,
                GitHubLink = team.GitHubLink,
                TeamLead = new UserResponseView
                {
                    Id = team.TeamLeader.Id,
                    FirstName = team.TeamLeader.FirstName,
                    LastName = team.TeamLeader.LastName,
                    Email = team.TeamLeader.Email,
                    Roles = team.TeamLeader.Roles,
                },
            }
        ).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamResponseView>> GetById([FromRoute] string id)
    {
        var team = await _dbContext.Teams.Include(team => team.TeamLeader).FirstOrDefaultAsync(entity => entity.Id == id);
        if (team == null)
        {
            return NotFound("Id not found in database");
        }

        return Ok(new TeamResponseView
        {
            Id = team.Id,
            Name = team.Name,
            GitHubLink = team.GitHubLink,
            TeamLead = new UserResponseView
            {
                Id = team.TeamLeader.Id,
                FirstName = team.TeamLeader.FirstName,
                LastName = team.TeamLeader.LastName,
                Email = team.TeamLeader.Email,
                Roles = team.TeamLeader.Roles,
            },
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TeamResponseView>> Delete([FromRoute] string id)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null)
        {
            return NotFound("Id not found in database");
        }

        var result = _dbContext.Teams.Remove(team);
        await _dbContext.SaveChangesAsync();
        return Ok(result.Entity);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TeamResponseView>> Update([FromBody]TeamRequestView teamview,[FromRoute] string id)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null)
        {
            return NotFound("Id not found in database");
        }
        team.Updated = DateTime.UtcNow;
        team.Name = teamview.Name;
        team.GitHubLink = teamview.GitHubLink;
        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == teamview.TeamLeadId);
        if (teamLead == null)
        {
            return NotFound("Id not found in database");
        }

        team.TeamLeader = teamLead;

        await _dbContext.SaveChangesAsync();

        return Ok(new TeamResponseView
        {
            Id = team.Id,
            Name = team.Name,
            GitHubLink = team.GitHubLink,
            TeamLead = new UserResponseView
            {
                Id = team.TeamLeader.Id,
                FirstName = team.TeamLeader.FirstName,
                LastName = team.TeamLeader.LastName,
                Email = team.TeamLeader.Email,
                Roles = team.TeamLeader.Roles,
            },
        });
    }

    [HttpPatch("{id}/{teamLeadId}")]
    public async Task<ActionResult<TeamResponseView>> ChangeTeamLead([FromRoute] string teamLeadId, [FromRoute] string id)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null)
        {
            return NotFound("Id not found in database");
        } 
        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == teamLeadId);
        if (teamLead == null)
        {
            return NotFound("Id not found in database");
        }

        team.TeamLeader = teamLead;
        await _dbContext.SaveChangesAsync();
        return Ok(new TeamResponseView
        {
            Id = team.Id,
            Name = team.Name,
            GitHubLink = team.GitHubLink,
            TeamLead = new UserResponseView
            {
                Id = team.TeamLeader.Id,
                FirstName = team.TeamLeader.FirstName,
                LastName = team.TeamLeader.LastName,
                Email = team.TeamLeader.Email,
                Roles = team.TeamLeader.Roles,
            },
        });
    }
}