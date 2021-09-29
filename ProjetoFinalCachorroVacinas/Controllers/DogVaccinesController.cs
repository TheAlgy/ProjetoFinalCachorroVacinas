using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCachorroVacinas.Data;
using ProjetoFinalCachorroVacinas.Models;
using System.Collections.Generic;
using System.Linq;


namespace ProjetoFinalCachorroVacinas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogVaccinesController : Controller
    {
        private readonly DBContext _context;

        public DogVaccinesController(DBContext context)
        {
            _context = context;
        }


        // GET: api/DogVaccines/ListDogVaccines
        [HttpGet]
        [Route("ListDogVaccines")]
        public List<DogVaccines> list() => _context.DogVaccines.ToList();

        // PUT: api/DogVaccines/PutDogVaccinesById/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutDogVaccinesById/{id}")]
        public IActionResult PutDogVaccinesById([FromRoute] int id, [FromBody] DogVaccines dogvaccines)
        {
            if (id != dogvaccines.Id)
            {
                return BadRequest();
            }

            _context.Entry(dogvaccines).State = EntityState.Modified;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogVaccinesExists(id))
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
        private bool DogVaccinesExists(int id)
        {
            return _context.DogVaccines.Any(e => e.Id == id);
        }


        // GET: api/DogVaccines/InsertDogVaccinesByIdDog/{id}
        [HttpPost]
        [Route("InsertDogVaccinesByIdDog/{id}")]
        public IActionResult InsertDogVaccinesByIdDog([FromRoute] int id, [FromBody] DogVaccines dogvaccines)
        {
            var dogs = _context.Dogs.Include(x => x.DogVaccines).Where(x => x.Id == id).First();
            dogs.DogVaccines.Add(dogvaccines);
            _context.DogVaccines.Add(dogvaccines);
            _context.Dogs.Update(dogs);
            _context.SaveChanges();
            return Ok(dogs);

            // _context.Persons.Include(x => x.DogList).ToList()
        }

        // DELETE: api/DogVaccines/DeleteDogVaccinesById/{id}
        [HttpDelete]
        [Route("DeleteDogVaccinesById/{id}")]

        public IActionResult DeleteDogVaccinesById([FromRoute] int id)
        {
            var dogvaccines = _context.DogVaccines.Find(id);
            if (dogvaccines == null)
            {
                return NotFound("Vacina não encontrada!");
            }

            _context.DogVaccines.Remove(dogvaccines);
            _context.SaveChanges();


            return Ok(_context.DogVaccines.ToList());
        }
        
    }

}
