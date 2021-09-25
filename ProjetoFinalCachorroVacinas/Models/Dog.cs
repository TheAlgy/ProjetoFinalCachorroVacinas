using System;
using System.Collections.Generic;


namespace ProjetoFinalCachorroVacinas.Models
{

    
    public class Dog
    {


        public int Id { get; set; }
        public string DogRace { get; set; }

        public string DogName { get; set; } 

        public string Gender { get; set; }

        public List<DogVaccines> DogVaccines { get; set; } 

    }

}