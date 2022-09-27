using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowthHormone.App.Dominio;
using GrowthHormone.App.Persistencia;

namespace GrowthHormone.App.Consola
{
    public class Familiares
    {
        private static IRepositorioPaciente _repoPaciente = new RepositorioPaciente(new ApplContext());
        private static IRepositorioFamiliar _repoFamiliar = new RepositorioFamiliar(new ApplContext());

        // Prueba de inserción de Familiar: Funcional
        public static Familiar addFamiliar(int idPacienteParentesco, string vinculo){
            var familiar = new Familiar{
                Nombre = "Jeniffer",
                Apellido = "Cardona",
                Documento = "5305353042",
                Telefono = "530648254",
                Genero = Genero.Femenino,
                Correo = "adnev.aqw@gose.sed.co"
            };

            List<Parentesco> pacientes = new List<Parentesco>();
            var paciente = _repoPaciente.GetPaciente(idPacienteParentesco);
            var parentesco = new Parentesco{
                Paciente = paciente,
                Vinculo = vinculo
            };

            pacientes.Add(parentesco); 

            var newFamiliar = _repoFamiliar.AddFamiliar(familiar); // Recibe el nuevo familiar creado
            updateFamiliarPacientes(newFamiliar, pacientes);
            return newFamiliar;
        }

        // Método que actualiza los pacientes de un familiar y el familiar de un paciente
        private static void updateFamiliarPacientes(Familiar familiar, List<Parentesco> pacientes){
            // var nuevofamiliar = _repoFamiliar.GetFamiliar(familiar.Id);
            familiar.Pacientes = pacientes;
            _repoFamiliar.UpdateFamiliar(familiar); // Se actualizan los hijos del familiar

            // Actualización del Familiar en los hijos
            foreach(var paciente in pacientes){
                paciente.Paciente.Familiar = familiar;
                _repoPaciente.UpdatePaciente(paciente.Paciente);

            }
        }
    }
}