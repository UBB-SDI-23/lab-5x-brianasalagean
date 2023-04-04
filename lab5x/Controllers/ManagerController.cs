using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab5.Service;
using lab5.Models;
using Microsoft.Extensions.Localization;
using lab5.Other;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private ManagerService service;

        public ManagerController(ManagerService service)
        {
            this.service = service;
        }

        public ManagerService ManagerService
        {
            get => default;
            set
            {
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> Get()
        {
            return Ok(await service.GetAllManagers());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var manager = await service.GetManager(Id);
            if (manager == null)
                return BadRequest("manager not found");
            var superHeroes = await service.GetSuperHeroesByManager(Id);
            var result = new Obj(manager, superHeroes.ToList());
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize(result, options);
            return Ok(jsonString);
        }

        [HttpGet("ManagersSortedByAvgAgeOfSuperHeroes")]
        public async Task<ActionResult> GetManagersAvgAgeHeroes()
        {
            IEnumerable<Manager> managers = await service.GetAllManagers();
            List<ManagerByAvgAgeSH> result = new List<ManagerByAvgAgeSH>();
            foreach (Manager manager in managers)
            {
                var partialResult = new ManagerByAvgAgeSH(manager.Id, await service.GetAvgAgeOfSuperHeroesByManager(manager.Id));
                result.Add(partialResult);
            }
            result.Sort((pr1, pr2) => pr1.AvgAgeOfSuperHero.CompareTo(pr2.AvgAgeOfSuperHero));
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize(result, options);
            return Ok(jsonString);
        }

        [HttpGet]
        [Route("YearsExp/{YearsExp}")]
        public async Task<ActionResult<List<Manager>>> Filter(int YearsExp)
        {
            return Ok(await service.GetManagersYearsExp(YearsExp));
        }

        [HttpPost]
        public async Task<ActionResult<List<Manager>>> Add(Manager manager)
        {
            if (manager.Age < 18)
                return BadRequest("manager must be an adult");
            if (manager.YearsExp < 0)
                return BadRequest("managers must have a positive number of years experiance");
            return Ok(await service.AddManager(manager));
        }

        [HttpPost("{Id}/SuperHeroes")]
        public async Task<ActionResult<List<Manager>>> AddSuperHeroesToManager(int Id, List<int>SuperHeroIds)
        {
            var manager = await service.GetManager(Id);
            if (manager == null)
                return BadRequest("manager not found");
            await service.UpdateManagerIds(Id, SuperHeroIds);
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize((manager, await service.GetSuperHeroesByManager(Id)), options);
            return Ok(jsonString);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<Manager>>> Update(int Id, Manager newManager)
        {
            if (Id != newManager.Id)
                return BadRequest("Ids don't match");
            var managers = await service.UpdateManager(Id, newManager);
            if (managers == null)
                return BadRequest("manager not found");
            return Ok(managers);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Manager>>> Delete(int Id)
        {
            var managers = await service.RemoveManager(Id);
            if (managers == null)
                return BadRequest("manager not found");
            return Ok(managers);
        }
    }
}
