using lab5.Models;
using System.Text.Json;

namespace lab5.Other
{
    public class Obj
    {
        public Manager manager;
        public List<SuperHero> superHeroes;

        public Obj(Manager manager, List<SuperHero> superHeroes)
        {
            this.manager = manager;
            this.superHeroes = superHeroes;
        }
    }
}
