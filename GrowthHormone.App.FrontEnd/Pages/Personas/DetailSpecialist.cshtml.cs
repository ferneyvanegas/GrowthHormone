using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class DetailSpecialistModel : PageModel
    {

        private readonly IRepositorioEspecialista repositorioEspecialista;
        private readonly IRepositorioPaciente repositorioPaciente;
        public Especialista Especialista{get; set;}
        public IEnumerable<Paciente> Pacientes{get; set;}

        // Banderas
        public int numPacientes;
        
        public DetailSpecialistModel(
            IRepositorioEspecialista _repositorioEspecialista,
            IRepositorioPaciente _repositorioPaciente
            ){
            repositorioPaciente = _repositorioPaciente;
            repositorioEspecialista = _repositorioEspecialista;

            numPacientes = 0;
        }
        public void OnGet()
        {
        }

        [HttpPost]
        public IActionResult OnPost(IFormCollection fc)
        {
            if(fc["idEspecialista"].ToString().Trim().Equals("")){
                return RedirectToPage("/Personas/NotFoundPerson");
            }
            else{
                var idEspecialista =  Int32.Parse(fc["idEspecialista"].ToString());
                Especialista = repositorioEspecialista.GetEspecialista(idEspecialista);
                if(Especialista==null){
                    return RedirectToPage("/Personas/NotFoundPerson");
                }
                else{
                    Pacientes = repositorioPaciente.GetAllPacientesBySpecialist(idEspecialista);
                    numPacientes = Pacientes.Count();
                    return Page();
                }
            }
        }
    }
}
