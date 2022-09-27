using System.Collections.Generic;

namespace GrowthHormone.App.Dominio;
public class Historia
{
   public int Id{get;set;}
   // public int Diagnostico {get;set;}
   public List<Cuidados>? Cuidados{get;set;}
   public List<RegistroPatron>? RegistrosPatrones{get; set;}
}
