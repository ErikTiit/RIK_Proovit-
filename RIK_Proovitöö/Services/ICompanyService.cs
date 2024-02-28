using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Services
{
    public interface ICompanyService
    {
        Task<Company> GetCompanyAsync(int id);
        Task UpdateCompanyAsync(int id, Company company);
    }
}
