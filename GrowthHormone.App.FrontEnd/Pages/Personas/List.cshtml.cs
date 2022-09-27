using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class ListModel : PageModel
    {
        private readonly IRepositorioPaciente repositorioPaciente;
        private readonly IRepositorioFamiliar repositorioFamiliar;
        private readonly IRepositorioEspecialista repositorioEspecialista;

        public IEnumerable<Paciente> Pacientes{get; set;}
        public IEnumerable<Familiar> Familiares{get; set;}
        public IEnumerable<Especialista> Especialistas{get; set;}

        public ListModel(
            IRepositorioPaciente _repositorioPaciente,
            IRepositorioFamiliar _repositorioFamiliar,
            IRepositorioEspecialista _repositorioEspecialista
            ){
            repositorioPaciente = _repositorioPaciente;
            repositorioFamiliar = _repositorioFamiliar;
            repositorioEspecialista = _repositorioEspecialista;
        }
        public void OnGet()
        {
            Pacientes = repositorioPaciente.GetAllPacientes();
            Familiares = repositorioFamiliar.GetAllFamiliares();
            Especialistas = repositorioEspecialista.GetAllEspecialistas();
        }
    }
}
