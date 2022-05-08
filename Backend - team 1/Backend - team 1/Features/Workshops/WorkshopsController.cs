using Backend___team_1.Database;
using Backend___team_1.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend___team_1.Features.Workshops;
[ApiController]
[Route("api/workshops")]
public class WorkshopsController : Controller
{
    private readonly AppDbContext _dbContext;

    public WorkshopsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<WorkshopResponseView>> Post([FromBody] WorkshopRequestView workshopview)
    {
        var trainer = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == workshopview.TrainerId);
        if (trainer == null)
        {
            return NotFound("Trainer id not found");
        }

        var cover = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == workshopview.CoverId);
        if (cover == null)
        {
            return NotFound("Cover Image id not found");
        }

        var presentation = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == workshopview.PresentationId);
        if (presentation == null)
        {
            return NotFound("Presentation id not found");
        }

        var participants = new User[workshopview.MaxCapacity];
        int count = 0;
        foreach (string id in workshopview.UsersIds)
        {
            var participant = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
            if (participant == null)
            {
                return NotFound("User id not found");
            }

            participants[count++] = participant;
        }

        var workshop = new Workshop
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Trainer = trainer,
            Topic = workshopview.Topic,
            Description = workshopview.Description,
            CoverImage = cover,
            DateStart = workshopview.DateStart,
            DateEnd = workshopview.DateEnd,
            MaxCapacity = workshopview.MaxCapacity,
            Location = workshopview.Location,
            Presentation = presentation,
            Participants = participants,
            Capacity = participants.Length,
        };

        await _dbContext.AddAsync(workshop);
        await _dbContext.SaveChangesAsync();

        var participantsview = new UserResponseView[workshop.MaxCapacity];
        count = 0;
        foreach (User participant in workshop.Participants)
        {
            var participantview = new UserResponseView
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                Email = participant.Email,
                Roles = participant.Roles,
            };
            participantsview[count++] = participantview;
        }
        return Ok(new WorkshopResponseView
        {
            Id = workshop.Id,
            Topic = workshop.Topic,
            Trainer = new UserResponseView
            {
                Id = workshop.Trainer.Id,
                FirstName = workshop.Trainer.FirstName,
                LastName = workshop.Trainer.LastName,
                Email = workshop.Trainer.Email,
                Roles = workshop.Trainer.Roles,
            },
            Description = workshop.Description,
            Coverpath = workshop.CoverImage.Path,
            DateStart = workshop.DateStart,
            DateEnd = workshop.DateEnd,
            MaxCapacity = workshop.MaxCapacity,
            Capacity = workshop.Capacity,
            Location = workshop.Location,
            PresentationPath = workshop.Presentation.Path,
            Participants = participantsview,
        });
    }
}