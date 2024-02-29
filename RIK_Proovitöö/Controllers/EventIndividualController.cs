using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

[Route("api/[controller]")]
[ApiController]
public class EventIndividualController : ControllerBase
{
    private readonly IEventIndividualRepository _eventIndividualRepository;

    public EventIndividualController(IEventIndividualRepository eventIndividualRepository)
    {
        _eventIndividualRepository = eventIndividualRepository;
    }

    // GET: api/EventIndividual
    [HttpGet]
    public async Task<IActionResult> GetEventIndividuals()
    {
        try
        {
            var eventIndividuals = await _eventIndividualRepository.GetEventIndividuals();
            return new OkObjectResult(eventIndividuals);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/EventIndividual/Event/5
    [HttpGet("Event/{eventId}")]
    public async Task<IActionResult> GetIndividualsForEvent(int eventId)
    {
        try
        {
            var eventIndividuals = await _eventIndividualRepository.GetIndividualsForEvent(eventId);
            return new OkObjectResult(eventIndividuals);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/EventIndividual/5/1
    [HttpGet("{eventId}/{individualId}")]
    public async Task<IActionResult> GetEventIndividual(int eventId, int individualId)
    {
        try
        {
            var eventIndividual = await _eventIndividualRepository.GetEventIndividual(eventId, individualId);

            if (eventIndividual == null)
            {
                return new NotFoundObjectResult(new { message = "EventIndividual not found" });
            }

            return new OkObjectResult(eventIndividual);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // PUT: api/EventIndividual/5/1
    [HttpPut("{eventId}/{individualId}")]
    public async Task<IActionResult> PutEventIndividual(int eventId, int individualId, EventIndividual eventIndividual)
    {
        if (eventId != eventIndividual.EventID || individualId != eventIndividual.IndividualID)
        {
            return new BadRequestObjectResult(new { message = "Invalid request" });
        }

        try
        {
            await _eventIndividualRepository.UpdateEventIndividual(eventIndividual);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _eventIndividualRepository.EventIndividualExists(eventId, individualId))
            {
                return new NotFoundObjectResult(new { message = "EventIndividual not found" });
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

    // POST: api/EventIndividual
    [HttpPost]
    public async Task<IActionResult> PostEventIndividual(EventIndividual eventIndividual)
    {
        try
        {
            await _eventIndividualRepository.AddEventIndividual(eventIndividual);
            return new CreatedAtActionResult("GetEventIndividual", "EventIndividual", new { eventId = eventIndividual.EventID, individualId = eventIndividual.IndividualID }, eventIndividual);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // DELETE: api/EventIndividual/5/1
    [HttpDelete("{eventId}/{individualId}")]
    public async Task<IActionResult> DeleteEventIndividual(int eventId, int individualId)
    {
        try
        {
            var eventIndividual = await _eventIndividualRepository.GetEventIndividual(eventId, individualId);
            if (eventIndividual == null)
            {
                return new NotFoundObjectResult(new { message = "EventIndividual not found" });
            }

            await _eventIndividualRepository.DeleteEventIndividual(eventId, individualId);
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
