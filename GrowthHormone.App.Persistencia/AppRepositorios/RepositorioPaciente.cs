using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public class RepositorioPaciente : IRepositorioPaciente
    {
        private readonly ApplContext _appContext;
        public RepositorioPaciente(ApplContext appContext){
            _appContext= appContext;
        }

        public IEnumerable<Paciente> GetAllPacientes(){
                return _appContext.Paciente;
        }

        public Paciente AddPaciente(Paciente paciente){
           var pacienteAdicionado =_appContext.Paciente.Add(paciente);
           _appContext.SaveChanges();
           return pacienteAdicionado.Entity; 
        }

        public Paciente UpdatePaciente(Paciente paciente){
            var pacienteEncontrado=_appContext.Paciente.FirstOrDefault(p=>p.Id==paciente.Id);
            if(pacienteEncontrado!=null){
                pacienteEncontrado.Nombre=paciente.Nombre;
                pacienteEncontrado.Apellido=paciente.Apellido;
                pacienteEncontrado.Telefono=paciente.Telefono;
                pacienteEncontrado.Documento=paciente.Documento;
                pacienteEncontrado.Genero=paciente.Genero;
                pacienteEncontrado.Direccion=paciente.Direccion;
                pacienteEncontrado.Latitud=paciente.Latitud;
                pacienteEncontrado.Longitud=paciente.Longitud;
                pacienteEncontrado.Ciudad=paciente.Ciudad;
                pacienteEncontrado.FechaNacimiento=paciente.FechaNacimiento;
                pacienteEncontrado.Familiar=paciente.Familiar;
                _appContext.SaveChanges();
           }
           return pacienteEncontrado;
            
        }

        public void DeletePaciente(int idPaciente ){
             var pacienteEncontrado =_appContext.Paciente.FirstOrDefault(p => p.Id == idPaciente);
             if(pacienteEncontrado == null){
                return;
            }
             _appContext.Paciente.Remove(pacienteEncontrado);
             _appContext.SaveChanges();
        }
         
        public Paciente GetPaciente(int idPaciente){
            return _appContext.Paciente.Where(p => p.Id == idPaciente).Include(p => p.Historia).FirstOrDefault();
        }

        public Paciente AsignarEspecialista(int idPaciente, int idEspecialista){
            var paciente= _appContext.Paciente.FirstOrDefault(p => p.Id == idPaciente);
            if(paciente != null){
                var especialista= _appContext.Especialista.FirstOrDefault(e => e.Id == idEspecialista);
                if(especialista!=null){
                    if(especialista.Especialidad == "Endocrino")
                        paciente.Endocrino = especialista;
                    else
                        paciente.Pediatra = especialista;
                    _appContext.SaveChanges();
                }
            }
            return paciente;

        }

        public IEnumerable<Paciente> GetAllPacientesBySpecialist(int idSpecialist){
                return _appContext.Paciente.Where(p => p.Pediatra.Id == idSpecialist || p.Endocrino.Id == idSpecialist );
        }

        public void AsignarHistoria(int idPaciente, int idHistoria){
            var paciente = _appContext.Paciente.FirstOrDefault(p => p.Id == idPaciente);
            if(paciente != null){
                var historia = _appContext.Historia.FirstOrDefault(e => e.Id == idHistoria);
                if(historia!=null){
                    paciente.Historia = historia;
                    _appContext.SaveChanges();
                }
            }
        }

        public Historia GetHistoria(int idPaciente){
            var historia = new Historia();
            var paciente = _appContext.Paciente.Where(p => p.Id == idPaciente).Include(p => p.Historia).FirstOrDefault();;
            if(paciente != null){
                historia = paciente.Historia;
            }
            return historia;
        }
    }
}
