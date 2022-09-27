using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class DetailModel : PageModel
    {

        private readonly IRepositorioPaciente RepositorioPaciente;
        private readonly IRepositorioEspecialista RepositorioEspecialista;
        [BindProperty]
        public Paciente Paciente{get; set;}
        
        [BindProperty]
        public List<Especialista> Especialistas{get; set;}

        public DetailModel(
            IRepositorioPaciente RepositorioPaciente,
            IRepositorioEspecialista RepositorioEspecialista
            ){
            this.RepositorioPaciente = RepositorioPaciente;
            this.RepositorioEspecialista = RepositorioEspecialista;
        }
        
        public void OnGet()
        {

        }

        [HttpPost]
        public IActionResult OnPost(IFormCollection fc)
        {
            if(fc["idPaciente"].ToString().Trim().Equals("")){
                return RedirectToPage("/Personas/NotFoundPerson");
            }
            else{
                var idPaciente =  Int32.Parse(fc["idPaciente"].ToString());
                Paciente=RepositorioPaciente.GetPaciente(idPaciente);
                if(Paciente==null){
                    return RedirectToPage("/Personas/NotFoundPerson");
                }
                else{
                    if(Paciente.Endocrino == null)
                        Especialistas.Add(null);
                    else
                        Especialistas.Add(RepositorioEspecialista.GetEspecialista(Paciente.Endocrino.Id));

                    if(Paciente.Pediatra == null)
                        Especialistas.Add(null);
                    else
                        Especialistas.Add(RepositorioEspecialista.GetEspecialista(Paciente.Pediatra.Id));
                    return Page();
                }
            }
        }
    }
}
