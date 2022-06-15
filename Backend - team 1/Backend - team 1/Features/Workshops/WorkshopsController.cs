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
            Participants = participantsview
        });
    }

    [HttpGet]
    public async Task<ActionResult<WorkshopResponseView>> Get()
    {
        return Ok(
            _dbContext.Workshops.Select(
                workshop => new WorkshopResponseView
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
                    Participants = workshop.Participants.Select(
                        participant => new UserResponseView
                        {
                            Id = participant.Id,
                            FirstName = participant.FirstName,
                            LastName = participant.LastName,
                            Email = participant.Email,
                            Roles = participant.Roles,
                        }
                        ).ToArray()
                }
                ).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkshopResponseView>> GetById([FromRoute] string id)
    {
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in database");
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
            Participants = workshop.Participants.Select(
                participant => new UserResponseView
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    Roles = participant.Roles,
                }
            ).ToArray()
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Workshop>> Delete([FromRoute] string id)
    {
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in database");
        }

        _dbContext.Workshops.Remove(workshop);
        await _dbContext.SaveChangesAsync();

        return Ok(workshop);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<WorkshopResponseView>> Update([FromRoute] string id, [FromBody] WorkshopRequestView workshopView)
    {
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in database");
        }
        
        var trainer = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == workshopView.TrainerId);
        if (trainer == null)
        {
            return NotFound("Trainer id not found");
        }

        var cover = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == workshopView.CoverId);
        if (cover == null)
        {
            return NotFound("Cover Image id not found");
        }

        var presentation = await _dbContext.Files.FirstOrDefaultAsync(entity => entity.Id == workshopView.PresentationId);
        if (presentation == null)
        {
            return NotFound("Presentation id not found");
        }

        var participants = new User[workshopView.MaxCapacity];
        int count = 0;
        foreach (string wsId in workshopView.UsersIds)
        {
            var participant = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == wsId);
            if (participant == null)
            {
                return NotFound("User id not found");
            }

            participants[count++] = participant;
        }

        workshop.Trainer = trainer;
        workshop.Topic = workshopView.Topic;
        workshop.Description = workshopView.Description;
        workshop.CoverImage = cover;
        workshop.DateStart = workshopView.DateStart;
        workshop.DateEnd = workshopView.DateEnd;
        workshop.Capacity = count;
        workshop.MaxCapacity = workshopView.MaxCapacity;
        workshop.Location = workshopView.Location;
        workshop.Participants = participants;
        workshop.Presentation = presentation;
        workshop.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        
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
            Participants = workshop.Participants.Select(
                participant => new UserResponseView
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    Roles = participant.Roles,
                }
            ).ToArray()
        });
    }

    [HttpPatch("{id}/ChangeTrainer")]
    public async Task<ActionResult<WorkshopResponseView>> ChangeTrainer([FromRoute] string id, [FromBody] string trainerId)
    {
        var trainer = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == trainerId);
        if (trainer == null)
        {
            return NotFound("Trainer id not found in database");
        }
        
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in database");
        }

        workshop.Trainer = trainer;
        workshop.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

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
            Participants = workshop.Participants.Select(
                participant => new UserResponseView
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    Roles = participant.Roles,
                }
            ).ToArray()
        });
    }

    [HttpPost("{id}/{participantid}")]
    public async Task<ActionResult<WorkshopResponseView>> AddParticipant([FromRoute] string id, [FromRoute] string participantid)
    {
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in dataabse");
        }

        var participant = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == participantid);
        if (participant == null)
        {
            return NotFound("Participant id not found in database");
        }

        if (workshop.Capacity >= workshop.MaxCapacity - 1)
        {
            throw new Exception("Participant cannot be added");
        }

        workshop.Participants[workshop.Capacity++] = participant;
        workshop.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

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
            Participants = workshop.Participants.Select(
                participant => new UserResponseView
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    Roles = participant.Roles,
                }
            ).ToArray()
        });
    }

    [HttpDelete("{id}/{participantid}")]
    public async Task<ActionResult<WorkshopResponseView>> RemoveParticipant([FromRoute] string id, [FromRoute] string participantid)
    {
        var workshop = await _dbContext.Workshops.Include(workshop => workshop.Trainer).FirstOrDefaultAsync(entity => entity.Id == id);
        if (workshop == null)
        {
            return NotFound("Workshop id not found in dataabse");
        }

        for (int i = 0; i < workshop.Capacity; i++)
        {
            if (workshop.Participants[i].Id == participantid)
            {
                for (int j = i; j < workshop.Capacity; j++)
                {
                    workshop.Participants[j] = workshop.Participants[j + 1];
                }

                workshop.Capacity--;
                
                break;
            }
        }

        await _dbContext.SaveChangesAsync();
        
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
            Participants = workshop.Participants.Select(
                participant => new UserResponseView
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    Roles = participant.Roles,
                }
            ).ToArray()
        });
    }
}