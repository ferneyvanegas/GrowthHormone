using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;


namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class AsignarEspecialistaModel: PageModel 
    {
        private readonly IRepositorioEspecialista _repositorioEspecialista;
        private readonly IRepositorioPaciente _repositorioPaciente;

        
        public Paciente  Paciente {get; set;}
        [BindProperty]
        public IEnumerable<Especialista>Especialistas {get; set;}
        

        public AsignarEspecialistaModel(
            IRepositorioEspecialista repositorioEspecialista, 
            IRepositorioPaciente repositorioPaciente 
        ) {
            _repositorioEspecialista = repositorioEspecialista;
            _repositorioPaciente = repositorioPaciente;

        }
        public void OnGet(int id)
        {
            Paciente = _repositorioPaciente.GetPaciente(id);
            Especialistas = _repositorioEspecialista.GetAllEspecialistas();
        }

        public IActionResult OnPost(int idPaciente, int endocrino, int pediatra){
            if(endocrino != 0){
                _repositorioPaciente.AsignarEspecialista(idPaciente, endocrino);
            }

            if(pediatra != 0){
                _repositorioPaciente.AsignarEspecialista(idPaciente, pediatra);
            }
            return Redirect("/Personas/List");
        }

    }


}
