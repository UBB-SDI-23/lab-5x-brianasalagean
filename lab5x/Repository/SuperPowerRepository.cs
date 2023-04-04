using lab5.Data;
using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Repository
{
    public class SuperPowerRepository
    {
        private DataContext dbContext;
        
        public SuperPowerRepository(DataContext dbContext)
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

        public Task<IEnumerable<SuperPower>> GetAllPowers()
        {
            var superPowers = dbContext.SuperPowers.ToList() as IEnumerable<SuperPower>;
            return Task.FromResult(superPowers);
        }

        public async Task<SuperPower> GetPower(int Id)
        {
            return await dbContext.SuperPowers.FindAsync(Id);
        }

        public async Task<List<SuperHero>> GetAllHeroesWithPower(int SuperPowerId)
        {
            List<SuperHeroPower> superHeroPowers = await dbContext.SuperHeroPowers.ToListAsync();
            List<SuperHero> superHeroes = new List<SuperHero>(); 
            foreach(SuperHeroPower superHeroPower in superHeroPowers)
            {
                if (superHeroPower.SuperPowerId == SuperPowerId)
                {
                    var superHero = await dbContext.SuperHeroes.FindAsync(superHeroPower.SuperHeroId);
                    if (superHero != null)
                        superHeroes.Add(superHero);
                }
            }
            return superHeroes;
        }

        public async Task<List<SuperPower>> AddPower(SuperPower power)
        {
            dbContext.SuperPowers.Add(power);
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperPowers.ToListAsync();
        }

        public async Task<List<SuperPower>> UpdatePower(int Id, SuperPower power)
        {
            var oldPower = await dbContext.SuperPowers.FindAsync(Id);
            if (oldPower == null)
                return null;
            oldPower.Name = power.Name;
            oldPower.Description = power.Description;
            oldPower.Type = power.Type;
            oldPower.HasAntidote = power.HasAntidote;
            oldPower.IsControlled = power.IsControlled;
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperPowers.ToListAsync();
        }

        public async Task<List<SuperPower>> DeletePower(int Id)
        {
            var power = await dbContext.SuperPowers.FindAsync(Id);
            if (power == null)
                return null;
            dbContext.SuperPowers.Remove(power);
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperPowers.ToListAsync();
        }
    }
}
