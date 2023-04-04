using lab5.Models;
using lab5.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPowerController : ControllerBase
    {
        public SuperPowerService service;

        public SuperPowerController(SuperPowerService service)
        {
            this.service = service;
        }

        public SuperPowerService SuperPowerService
        {
            get => default;
            set
            {
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperPower>>> Get()
        {
            return Ok(await service.GetAllPowers());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var superPower = await service.GetPower(Id);
            if (superPower == null)
                return BadRequest("power not found");
            var superHeroes = await service.GetAllHeroesWithPower(Id);
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize((superPower, superHeroes), options);
            return Ok(jsonString);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperPower>>> Add(SuperPower superPower)
        {
            if (superPower.Type != "physical" && superPower.Type != "mental")
                return BadRequest("the type of a superpower must be physical or mental");
            return Ok(await service.AddPower(superPower));
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<SuperPower>>> Update(int Id, SuperPower superPower)
        {
            if (Id != superPower.Id)
                return BadRequest("Ids don't match");
            var powers = await service.UpdatePower(Id, superPower);
            if (powers == null)
                return BadRequest("power not found");
            return Ok(powers);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int Id)
        {
            var powers = await service.DeletePower(Id);
            if (powers == null)
                return BadRequest("power not found");
            return Ok(powers);
        }
    }
}
