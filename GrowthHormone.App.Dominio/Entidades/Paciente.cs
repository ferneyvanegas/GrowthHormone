using System;
namespace GrowthHormone.App.Dominio;
public class Paciente:Persona
{
   
   public string Direccion {get;set;}
   public float Latitud{get;set;}
   public float Longitud{get;set;}
   public string Ciudad {get;set;}
   public DateTime FechaNacimiento{get;set;}
   public Familiar? Familiar{get;set;}
   public Especialista? Endocrino{get;set;}
   public Especialista? Pediatra{get;set;}
   public Historia? Historia{get;set;}
}
