using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Services
{
    public interface IIndividualService
    {
        Task<Individual> GetIndividualAsync(int id);
        Task UpdateIndividualAsync(int id, Individual individual);
    }
}
