using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

[Route("api/[controller]")]
[ApiController]
public class EventCompanyController : ControllerBase
{
    private readonly IEventCompanyRepository _eventCompanyRepository;

    public EventCompanyController(IEventCompanyRepository eventCompanyRepository)
    {
        _eventCompanyRepository = eventCompanyRepository;
    }

    // GET: api/EventCompany
    [HttpGet]
    public async Task<IActionResult> GetEventCompanies()
    {
        try
        {
            var eventCompanies = await _eventCompanyRepository.GetEventCompanies();
            return new OkObjectResult(eventCompanies);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/EventCompany/Event/5
    [HttpGet("Event/{eventId}")]
    public async Task<IActionResult> GetCompaniesForEvent(int eventId)
    {
        try
        {
            var eventCompanies = await _eventCompanyRepository.GetCompaniesForEvent(eventId);
            return new OkObjectResult(eventCompanies);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/EventCompany/5/1
    [HttpGet("{eventId}/{companyId}")]
    public async Task<IActionResult> GetEventCompany(int eventId, int companyId)
    {
        try
        {
            var eventCompany = await _eventCompanyRepository.GetEventCompany(eventId, companyId);

            if (eventCompany == null)
            {
                return new NotFoundObjectResult(new { message = "EventCompany not found" });
            }

            return new OkObjectResult(eventCompany);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // PUT: api/EventCompany/5/1
    [HttpPut("{eventId}/{companyId}")]
    public async Task<IActionResult> PutEventCompany(int eventId, int companyId, EventCompany eventCompany)
    {
        if (eventId != eventCompany.EventID || companyId != eventCompany.CompanyID)
        {
            return new BadRequestObjectResult(new { message = "Invalid request" });
        }

        try
        {
            await _eventCompanyRepository.UpdateEventCompany(eventCompany);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _eventCompanyRepository.EventCompanyExists(eventId, companyId))
            {
                return new NotFoundObjectResult(new { message = "EventCompany not found" });
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

    // POST: api/EventCompany
    [HttpPost]
    public async Task<IActionResult> PostEventCompany(EventCompany eventCompany)
    {
        try
        {
            await _eventCompanyRepository.AddEventCompany(eventCompany);
            return new CreatedAtActionResult("GetEventCompany", "EventCompany", new { eventId = eventCompany.EventID, companyId = eventCompany.CompanyID }, eventCompany);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // DELETE: api/EventCompany/5/1
    [HttpDelete("{eventId}/{companyId}")]
    public async Task<IActionResult> DeleteEventCompany(int eventId, int companyId)
    {
        try
        {
            var eventCompany = await _eventCompanyRepository.GetEventCompany(eventId, companyId);
            if (eventCompany == null)
            {
                return new NotFoundObjectResult(new { message = "EventCompany not found" });
            }

            await _eventCompanyRepository.DeleteEventCompany(eventId, companyId);
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
