using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : ControllerBase
    {
        private DataContext _context;

        public SuperHeroiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroi>>> Get()
        {
            return Ok(await _context.SuperHerois.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroi>> Get(int id)
        {
            var heroi = await _context.SuperHerois.FindAsync(id);
            if (heroi == null)
            {
                return BadRequest("Heroi não encontrado. (¯ヘ¯)");
            }
            return Ok(heroi);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroi>>> AddHeroi(SuperHeroi heroi)
        {
            _context.SuperHerois.Add(heroi);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHerois.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHeroi>>> UpdateHeroi(SuperHeroi request)
        {
            var dbheroi = await _context.SuperHerois.FindAsync(request.Id);
            if (dbheroi == null)
            {
                return BadRequest("Heroi não encontrado. (¯ヘ¯)");
            }

            dbheroi.Nome = request.Nome;
            dbheroi.PrimeiroNome = request.PrimeiroNome;
            dbheroi.SegundoNome = request.SegundoNome;
            dbheroi.Lugar = request.Lugar;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHerois.ToListAsync());
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHeroi>>> Delete(int id)
        {
            var dbheroi = await _context.SuperHerois.FindAsync(id);
            if (dbheroi == null)
            {
                return BadRequest("Heroi não encontrado. (¯ヘ¯)");
            }
            _context.SuperHerois.Remove(dbheroi);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHerois.ToListAsync());

        }
    }
}
