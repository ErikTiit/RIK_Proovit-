using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

[Route("api/[controller]")]
[ApiController]
public class IndividualController : ControllerBase
{
    private readonly IIndividualRepository _individualRepository;

    public IndividualController(IIndividualRepository individualRepository)
    {
        _individualRepository = individualRepository;
    }

    // GET: api/Individual
    [HttpGet]
    public async Task<IActionResult> GetIndividuals()
    {
        try
        {
            var individuals = await _individualRepository.GetIndividuals();
            return new OkObjectResult(individuals);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // GET: api/Individual/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIndividual(int id)
    {
        try
        {
            var individual = await _individualRepository.GetIndividual(id);

            if (individual == null)
            {
                return new NotFoundObjectResult(new { message = "Individual not found" });
            }

            return new OkObjectResult(individual);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // PUT: api/Individual/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIndividual(int id, Individual individual)
    {
        if (id != individual.ID)
        {
            return new BadRequestObjectResult(new { message = "Invalid request" });
        }

        try
        {
            await _individualRepository.UpdateIndividual(individual);
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _individualRepository.IndividualExists(id))
            {
                return new NotFoundObjectResult(new { message = "Individual not found" });
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

    // POST: api/Individual
    [HttpPost]
    public async Task<IActionResult> PostIndividual(Individual individual)
    {
        try
        {
            await _individualRepository.AddIndividual(individual);
            return new CreatedAtActionResult("GetIndividual", "Individual", new { id = individual.ID }, individual);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine(ex.Message);
            return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    // DELETE: api/Individual/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIndividual(int id)
    {
        try
        {
            var individual = await _individualRepository.GetIndividual(id);
            if (individual == null)
            {
                return new NotFoundObjectResult(new { message = "Individual not found" });
            }

            await _individualRepository.DeleteIndividual(id);
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
