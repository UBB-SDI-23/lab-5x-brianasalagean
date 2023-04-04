using lab5.Data;
using lab5.Models;
using lab5.Other;
using lab5.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace lab5.Service
{
    public class SuperHeroService
    {
        private SuperHeroRepository repo;

        public SuperHeroService(SuperHeroRepository repo)
        {
            this.repo = repo;
        }

        public SuperHeroRepository SuperHeroRepository
        {
            get => default;
            set
            {
            }
        }

        public Task<IEnumerable<SuperHero>> GetAllHeroes()
        {
            return repo.GetAllHeroes();
        }

        public async Task<SuperHero> GetHero(int Id)
        {
            return await repo.GetHero(Id);
        }

        public Task<IEnumerable<SuperPower>> GetPowersOfHero(int SuperHeroId)
        {
            return repo.GetPowersOfHero(SuperHeroId);
        }

        public Task<IEnumerable<SHByNrOfSP>> GetHeroesNrPowers()
        {
            IEnumerable<SuperHero> superHeroes = repo.GetAllHeroes().Result;
            var result = new List<SHByNrOfSP>();
            foreach (SuperHero superHero in superHeroes)
            {
                var partialResult = new SHByNrOfSP(superHero.Id, repo.GetNrPowersOfHero(superHero.Id).Result);
                result.Add(partialResult);
            }
            result.Sort((pr1, pr2) => pr1.NrSuperPowers.CompareTo(pr2.NrSuperPowers));
            var finalResult = result as IEnumerable<SHByNrOfSP>;
            return Task.FromResult(finalResult);
        }

        public async Task<List<SuperHero>> AddHero(SuperHero superHero)
        {
            return await repo.AddHero(superHero);
        }

        public async Task<List<SuperHero>> UpdateHero(int Id, SuperHero superHero)
        {
            return await repo.UpdateHero(Id, superHero);
        }

        public async Task<List<SuperHero>> RemoveHero(int Id)
        {
            return await repo.RemoveHero(Id);
        }
    }
}
