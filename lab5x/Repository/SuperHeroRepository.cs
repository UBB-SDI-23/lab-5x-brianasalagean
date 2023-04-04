using lab5.Data;
using lab5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace lab5.Repository
{
    public class SuperHeroRepository
    {
        private DataContext dbContext;

        public SuperHeroRepository(DataContext dbContext)
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

        public Task<IEnumerable<SuperHero>> GetAllHeroes()
        {
            var superheroes = dbContext.SuperHeroes.ToList() as IEnumerable<SuperHero>;
            return Task.FromResult(superheroes);
        }

        public async Task<SuperHero> GetHero(int Id)
        {
            return await dbContext.SuperHeroes.FindAsync(Id);
        }

        public Task<IEnumerable<SuperPower>> GetPowersOfHero(int SuperHeroId)
        {
           var result = (
                from SHP in dbContext.SuperHeroPowers
                where SHP.SuperHeroId == SuperHeroId
                join SP in dbContext.SuperPowers on SHP.SuperPowerId equals SP.Id
                select SP
            ) as IEnumerable<SuperPower>;
            return Task.FromResult(result);
        } 

        public Task<int> GetNrPowersOfHero(int SuperHeroId)
        {
            var superHeroPowers = GetPowersOfHero(SuperHeroId).Result.ToList();
            return Task.FromResult(superHeroPowers.Count());
        }

        public async Task<List<SuperHero>> AddHero(SuperHero superHero)
        {
            var manager = await dbContext.Managers.FindAsync(superHero.ManagerId);
            if (manager == null)
                return await dbContext.SuperHeroes.ToListAsync();
            dbContext.SuperHeroes.Add(superHero);
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>> UpdateHero(int Id, SuperHero superHero)
        {
            var hero = await dbContext.SuperHeroes.FindAsync(Id);
            if (hero == null)
                return null;
            hero.Name = superHero.Name;
            hero.FirstName = superHero.FirstName;
            hero.LastName = superHero.LastName;
            hero.Age = superHero.Age;
            hero.Place = superHero.Place;
            hero.ManagerId = superHero.ManagerId;
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>> RemoveHero(int Id)
        {
            var hero = await dbContext.SuperHeroes.FindAsync(Id);
            if (hero == null)
                return null;
            dbContext.SuperHeroes.Remove(hero);
            await dbContext.SaveChangesAsync();
            return await dbContext.SuperHeroes.ToListAsync();
        }
    }
}
