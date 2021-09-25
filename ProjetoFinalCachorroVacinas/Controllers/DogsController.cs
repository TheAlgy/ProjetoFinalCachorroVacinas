using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCachorroVacinas.Data;
using ProjetoFinalCachorroVacinas.Models;
using System.Collections.Generic;

namespace ProjetoFinalCachorroVacinas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DBContext _context;

        public DogsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Dogs/ListDogs
        
        [HttpGet]
        [Route("ListDogs")]
        public List<Dog> list() => _context.Dogs.Include(x => x.DogVaccines).ToList();

        // GET: api/Dogs/5
        [HttpGet]
        [Route("GetDogById/{id}")]
        public IActionResult getid([FromRoute] int id)
        {
            var dog = _context.Dogs.Include(x => x.DogVaccines).Where(x => x.Id == id).ToList();


            if (dog == null)
            {
                return NotFound("Cachorro não encontrado!");
            }

            return Ok(dog);
        }

        // PUT: api/Dogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutDogById/{id}")]
        public IActionResult PutDog([FromRoute] int id, [FromBody] Dog dog)
        {
            if (id != dog.Id)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;
            //var OriginalDog = _context.Dogs.Include(x => x.DogVaccines).FirstOrDefault(x => x.Id == id);
            //OriginalDog = dog;
          

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PostDog")]

        public IActionResult PostDog([FromBody] Dog dog)
        {
            _context.Dogs.Add(dog);
             _context.SaveChangesAsync();

            return Created("", dog);
        }

        // DELETE: api/Dogs/5
        [HttpDelete]
        [Route("DeleteDogById/{id}")]

        public IActionResult DeleteDogById([FromRoute] int id)
        {
             var dog = _context.Dogs.Include(x => x.DogVaccines).FirstOrDefault(x => x.Id == id);
            if (dog == null)
            {
                return NotFound("Cachorro não encontrado!");
            }

            _context.Dogs.Remove(dog);
            _context.DogVaccines.RemoveRange(dog.DogVaccines);
            _context.SaveChanges();


            return Ok(_context.Dogs.Include(x => x.DogVaccines).ToList());
        }
        private bool DogExists(int id)
        {
            return _context.Dogs.Any(e => e.Id == id);
        }
    }
}
