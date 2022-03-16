using Backend___team_1.Database;
using Backend___team_1.Features.Teams;
using Backend___team_1.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Features.UserProfiles;

[ApiController]
[Route("api/userprofile")]
public class UserProfilesController : Controller
{
    private readonly AppDbContext _dbContext;

    public UserProfilesController(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserProfileResponseView>> Post(UserProfileRequestView upview)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == upview.UserId);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }
        var photo = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == upview.PhotoId);
        if (photo == null)
        {
            return NotFound("Id not found in database");
        }

        var teams = new List<Team>();
        foreach (string t in upview.TeamsIds)
        {
            var tm = await _dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == t);
            if (tm == null)
            {
                return NotFound("Id not found in database");
            }
            teams.Add(tm);
        }

        var userprofile = new UserProfile
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            User = user,
            Birthday = upview.Birthday,
            FacebookLink = upview.FacebookLink,
            Phone = upview.Phone,
            Photo = photo,
            Teams = teams,
        };

        await _dbContext.UserProfiles.AddAsync(userprofile);
        await _dbContext.SaveChangesAsync();

        var teamsview = new List<TeamResponseView>();
        foreach (var t in userprofile.Teams)
        {
            var tview = new TeamResponseView
            {
                Id = t.Id,
                Name = t.Name,
                GitHubLink = t.GitHubLink,
                TeamLead = new UserResponseView
                {
                    Id = t.TeamLeader.Id,
                    FirstName = t.TeamLeader.FirstName,
                    LastName = t.TeamLeader.LastName,
                    Email = t.TeamLeader.Email,
                    Roles = t.TeamLeader.Roles,
                },
            };
            teamsview.Add(tview);
        }

        return Ok(new UserProfileResponseView
        {
            Id = userprofile.Id,
            User = new UserResponseView
            {
                Id = userprofile.User.Id,
                FirstName = userprofile.User.FirstName,
                LastName = userprofile.User.LastName,
                Email = userprofile.User.Email,
                Roles = userprofile.User.Roles,
            },
            Birthday = userprofile.Birthday,
            Phone = userprofile.Phone,
            FacebookLink = userprofile.FacebookLink,
            PhotoPath = userprofile.Photo.Path,
            TeamList = teamsview,
        });
    }

    [HttpGet]
    public async Task<ActionResult<UserProfileResponseView>> Get()
    {

        return Ok(_dbContext.UserProfiles.Select(
            userp => new UserProfileResponseView
            {
                Id = userp.Id,
                User = new UserResponseView
                {
                    FirstName = userp.User.FirstName,
                    LastName = userp.User.LastName,
                    Email = userp.User.Email,
                    Roles = userp.User.Roles,
                },
                Birthday = userp.Birthday,
                FacebookLink = userp.FacebookLink,
                Phone = userp.Phone,
                PhotoPath = userp.Photo.Path,
                TeamList = userp.Teams.Select(
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
                    ).ToList(),
            }
        ));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserProfileResponseView>> GetById([FromRoute] string id)
    {
        var userprofile = await _dbContext.UserProfiles.FirstOrDefaultAsync(entity => entity.Id == id);
        if (userprofile == null)
        {
            return NotFound("Id not found in database");
        }
        
        var teamsview = new List<TeamResponseView>();
        foreach (var t in userprofile.Teams)
        {
            var tview = new TeamResponseView
            {
                Id = t.Id,
                Name = t.Name,
                GitHubLink = t.GitHubLink,
                TeamLead = new UserResponseView
                {
                    Id = t.TeamLeader.Id,
                    FirstName = t.TeamLeader.FirstName,
                    LastName = t.TeamLeader.LastName,
                    Email = t.TeamLeader.Email,
                    Roles = t.TeamLeader.Roles,
                },
            };
            teamsview.Add(tview);
        }
        
        return Ok(new UserProfileResponseView()
        {
            Id = userprofile.Id,
            User = new UserResponseView
            {
                Id = userprofile.User.Id,
                FirstName = userprofile.User.FirstName,
                LastName = userprofile.User.LastName,
                Email = userprofile.User.Email,
                Roles = userprofile.User.Roles,
            },
            Birthday = userprofile.Birthday,
            Phone = userprofile.Phone,
            FacebookLink = userprofile.FacebookLink,
            PhotoPath = userprofile.Photo.Path,
            TeamList = teamsview,
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserProfileResponseView>> Delete([FromRoute] string id)
    {
        var userprofile = await _dbContext.UserProfiles.FirstOrDefaultAsync(entity => entity.Id == id);
        if (userprofile == null)
        {
            return NotFound("Id not found in database");
        }

        _dbContext.UserProfiles.Remove(userprofile);
        await _dbContext.SaveChangesAsync();
        
        var teamsview = new List<TeamResponseView>();
        foreach (var t in userprofile.Teams)
        {
            var tview = new TeamResponseView
            {
                Id = t.Id,
                Name = t.Name,
                GitHubLink = t.GitHubLink,
                TeamLead = new UserResponseView
                {
                    Id = t.TeamLeader.Id,
                    FirstName = t.TeamLeader.FirstName,
                    LastName = t.TeamLeader.LastName,
                    Email = t.TeamLeader.Email,
                    Roles = t.TeamLeader.Roles,
                },
            };
            teamsview.Add(tview);
        }
        
        return Ok(new UserProfileResponseView()
        {
            Id = userprofile.Id,
            User = new UserResponseView
            {
                Id = userprofile.User.Id,
                FirstName = userprofile.User.FirstName,
                LastName = userprofile.User.LastName,
                Email = userprofile.User.Email,
                Roles = userprofile.User.Roles,
            },
            Birthday = userprofile.Birthday,
            Phone = userprofile.Phone,
            FacebookLink = userprofile.FacebookLink,
            PhotoPath = userprofile.Photo.Path,
            TeamList = teamsview,
        });
    }
}