using System;
using System.Collections.Generic;


namespace ProjetoSPRO.Model
{

    
    public class Dog
    {


        public int Id { get; set; }
        public string PetName { get; set; }

        public int IdDog { get; set; }

        public string DogName { get; set; } 

        public DateTime Birth { get; set; }

        public string Gender { get; set; }

        public List<DogVaccines> DogVaccines { get; set; } 

    }

}