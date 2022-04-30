using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
        {
            new SuperHero() {
                    Id = 1,
                    Alias = "Batman",
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    Location = "Gotham"

                },
                new SuperHero() {
                    Id = 2,
                    Alias = "Superman",
                    FirstName = "Clark",
                    LastName = "Kent",
                    Location = "Metropolis"

                },
                new SuperHero() {
                    Id = 3,
                    Alias = "Spiderman",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Location = "New York City, NY"

                },
                new SuperHero() {
                    Id = 4,
                    Alias = "Captain America",
                    FirstName = "Steve",
                    LastName = "Rogers",
                    Location = "Brooklyn, NY"

                }
        };

        [HttpGet]
        [Route("GetAllHeroes")]
        public async Task<ActionResult<List<SuperHero>>> GetHeros()
        {
            if (heros != null)
            {

                return Ok(heros);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetHeroByID")]
        public async Task<ActionResult<SuperHero>> GetHeroByID(int id)
        {
            var hero = heros.Find(x => x.Id == id);

            if (hero != null)
            {
                return Ok(hero);
            }
            else
            {
                return NotFound("No hero with that ID found.");
            }
        }


        [HttpPost]
        [Route("AddHero")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            hero.Id = heros.Count() + 1;
            if (string.IsNullOrEmpty(hero.Location))
            {
                hero.Location = "Unknown";
            }

            if (hero != null && !string.IsNullOrEmpty(hero.Alias) && !string.IsNullOrEmpty(hero.FirstName) && !string.IsNullOrEmpty(hero.LastName))
            {
                heros.Add(hero);
                return Ok(heros);
            }
            else
            {
                return BadRequest(heros);
            }
        }

        [HttpPatch]
        [Route("UpdateHeroByID")]
        public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero heroRequest)
        {

            var hero = heros.Find(x => x.Id == id);
            if (hero != null)
            {
                if (!string.IsNullOrEmpty(heroRequest.Alias))
                {
                    hero.Alias = heroRequest.Alias;
                }
                if (!string.IsNullOrEmpty(heroRequest.FirstName))
                {
                    hero.FirstName = heroRequest.FirstName;
                }
                if (!string.IsNullOrEmpty(heroRequest.LastName))
                {
                    hero.LastName= heroRequest.LastName;
                }
                if (!string.IsNullOrEmpty(heroRequest.Location))
                {
                    hero.Location= heroRequest.Location;
                }

                return Ok(hero);
            }
            else
            {
                return NotFound("Didn't find a hero with this Id.");
            }
        }

        [HttpDelete]
        [Route("RemoveHeroByID")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = heros.Find(x => x.Id == id);

            if (hero == null)
            {
                return BadRequest("No hero with that Id exists.");
            }

            heros.Remove(hero);
            return Ok(heros);
        }
    }
}
