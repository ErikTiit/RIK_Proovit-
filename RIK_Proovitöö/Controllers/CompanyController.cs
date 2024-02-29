using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetCompanies();
                return new OkObjectResult(companies);
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompany(id);

                if (company == null)
                {
                    return new NotFoundObjectResult(new { message = "Company not found" });
                }

                return new OkObjectResult(company);
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.ID)
            {
                return new BadRequestObjectResult(new { message = "Invalid request" });
            }

            try
            {
                await _companyRepository.UpdateCompany(company);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _companyRepository.CompanyExists(id))
                {
                    return new NotFoundObjectResult(new { message = "Company not found" });
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

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> PostCompany(Company company)
        {
            try
            {
                await _companyRepository.AddCompany(company);
                return new CreatedAtActionResult("GetCompany", "Company", new { id = company.ID }, company);
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return new ObjectResult(new { message = "An error occurred while processing your request.", error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                if (!await _companyRepository.CompanyExists(id))
                {
                    return new NotFoundObjectResult(new { message = "Company not found" });
                }

                await _companyRepository.DeleteCompany(id);
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
}
