using Backend___team_1.Database;
using Backend___team_1.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend___team_1.Features.Clients;

[ApiController]
[Route("api/clients")]
public class ClientsController : Controller
{
    private readonly AppDbContext _appdbcontext;

    public ClientsController(AppDbContext appdbcontext)
    {
        _appdbcontext = appdbcontext;
    }

    [HttpPost]
    public async Task<ActionResult<ClientResponseView>> Add([FromBody] ClientRequestView clientRequest)
    {
        var contact =
            await _appdbcontext.Users.FirstOrDefaultAsync(entity => entity.Id == clientRequest.ContactPersonId);
        if (contact == null)
        {
            return NotFound("Contact Person Id not found!");
        }

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            ContactPerson = contact,
            Email = clientRequest.Email,
            Name = clientRequest.Name,
            Phone = clientRequest.Phone,
        };

        await _appdbcontext.Clients.AddAsync(client);
        await _appdbcontext.SaveChangesAsync();

        return Ok(new ClientResponseView
        {
            Id = client.Id,
            Email = client.Email,
            Name = client.Name,
            Phone = client.Phone,
            ContactPerson = new UserResponseView
            {
                Email = client.ContactPerson.Email,
                FirstName = client.ContactPerson.FirstName,
                Id = client.ContactPerson.Id,
                LastName = client.ContactPerson.LastName,
                Roles = client.ContactPerson.Roles,
            },
        });
    }

    [HttpGet]
    public async Task<ActionResult<ClientResponseView>> Get()
    {
        return Ok(_appdbcontext.Clients.Select(
            client => new ClientResponseView
            {
                Id = client.Id,
                Email = client.Email,
                Name = client.Name,
                Phone = client.Phone,
                ContactPerson = new UserResponseView
                {
                    Email = client.ContactPerson.Email,
                    FirstName = client.ContactPerson.FirstName,
                    Id = client.ContactPerson.Id,
                    LastName = client.ContactPerson.LastName,
                    Roles = client.ContactPerson.Roles,
                }
            }
        ).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientResponseView>> GetById([FromRoute] string id)
    {
        var client = await _appdbcontext.Clients.Include(client => client.ContactPerson).FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null)
        {
            return NotFound("Client Id not found!");
        }

        return Ok(new ClientResponseView
        {
            Id = client.Id,
            Email = client.Email,
            Name = client.Name,
            Phone = client.Phone,
            ContactPerson = new UserResponseView
            {
                Email = client.ContactPerson.Email,
                FirstName = client.ContactPerson.FirstName,
                Id = client.ContactPerson.Id,
                LastName = client.ContactPerson.LastName,
                Roles = client.ContactPerson.Roles,
            },
        });
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ClientResponseView>> Update([FromBody] ClientRequestView clientview,
        [FromRoute] string id)
    {
        var client = await _appdbcontext.Clients.Include(client => client.ContactPerson).FirstOrDefaultAsync(client => client.Id == id);
        if (client == null)
        {
            return NotFound("Client is not found!");
        }

        var contactPerson =
            await _appdbcontext.Users.FirstOrDefaultAsync(client => client.Id == clientview.ContactPersonId);
        if (contactPerson == null)
        {
            return NotFound("Contact Person Id not found!");
        }

        client.Email = clientview.Email;
        client.Name = clientview.Name;
        client.Phone = clientview.Phone;
        client.Updated = DateTime.UtcNow;
        client.ContactPerson = contactPerson;

        await _appdbcontext.SaveChangesAsync();

        return Ok(new ClientResponseView
        {
            Email = client.Email,
            Id = client.Email,
            Name = client.Name,
            Phone = client.Phone,
            ContactPerson = new UserResponseView
            {
                Email = client.ContactPerson.Email,
                FirstName = client.ContactPerson.FirstName,
                Id = client.ContactPerson.Id,
                LastName = contactPerson.LastName,
                Roles = client.ContactPerson.Roles,
            },
        });
    }

    [HttpDelete]
    public async Task<ActionResult<ClientResponseView>> Delete([FromRoute] string id)
    {
        var client = await _appdbcontext.Clients.Include(client => client.ContactPerson).FirstOrDefaultAsync(client => client.Id == id);
        if (client == null)
        {
            return NotFound("Client id not found!");
        }

        var result = _appdbcontext.Clients.Remove(client);
        await _appdbcontext.SaveChangesAsync();
        return Ok(result.Entity);
    }

    [HttpPatch("{id}/{ContactPersonId}")]
    public async Task<ActionResult<ClientResponseView>> ChangeContactPerson([FromRoute] string id,
        [FromRoute] string contactPersonId)
    {
        var client = await _appdbcontext.Clients.Include(client => client.ContactPerson).FirstOrDefaultAsync(client => client.Id == id);
        if (client == null)
        {
            return NotFound("Client Id not found!");
        }

        var contactPerson = await _appdbcontext.Users.FirstOrDefaultAsync(contact => contact.Id == contactPersonId);
        if (contactPerson == null)
        {
            return NotFound("Contact Person id not found!");
        }

        client.ContactPerson = contactPerson;
        await _appdbcontext.SaveChangesAsync();

        return Ok(new ClientResponseView
        {
            Email = client.Email,
            Id = client.Id,
            Name = client.Email,
            Phone = client.Phone,
            ContactPerson = new UserResponseView
            {
                Email = client.ContactPerson.Email,
                FirstName = client.ContactPerson.FirstName,
                Id = client.ContactPerson.Id,
                LastName = client.ContactPerson.LastName,
                Roles = client.ContactPerson.Roles,
            },
        });
    }
}