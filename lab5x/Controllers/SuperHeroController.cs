using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab5.Models;
using lab5.Service;
using lab5.Data;
using System.Text.Json;
using lab5.Other;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private SuperHeroService service;

        public SuperHeroController(SuperHeroService service)
        {
            this.service = service;
        }

        public SuperHeroService SuperHeroService
        {
            get => default;
            set
            {
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> Get()
        {
            return Ok(await service.GetAllHeroes());
        }

        [HttpGet("SuperHeroesSortedByNrOfSuperPowers")]
        public async Task<ActionResult> GetHeroesNrPowers()
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize(await service.GetHeroesNrPowers(), options);
            return Ok(jsonString);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var hero = await service.GetHero(Id);
            if (hero == null)
                return BadRequest("hero not found");
            var superPowers = await service.GetPowersOfHero(Id);
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize((hero, superPowers), options);
            return Ok(jsonString);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Add(SuperHero superHero)
        {
            if (superHero.Age < 0)
                return BadRequest("the age of a superhero can't be negative");
            return Ok(await service.AddHero(superHero));
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> Update(int Id, SuperHero superHero)
        {
            if (Id != superHero.Id)
                return BadRequest("Ids don't match");
            var heroes = await service.UpdateHero(Id, superHero);
            if (heroes == null)
                return BadRequest("hero not found");
            return Ok(heroes);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int Id)
        {
            var heroes = await service.RemoveHero(Id);
            if (heroes == null)
                return BadRequest("hero not found");
            return Ok(heroes);
        }
    }
}
