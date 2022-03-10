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
}