﻿
using System;
using ProjetoFinalCachorroVacinas.Models;

namespace ProjetoSPRO.Model
{

    public class Person
    {

        public int Id { get; set; }

        public string PersonName { get; set; } 

        public DateTime Birth { get; set; }

        public string Gender { get; set; }

        public PostalAddress PostalAddress { get; set; } 
    }

 




}