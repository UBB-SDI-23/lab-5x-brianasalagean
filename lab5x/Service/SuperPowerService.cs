using lab5.Models;
using lab5.Repository;

namespace lab5.Service
{
    public class SuperPowerService
    {
        SuperPowerRepository repo;

        public SuperPowerService(SuperPowerRepository repo)
        {
            this.repo = repo;
        }

        public SuperPowerRepository SuperPowerRepository
        {
            get => default;
            set
            {
            }
        }

        public Task<IEnumerable<SuperPower>> GetAllPowers()
        {
            return repo.GetAllPowers();
        }

        public async Task<SuperPower> GetPower(int Id)
        {
            return await repo.GetPower(Id);
        }

        public async Task<List<SuperHero>> GetAllHeroesWithPower(int SuperPowerId)
        {
            return await repo.GetAllHeroesWithPower(SuperPowerId);
        }

        public async Task<List<SuperPower>> AddPower(SuperPower power)
        {
            return await repo.AddPower(power);
        }

        public async Task<List<SuperPower>> UpdatePower(int Id, SuperPower power)
        {
            return await repo.UpdatePower(Id, power);
        }

        public async Task<List<SuperPower>> DeletePower(int Id)
        {
            return await repo.DeletePower(Id);
        }
    }
}
