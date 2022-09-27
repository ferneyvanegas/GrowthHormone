using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthHormone.App.Consola
{
    public class Menu
    {
        public static void showMenu(){

            var option = 0;
            while(option != 6){
                Console.WriteLine("Menú");
                Console.WriteLine("Escoge una opción (Ej: 1):");
                Console.WriteLine("1. TEST: Registrar Paciente de prueba\n"
                                + "2. TEST: Registrar Especialista de prueba\n"
                                + "3. TEST: Registrar Familiar de prueba\n" 
                                + "4. TEST: Consultar todos los pacientes\n"
                                + "5. TEST: Consultar Paciente por ID\n"
                                + "6. Salir");
                option = Convert.ToInt32(Console.ReadLine());

                switch(option){
                    case 1: Console.WriteLine("Agregando paciente de prueba...");
                            var nuevoPaciente = Pacientes.addPaciente();
                            Console.WriteLine("Se ha ingresado un paciente de prueba con ID #" + nuevoPaciente.Id);
                            Console.ReadLine();
                            break;
                    
                    case 2: Console.WriteLine("Escoja Especialidad(Ej: 2):");
                            Console.WriteLine("1. Pediatra\n"
                                            + "2. Endocrino\n");
                            var esp = Console.ReadLine();
                            var especialidad = "";
                            if(esp.Equals("1"))
                                especialidad = "Pediatra";
                            else
                                especialidad = "Endocrino";
                            Console.WriteLine("Por favor ingresa el registro único de talento humano\n");
                            var registro = Console.ReadLine();
                            var nuevoEspacialista = Especialistas.addEspecialista(especialidad, registro);
                            Console.WriteLine("Se ha ingresado un especialista " + especialidad + " de prueba con ID #" + nuevoEspacialista.Id);
                            Console.ReadLine();
                            break;
                    
                    case 3: Console.WriteLine("Dijite el ID del Paciente\n");
                            var idPacienteParentesco = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Dijite el parentesco");
                            var vinculo = Console.ReadLine();
                            Console.WriteLine("Agregando familiar de prueba...");
                            var nuevoFamiliar = Familiares.addFamiliar(idPacienteParentesco, vinculo);
                            Console.WriteLine("Se ha ingresado un familiar de prueba con ID #" + nuevoFamiliar.Id);
                            Console.ReadLine();
                            break;

                    case 4: Console.WriteLine("Buscando Pacientes...\n");
                            Pacientes.getAllPacientes();
                            Console.WriteLine("Búsqueda finalizada");
                            Console.ReadLine();
                            break;

                    case 5: Console.WriteLine("Ingrese el ID del paciente (niñ@)\n");
                            var idPaciente = Convert.ToInt32(Console.ReadLine());
                            Pacientes.getPaciente(idPaciente); 
                            Console.ReadLine();
                            break;
                    
                    case 7: Console.WriteLine("Activando inserción masiva...");
                            Pacientes.addManyPacientes();
                            Console.WriteLine("Finalizando inserción masiva satisfactoriamente");
                            break;
                }
            }
        }        
    }
}