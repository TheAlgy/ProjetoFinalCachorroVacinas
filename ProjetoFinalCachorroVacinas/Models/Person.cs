
using System;
using System.Collections.Generic;

namespace ProjetoFinalCachorroVacinas.Models
{

    public class Person
    {

        public int Id { get; set; }

        public string PersonName { get; set; } 

        public string Gender { get; set; }

        public List<Dog> DogList { get; set; }
    }

 




}