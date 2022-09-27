using System;
namespace GrowthHormone.App.Dominio;
public class RegistroPatron
{
   public int Id{get;set;}
   public DateTime FechaHora{get;set;}
   public float Valor{get;set;}
   public PatronesCrecimiento Crecimiento{get;set;}
}