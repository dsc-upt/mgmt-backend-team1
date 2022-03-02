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
    public async Task<UserResponseView> Add(UserRequestView userRequest)
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

        return new UserResponseView
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        };
    }

    [HttpGet]
    public async Task<List<UserResponseView>> Get()
    {
        return _appDbContext.Users.Select(
            user => new UserResponseView
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles,
            }
        ).ToList();
    }

    public async Task<UserResponseView> GetById([FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            throw new ArgumentException("Id not found in database.");
        }
        return new UserResponseView
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        };
    }

    [HttpDelete]
    public async Task<User> Delete([FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            throw new ArgumentException("Id not found in database.");
        }

        var result = _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    [HttpPatch]
    public async Task<UserResponseView> Update(UserRequestView userview, [FromRoute]string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == null)
        {
            throw new AggregateException("Id not found in databse");
        }

        user.FirstName = userview.FirstName;
        user.LastName = userview.LastName;
        user.Email = userview.Email;
        user.Roles = userview.Roles;
        user.Updated = DateTime.UtcNow;
        return new UserResponseView
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        };
    }

    [HttpPatch]
    public async Task<UserResponseView> AddRole([FromRoute] string role, [FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            throw new ArgumentException("Id not found in database");
        }
        user.Roles.Add(role);
        await _appDbContext.SaveChangesAsync();
        return new UserResponseView
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        };
    }
    
    [HttpPatch]
    public async Task<UserResponseView> RemoveRole([FromRoute] string role, [FromRoute] string id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            throw new ArgumentException("Id not found in database");
        }

        var ok = user.Roles.Find(entity => entity == role);
        if (ok == null)
        {
            throw new ArgumentException("This user does no have the {0} role", role);
        }
        user.Roles.Remove(role);
        await _appDbContext.SaveChangesAsync();
        return new UserResponseView
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        };
    }
}