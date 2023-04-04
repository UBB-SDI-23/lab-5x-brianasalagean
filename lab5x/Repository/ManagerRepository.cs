using lab5.Data;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab5.Repository
{
    public class ManagerRepository
    {

        private DataContext dbContext;

        public ManagerRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DataContext DataContext
        {
            get => default;
            set
            {
            }
        }

        public Task<IEnumerable<Manager>> GetAllManagers()
        {
            var managers =  dbContext.Managers.ToList() as IEnumerable<Manager>;
            return Task.FromResult(managers);
        }

        public async Task<Manager> GetManager(int Id)
        {
            return await dbContext.Managers.FindAsync(Id);
        }

        public Task<IEnumerable<SuperHero>> GetSuperHeroesByManager(int ManagerId)
        {
            var superHeroes = dbContext.SuperHeroes
                .Where(x => x.ManagerId == ManagerId)
                .ToList() as IEnumerable<SuperHero>;
            return Task.FromResult(superHeroes);
        }

        public async Task<List<Manager>> AddManager(Manager manager)
        {
            dbContext.Managers.Add(manager);
            await dbContext.SaveChangesAsync();
            return await dbContext.Managers.ToListAsync();
        }

        public async Task<List<Manager>> UpdateManager(int Id, Manager newManager)
        {
            var oldManager = await dbContext.Managers.FindAsync(Id);
            if (oldManager == null)
                return null;
            oldManager.FirstName = newManager.FirstName;
            oldManager.LastName = newManager.LastName; 
            oldManager.Location = newManager.Location; 
            oldManager.Age = newManager.Age;
            oldManager.YearsExp = newManager.YearsExp;
            await dbContext.SaveChangesAsync();
            return await dbContext.Managers.ToListAsync();
        }

        public async Task<List<Manager>> RemoveManager(int Id)
        {
            var manager = await dbContext.Managers.FindAsync(Id);
            if (manager == null)
                return null;
            dbContext.Managers.Remove(manager);
            await dbContext.SaveChangesAsync();
            return await dbContext.Managers.ToListAsync();
        }

        public async Task<List<SuperHero>> UpdateManagerIds(int Id, List<int> SuperHeroIds)
        {
            foreach(int shid in SuperHeroIds)
            {
                var hero = await dbContext.SuperHeroes.FindAsync(shid);
                if (hero != null)
                {
                    hero.ManagerId = Id;
                    await dbContext.SaveChangesAsync();
                }
            }
            return await dbContext.SuperHeroes.ToListAsync();
        }
    }
}
