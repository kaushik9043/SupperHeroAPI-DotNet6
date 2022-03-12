using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI;
using SupperHeroAPI.Data;
using System.Data.Entity;

namespace SupperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupperHeroController : ControllerBase
    {
        private static List<SupperHero> heroes = new List<SupperHero>
        {
                new SupperHero {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName="Parker",
                    Place = "New York City"
                },
                 new SupperHero {
                    Id = 2,
                    Name = "Ironman",
                    FirstName = "Tony",
                    LastName="Stark",
                    Place = "Long Island"
                 }

        };
        private readonly DataContext _context;

        public  SupperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupperHero>>> Get()
        {
            return Ok(await _context.SupperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupperHero>> Get(int id)
        {
            var hero = await _context.SupperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SupperHero>>> AddHero(SupperHero hero)
        {
            _context.SupperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SupperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SupperHero>>> UpdateHero(SupperHero request)
        {
            var dbhero = await _context.SupperHeroes.FindAsync( request.Id);
            if (dbhero == null)
                return BadRequest("Hero not found.");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName; 
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();
            
            return Ok(await _context.SupperHeroes.ToListAsync());
        }
        
         
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SupperHero>>> Delete(int id)
        {
            var hero = heroes.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not found.");
            heroes.Remove(hero);
            return Ok(heroes);
        }

    }
}
