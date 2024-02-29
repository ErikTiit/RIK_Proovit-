using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public interface IIndividualRepository
    {
        Task<IEnumerable<Individual>> GetIndividuals();
        Task<Individual> GetIndividual(int id);
        Task<Individual> AddIndividual(Individual individual);
        Task<Individual> UpdateIndividual(Individual individual);
        Task DeleteIndividual(int id);
        Task<bool> IndividualExists(int id);
    }

}
