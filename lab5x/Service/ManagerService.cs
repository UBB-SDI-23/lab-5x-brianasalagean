using lab5.Models;
using lab5.Repository;
using Microsoft.AspNetCore.Mvc;

namespace lab5.Service
{
    public class ManagerService
    {
        private ManagerRepository repo;

        public ManagerService(ManagerRepository repo)
        {
            this.repo = repo;
        }

        public ManagerRepository ManagerRepository
        {
            get => default;
            set
            {
            }
        }

        public Task<IEnumerable<Manager>> GetAllManagers()
        {
            return repo.GetAllManagers();
        }

        public async Task<Manager> GetManager(int Id)
        {
            return await repo.GetManager(Id);
        }

        public Task<IEnumerable<SuperHero>> GetSuperHeroesByManager(int ManagerId)
        {
            return repo.GetSuperHeroesByManager(ManagerId);
        }

        public async Task<int> GetAvgAgeOfSuperHeroesByManager(int ManagerId)
        {
            var superHeroes = repo.GetSuperHeroesByManager(ManagerId).Result;
            int nrSuperHeroes = superHeroes.Count();
            if (nrSuperHeroes == 0)
                return 0;
            int sumAge = 0;
            foreach(SuperHero superHero in superHeroes)
            {
                sumAge += superHero.Age;
            }
            return sumAge / nrSuperHeroes;
        }

        public async Task<List<Manager>> AddManager(Manager manager)
        {
            return await repo.AddManager(manager);
        }

        public async Task<List<Manager>> UpdateManager(int Id, Manager newManager)
        {
            return await repo.UpdateManager(Id, newManager);
        }

        public async Task<List<Manager>> RemoveManager(int Id)
        {
            return await repo.RemoveManager(Id);
        }

        public async Task<IEnumerable<Manager>> GetManagersYearsExp(int YearsExp)
        {
            var managers = await repo.GetAllManagers();
            return managers.Where(m => m.YearsExp > YearsExp);
        }

        public async Task<List<SuperHero>> UpdateManagerIds(int Id, List<int> SuperHeroIds)
        {
            return await repo.UpdateManagerIds(Id, SuperHeroIds);
        }
    }
}
