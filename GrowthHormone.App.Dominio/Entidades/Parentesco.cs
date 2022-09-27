using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthHormone.App.Dominio
{
    public class Parentesco
    {
        public int Id{get; set;}
        public Paciente Paciente{get; set;}
        public string Vinculo{get; set;}        
    }
}