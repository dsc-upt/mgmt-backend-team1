using Backend___team_1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Features.Users;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly  AppDbContext _appDbContext;

    public UsersController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseView>> Add([FromBody]UserRequestView userRequest)
    {
        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            Roles = userRequest.Roles,
        };
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();

        return Ok(new UserResponseView
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }

    [HttpGet]
    public async Task<ActionResult<UserResponseView>> Get()
    {
        return Ok(_appDbContext.Users.Select(
            user => new UserResponseView
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles,
            }
        ).ToList());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseView>> GetById([FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }

        return Ok(new UserResponseView
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete([FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }

        var result = _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
        return Ok(result.Entity);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UserResponseView>> Update([FromBody]UserRequestView userview, [FromRoute]string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }
        
        user.FirstName = userview.FirstName;
        user.LastName = userview.LastName;
        user.Email = userview.Email;
        user.Roles = userview.Roles;
        user.Updated = DateTime.UtcNow;

        await _appDbContext.SaveChangesAsync();

        return Ok(new UserResponseView
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }

    [HttpPost("{id}/{role}")]
    public async Task<ActionResult<UserResponseView>> AddRole([FromRoute] string role, [FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }
        user.Roles.Add(role);
        await _appDbContext.SaveChangesAsync();
        return Ok(new UserResponseView
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }
    
    [HttpDelete("{id}/{role}")]
    public async Task<ActionResult<UserResponseView>> RemoveRole([FromRoute] string role, [FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found in database");
        }

        var ok = user.Roles.Find(entity => entity == role);
        if (ok == null)
        {
            throw new ArgumentException("This user does no have the {0} role", role);
        }
        user.Roles.Remove(role);
        await _appDbContext.SaveChangesAsync();
        return Ok(new UserResponseView
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }
}