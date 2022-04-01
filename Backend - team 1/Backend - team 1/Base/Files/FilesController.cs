using Backend___team_1.Database;
using Backend___team_1.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Base.Files;

[ApiController]
[Route("api/files")]
public class FilesController : Controller
{
    private readonly AppDbContext _dbContext;

    public FilesController(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<FileResponseView>> Add(FileRequestView fileview)
    {
        var model = new FileModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = fileview.Name,
            Path = fileview.Path,
            Extension = fileview.Extension,
            Size = fileview.Size,
        };

        await _dbContext.Files.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return Ok(new FileResponseView
        {
            Id = model.Id,
            Name = model.Name,
            Path = model.Path,
            Extension = model.Extension,
            Size = model.Size,
        });
    }

    [HttpGet]
    public async Task<ActionResult<FileResponseView>> Get()
    {
        return Ok(_dbContext.Files.Select(
            file => new FileResponseView
            {
                Id = file.Id,
                Name = file.Name,
                Path = file.Path,
                Extension = file.Extension,
                Size = file.Size,
            }
        ).ToList());
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UserResponseView>> Update([FromRoute] string id, [FromBody]FileResponseView fileView)
    {
        var file = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == id);
        if (file == null)
        {
            return NotFound("Id not found in database");
        }

        file.Name = fileView.Name;
        file.Path = fileView.Path;
        file.Extension = fileView.Extension;
        file.Size = fileView.Size;
        file.Updated = DateTime.UtcNow;

        return Ok(new FileResponseView
        {
            Id = file.Id,
            Name = file.Name,
            Path = file.Path,
            Extension = file.Extension,
            Size = file.Size,
        });
    }
}