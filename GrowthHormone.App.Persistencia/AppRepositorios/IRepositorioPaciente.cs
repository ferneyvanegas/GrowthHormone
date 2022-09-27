using System.Collections.Generic;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public interface IRepositorioPaciente
    {
        IEnumerable<Paciente> GetAllPacientes();
        Paciente AddPaciente(Paciente paciente);
        Paciente UpdatePaciente(Paciente paciente);
        void DeletePaciente(int idPaciente );
        Paciente GetPaciente(int idPaciente);
        Paciente AsignarEspecialista (int idPaciente, int idEspecialista);
        IEnumerable<Paciente> GetAllPacientesBySpecialist(int idSpecialist);
        void AsignarHistoria(int idPaciente, int idHistoria);
        Historia GetHistoria(int idPaciente);
    }
}

