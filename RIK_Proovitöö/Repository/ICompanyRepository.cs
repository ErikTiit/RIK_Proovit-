using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int id);
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(Company company);
        Task DeleteCompany(int id);
        Task<bool> CompanyExists(int id);
    }
}
