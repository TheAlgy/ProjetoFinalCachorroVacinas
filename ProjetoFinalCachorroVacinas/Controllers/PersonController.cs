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
    public class PersonController : Controller
    {
        private readonly DBContext _context;

        public PersonController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Person/ListPersons

        [HttpGet]
        [Route("ListPerson")]
        public List<Person> listPerson() => _context.Persons.Include(x => x.DogList).ThenInclude(c => c.DogVaccines).ToList();

        // GET: api/Dogs/5
        [HttpGet]
        [Route("GetPersonById/{id}")]
        public IActionResult getPersonById([FromRoute] int id)
        {
            var person = _context.Persons.Include(x => x.DogList).ThenInclude(c => c.DogVaccines).Where(x => x.Id == id).ToList();


            if (person == null)
            {
                return NotFound("Pessoa não encontrada!");
            }

            return Ok(person);
        }

        // PUT: api/Dogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutPersonById/{id}")]
        public IActionResult PutPersonById([FromRoute] int id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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
        [Route("PostPerson")]

        public IActionResult PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChangesAsync();

            return Created("", person);
        }

        // DELETE: api/Person/5
        [HttpDelete]
        [Route("DeletePersonById/{id}")]

        public IActionResult DeletePersonById([FromRoute] int id)
        {
            var person = _context.Persons.Include(x => x.DogList).ThenInclude(c => c.DogVaccines).FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound("Pessoa não encontrada!");
            }

            _context.Persons.Remove(person);
            _context.Dogs.RemoveRange(person.DogList);
            //_context.DogVaccines.Remove(person.DogList.DogVaccines);
            _context.SaveChanges();


            return Ok(_context.Dogs.Include(x => x.DogVaccines).ToList());
        }
        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
