using lab5.Repository;
using lab5.Service;
using lab5.Models;
using Moq;
using lab5.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection.Metadata;
using lab5.Other;
using System.Collections.Generic;

namespace lab5x_testing
{
    public class NonCRUDTests
    {
        [Test]
        public void TestSuperHeroesSortedByNrOfSuperPowers()
        {
            var dataContextMock = new Mock<DataContext>();
            var shRepo = new SuperHeroRepository(dataContextMock.Object);
            var shService = new SuperHeroService(shRepo);
            var superHeroes = new List<SuperHero>()
            {
                new SuperHero {
                    Id = 1,
                    Name = "Ant-Man",
                    FirstName = "Hank",
                    LastName = "Pym",
                    Age = 60,
                    Place = "Nebraska",
                    ManagerId = 2
                  },
                new SuperHero  {
                    Id = 2,
                    Name = "Spider-Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Age = 18,
                    Place = "NYC",
                    ManagerId = 1
                  },
                new SuperHero  {
                    Id = 3,
                    Name = "Ironman",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Age = 53,
                    Place = "Long Island",
                    ManagerId = 1
                  },
                new SuperHero  {
                    Id = 4,
                    Name = "Black Panther",
                    FirstName = "T",
                    LastName = "Challa",
                    Age = 37,
                    Place = "Wakanda",
                    ManagerId = 2
                  }
            }.AsQueryable();
            var shMockSet = new Mock<DbSet<SuperHero>>();
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.Provider).Returns(superHeroes.Provider);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.Expression).Returns(superHeroes.Expression);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.ElementType).Returns(superHeroes.ElementType);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.GetEnumerator()).Returns(() => superHeroes.GetEnumerator());
            dataContextMock.Setup(db => db.SuperHeroes).Returns(shMockSet.Object);

            var spRepo = new SuperPowerRepository(dataContextMock.Object);
            var spService = new SuperPowerService(spRepo);
            var superPowers = new List<SuperPower>()
            {
                  new SuperPower {
                    Id = 5,
                    Name = "teleport",
                    Description = "spawn to different place",
                    Type = "physical",
                    HasAntidote = true,
                    IsControlled = true
                  },
                  new SuperPower {
                    Id = 7,
                    Name = "spider sense",
                    Description = "anticipation of bad events",
                    Type = "mental",
                    HasAntidote = true,
                    IsControlled = false
                  },
                  new SuperPower {
                    Id = 9,
                    Name = "fast healing",
                    Description = "healing fast from injury",
                    Type = "physical",
                    HasAntidote = false,
                    IsControlled = false
                  }
            }.AsQueryable();
            var spMockSet = new Mock<DbSet<SuperPower>>();
            spMockSet.As<IQueryable<SuperPower>>().Setup(m => m.Provider).Returns(superPowers.Provider);
            spMockSet.As<IQueryable<SuperPower>>().Setup(m => m.Expression).Returns(superPowers.Expression);
            spMockSet.As<IQueryable<SuperPower>>().Setup(m => m.ElementType).Returns(superPowers.ElementType);
            spMockSet.As<IQueryable<SuperPower>>().Setup(m => m.GetEnumerator()).Returns(() => superPowers.GetEnumerator());
            dataContextMock.Setup(db => db.SuperPowers).Returns(spMockSet.Object);

            var superHeroPowers = new List<SuperHeroPower>()
            {
                new SuperHeroPower
                {
                    SuperHeroId = 2,
                    SuperPowerId = 7,
                    Score = 7,
                    UseTime = 24,
                    RecoveryTime = 0
                },
                new SuperHeroPower
                {
                    SuperHeroId = 1,
                    SuperPowerId = 9,
                    Score = 8,
                    UseTime = 12,
                    RecoveryTime = 1
                },
                new SuperHeroPower
                {
                    SuperHeroId = 2,
                    SuperPowerId = 9,
                    Score = 5,
                    UseTime = 4,
                    RecoveryTime = 2
                },
                new SuperHeroPower
                {
                    SuperHeroId = 3,
                    SuperPowerId = 5,
                    Score = 3,
                    UseTime = 1,
                    RecoveryTime = 24
                }
            }.AsQueryable();
            var shpMockSet = new Mock<DbSet<SuperHeroPower>>();
            shpMockSet.As<IQueryable<SuperHeroPower>>().Setup(m => m.Provider).Returns(superHeroPowers.Provider);
            shpMockSet.As<IQueryable<SuperHeroPower>>().Setup(m => m.Expression).Returns(superHeroPowers.Expression);
            shpMockSet.As<IQueryable<SuperHeroPower>>().Setup(m => m.ElementType).Returns(superHeroPowers.ElementType);
            shpMockSet.As<IQueryable<SuperHeroPower>>().Setup(m => m.GetEnumerator()).Returns(() => superHeroPowers.GetEnumerator());
            dataContextMock.Setup(db => db.SuperHeroPowers).Returns(shpMockSet.Object);

            List<SHByNrOfSP> expected = new List<SHByNrOfSP>
            {
                new SHByNrOfSP(4, 0),
                new SHByNrOfSP(1, 1),
                new SHByNrOfSP(3, 1),
                new SHByNrOfSP(2, 2)
            };

            var result = shService.GetHeroesNrPowers().Result.ToList();
            for (var i = 0; i < 4; i += 1)
            {
                Assert.True(result[i].NrSuperPowers == expected[i].NrSuperPowers);
            }
        }

        [Test]
        public void TestAvgAgeOfSuperHeroesByManager()
        {
            var dataContextMock = new Mock<DataContext>();
            var shRepo = new SuperHeroRepository(dataContextMock.Object);
            var shService = new SuperHeroService(shRepo);
            var superHeroes = new List<SuperHero>()
            {
                new SuperHero {
                    Id = 1,
                    Name = "Ant-Man",
                    FirstName = "Hank",
                    LastName = "Pym",
                    Age = 60,
                    Place = "Nebraska",
                    ManagerId = 2
                  },
                new SuperHero  {
                    Id = 2,
                    Name = "Spider-Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Age = 18,
                    Place = "NYC",
                    ManagerId = 1
                  },
                new SuperHero  {
                    Id = 3,
                    Name = "Ironman",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Age = 53,
                    Place = "Long Island",
                    ManagerId = 1
                  },
                new SuperHero  {
                    Id = 4,
                    Name = "Black Panther",
                    FirstName = "T",
                    LastName = "Challa",
                    Age = 37,
                    Place = "Wakanda",
                    ManagerId = 2
                  }
            }.AsQueryable();
            var shMockSet = new Mock<DbSet<SuperHero>>();
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.Provider).Returns(superHeroes.Provider);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.Expression).Returns(superHeroes.Expression);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.ElementType).Returns(superHeroes.ElementType);
            shMockSet.As<IQueryable<SuperHero>>().Setup(m => m.GetEnumerator()).Returns(() => superHeroes.GetEnumerator());
            dataContextMock.Setup(db => db.SuperHeroes).Returns(shMockSet.Object);

            var managerRepo = new ManagerRepository(dataContextMock.Object);
            var managerService = new ManagerService(managerRepo);
            var managers = new List<Manager>()
            {
                new Manager {
                    Id = 1,
                    FirstName = "Nick",
                    LastName = "Fury",
                    Location = "NYC",
                    Age = 60,
                    YearsExp = 30
                  },
                new Manager {
                    Id = 2,
                    FirstName = "Norman",
                    LastName = "Oborn",
                    Location = "Brooklyn",
                    Age =  70,
                    YearsExp = 25
                  },
                new Manager  {
                    Id = 3,
                    FirstName = "Steve",
                    LastName = "Rogers",
                    Location =  "Brooklyn",
                    Age = 105,
                    YearsExp = 6
                  }
            }.AsQueryable();
            var mgMockSet = new Mock<DbSet<Manager>>();
            mgMockSet.As<IQueryable<Manager>>().Setup(m => m.Provider).Returns(managers.Provider);
            mgMockSet.As<IQueryable<Manager>>().Setup(m => m.Expression).Returns(managers.Expression);
            mgMockSet.As<IQueryable<Manager>>().Setup(m => m.ElementType).Returns(managers.ElementType);
            mgMockSet.As<IQueryable<Manager>>().Setup(m => m.GetEnumerator()).Returns(() => managers.GetEnumerator());
            dataContextMock.Setup(db => db.Managers).Returns(mgMockSet.Object);

            var result = managerService.GetAvgAgeOfSuperHeroesByManager(1).Result;
            Assert.AreEqual(35, result);
            var result2 = managerService.GetAvgAgeOfSuperHeroesByManager(2).Result;
            Assert.AreEqual(48, result2);
        }
    }
}