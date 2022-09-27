using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowthHormone.App.Dominio;
using GrowthHormone.App.Persistencia;
using Newtonsoft.Json;

namespace GrowthHormone.App.Consola
{
    public class Pacientes
    {
        private static IRepositorioPaciente _repoPaciente = new RepositorioPaciente(new ApplContext());

        // Prueba de inserción de Paciente: Funcional

        public static Paciente addPaciente(){
            var paciente = new Paciente{
                Nombre = "Nicolas",
                Apellido = "Ruiz",
                Documento = "1523458754",
                Telefono = "343434",
                Genero = Genero.Femenino,
                Direccion = "Av. Plaza Américas",
                Latitud = -75.343F,
                Longitud = 5.0343F,
                Ciudad = "Bogotá",
                FechaNacimiento = Convert.ToDateTime("2022-01-01")
            };
            var nuevoPaciente = _repoPaciente.AddPaciente(paciente); 
            return nuevoPaciente;
        }
        
        // Prueba de búsqueda de Paciente por ID: Funcional
        public static void getPaciente(int idPaciente){
            var paciente = _repoPaciente.GetPaciente(idPaciente);
            Console.WriteLine("PACIENTE\n"
                            + "================================\n"
                            + "ID: " + paciente.Id + "\n"
                            + "Nombre y Apellido: " + paciente.Nombre + " " + paciente.Apellido + "\n"
                            + "Género: " + paciente.Genero + "\n"
                            + "Teléfono: " + paciente.Telefono + "\n"
                            + "Fecha Nacimiento: " + paciente.FechaNacimiento + "\n"
                            + "Dirección: " + paciente.Direccion + "\n"
                            + "Ciudad: " + paciente.Ciudad + "\n"
                            + "Latitud: " + paciente.Latitud + "\n"
                            + "Longitud: " + paciente.Longitud + "\n"
                            + "================================\n"
                            );
        }

        // Prueba de búsqueda de todos los Pacientes (Creacion lista pacientes): Funcional
        public static void getAllPacientes(){
            var pacientes = _repoPaciente.GetAllPacientes();
            var cont = 1;
            foreach(var paciente in pacientes){
                Console.WriteLine("PACIENTE #" + cont + ":\n"
                            + "================================\n"
                            + "ID: " + paciente.Id + "\n"
                            + "Nombre y Apellido: " + paciente.Nombre + " " + paciente.Apellido + "\n"
                            + "Teléfono: " + paciente.Telefono + "\n"
                            + "Dirección: " + paciente.Direccion + "\n"
                            + "================================\n"
                            );
                cont++;
            }
        }

        // Método que busca archivos json y los convierte en Pacientes
        public static void addManyPacientes(){
            for(var x = 1; x <=3; x++){
                StreamReader r = new StreamReader("json/Paciente_" + x + ".json");
                string jsonString = r.ReadToEnd();
                Paciente paciente = JsonConvert.DeserializeObject<Paciente>(jsonString);
                var nuevoPaciente = _repoPaciente.AddPaciente(paciente); 
            }
            
        }
    }
}