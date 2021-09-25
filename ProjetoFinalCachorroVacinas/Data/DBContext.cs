using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCachorroVacinas.Models;

namespace ProjetoFinalCachorroVacinas.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<DogVaccines> DogVaccines { get; set; }
    }
}
