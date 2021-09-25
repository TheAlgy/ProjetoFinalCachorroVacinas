using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCachorroVacinas.Data;
using ProjetoFinalCachorroVacinas.Models;

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
        public IActionResult GetDogById([FromRoute] int id)
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
        public IActionResult PutDogById([FromRoute] int id, [FromBody] Dog dog)
        {
            if (id != dog.Id)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;
          

            try
            {
                _context.SaveChanges();
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
             _context.SaveChanges();

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


        // GET: api/Dogs/InsertDogByPersonId
        [HttpPost]
        [Route("InsertDogByPersonId/{id}")]
        public IActionResult InsertDogByPersonId([FromRoute] int id, [FromBody] Dog dog) 
        {
            var person = _context.Persons.Include(x => x.DogList).Where(x => x.Id == id).First();
            person.DogList.Add(dog);
            _context.Dogs.Add(dog);
            _context.Persons.Update(person);
            _context.SaveChanges();
            return Ok(person);
           
           // _context.Persons.Include(x => x.DogList).ToList()
        }

    }
}
