using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventsController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        try
        {
            var events = await _eventRepository.GetEvents();
            return new OkObjectResult(events);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        try
        {
            var eventItem = await _eventRepository.GetEvent(id);

            if (eventItem == null)
            {
                return new NotFoundObjectResult(new { message = "Event not found" });
            }

            return new OkObjectResult(eventItem);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // PUT: api/Events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, Event eventItem)
    {
        if (id != eventItem.ID)
        {
            return new BadRequestObjectResult(new { message = "Invalid request" });
        }

        try
        {
            await _eventRepository.UpdateEvent(eventItem);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _eventRepository.EventExists(id))
            {
                return new NotFoundObjectResult(new { message = "Event not found" });
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // POST: api/Events
    [HttpPost]
    public async Task<IActionResult> PostEvent(Event eventItem)
    {
        if (eventItem.Date <= DateTime.Now)
        {
            return new BadRequestObjectResult(new { message = "The event date must be in the future." });
        }

        try
        {
            await _eventRepository.AddEvent(eventItem);
            return new CreatedAtActionResult("GetEvent", "Events", new { id = eventItem.ID }, eventItem);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        try
        {
            var eventItem = await _eventRepository.GetEvent(id);
            if (eventItem == null)
            {
                return new NotFoundObjectResult(new { message = "Event not found" });
            }

            await _eventRepository.DeleteEvent(id);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
