using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class HistoryModel : PageModel
    {
        private readonly IRepositorioPaciente repositorioPaciente;
        private readonly IRepositorioHistoria repositorioHistoria;
        [BindProperty]
        public Paciente  Paciente {get; set;}
        [BindProperty]
        public Historia Historia {get; set;}
        [BindProperty]
        public List<RegistroPatron> RegistrosPatrones {get; set;}
        
        public HistoryModel(
            IRepositorioPaciente _repositorioPaciente,
            IRepositorioHistoria _repositorioHistoria
        ) {
            repositorioPaciente = _repositorioPaciente;
            repositorioHistoria = _repositorioHistoria;
        }
        public void OnGet(int? pacienteId)
        {
            if(pacienteId.HasValue){
                Paciente = repositorioPaciente.GetPaciente(pacienteId.Value);
                if(Paciente.Historia != null)
                    Historia = repositorioHistoria.GetHistory(Paciente.Historia.Id);
            }
        }

        public IActionResult OnPost(int idPaciente, string patron, float patronValue){
            Paciente = repositorioPaciente.GetPaciente(idPaciente);
            if(Paciente.Historia == null){
                Historia = repositorioHistoria.AddHistory(Historia);
                repositorioPaciente.AsignarHistoria(idPaciente, Historia.Id);
                Paciente = repositorioPaciente.GetPaciente(idPaciente);
            }
            else{
                Historia = Paciente.Historia;
            }
            // Patrón para agregar
            var registroPatron = new RegistroPatron();
            registroPatron.Valor = patronValue;
            registroPatron.Crecimiento = (PatronesCrecimiento)Enum.Parse(typeof(PatronesCrecimiento), patron);
            registroPatron.FechaHora = DateTime.Today;
            repositorioHistoria.AddPatron(Historia.Id, registroPatron);
            
            return Page();
        }
    }
}
