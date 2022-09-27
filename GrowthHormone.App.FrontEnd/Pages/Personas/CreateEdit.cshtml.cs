using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrowthHormone.App.Persistencia;
using GrowthHormone.App.Dominio;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GrowthHormone.App.FrontEnd.Pages.Personas
{
    public class CreateEditModel : PageModel
    {
        private readonly IRepositorioEspecialista repositorioEspecialista;
        private readonly IRepositorioPaciente repositorioPaciente;
        private readonly IRepositorioFamiliar repositorioFamiliar;

        [BindProperty]
        public Especialista Especialista{get; set;}
        
        [BindProperty]
        public Paciente Paciente{get; set;}

        [BindProperty]
        public Familiar Familiar{get; set;}

        public string msn = "";
        [BindProperty]
        public string[] cities{get; set;}
        // public List<Parentesco> pacientes = new List<Parentesco>();
        [BindProperty]
        public IEnumerable<Paciente> Pacientes{get; set;}
        // Flags
        public int inserted = 0;

        public CreateEditModel(
            IRepositorioEspecialista _repositorioEspecialista, 
            IRepositorioPaciente _repositorioPaciente,
            IRepositorioFamiliar _repositorioFamiliar
            ){
            repositorioEspecialista = _repositorioEspecialista;
            repositorioPaciente = _repositorioPaciente;
            repositorioFamiliar = _repositorioFamiliar;
        }
        
        public void OnGet(int? personType, int? personId)
        {   
            /*
                Parámetro usado para editar una persona.
                personType: 
                    0 :  
                    1 : Paciente 
                    2 : Familiar
            */
            if(personType.HasValue && personId.HasValue){
                switch(personType){
                    case 0: break;
                    case 1: Paciente = repositorioPaciente.GetPaciente(personId.Value);
                            break; 
                    case 2: Familiar = repositorioFamiliar.GetFamiliar(personId.Value);
                            break;
                }
            }
            loadCities();
            Pacientes = repositorioPaciente.GetAllPacientes();
        }

        [HttpPost]
        public ActionResult OnPost(IFormCollection fc){
            // Verificaciones de la información que llega
            // ==================================
            /* Console.WriteLine("--->" + fc["ids"].ToString());
            Console.WriteLine("--->" + fc["parentescos"].ToString()); 
            Console.WriteLine("Tipo persona que llega (): " + fc["tipoPersona"].ToString()); */
            // ==================================
            
            var validation = validateForm(fc, fc["tipoPersona"].ToString());
            
            if(validation)
                try{
                    var tipoPersona =  fc["tipoPersona"].ToString();
                    switch(tipoPersona){
                        case "Especialista": // Agregar un especialista
                                            Especialista.Documento = fc["Documento"].ToString();
                                            Especialista.Nombre = fc["Nombre"].ToString();
                                            Especialista.Apellido = fc["Apellido"].ToString();
                                            Especialista.Telefono = fc["Telefono"].ToString();
                                            Especialista.Genero =  (Genero)Enum.Parse(typeof(Genero), fc["Genero"].ToString());
                                            repositorioEspecialista.AddEspecialista(Especialista);
                                            if(fc["IdPersona"].ToString() == ""){
                                            repositorioEspecialista.UpdateEspecialista(Especialista);
                                            }
                                            else{
                                                repositorioEspecialista.UpdateEspecialista(Especialista);
                                            }
                                            break;
                        case "Paciente":// Agregar un paciente
                                        Paciente.Documento = fc["Documento"].ToString();
                                        Paciente.Nombre = fc["Nombre"].ToString();
                                        Paciente.Apellido = fc["Apellido"].ToString();
                                        Paciente.Telefono = fc["Telefono"].ToString();
                                        Paciente.Genero =  (Genero)Enum.Parse(typeof(Genero), fc["Genero"].ToString());
                                        if(fc["IdPersona"].ToString() == ""){
                                            repositorioPaciente.AddPaciente(Paciente);
                                        }
                                        else{
                                            Paciente.Id = Int32.Parse(fc["IdPersona"].ToString());
                                            repositorioPaciente.UpdatePaciente(Paciente);
                                        }
                                        break;
                        case "Familiar":// Agregar un familiar
                                        Familiar.Documento = fc["Documento"].ToString();
                                        Familiar.Nombre = fc["Nombre"].ToString();
                                        Familiar.Apellido = fc["Apellido"].ToString();
                                        Familiar.Telefono = fc["Telefono"].ToString();
                                        Familiar.Genero =  (Genero)Enum.Parse(typeof(Genero), fc["Genero"].ToString());
                                        Familiar newFamiliar;
                                        if(fc["IdPersona"].ToString() == ""){
                                            newFamiliar = repositorioFamiliar.AddFamiliar(Familiar);
                                        }
                                        else{
                                            Familiar.Id = Int32.Parse(fc["IdPersona"].ToString());
                                            newFamiliar = repositorioFamiliar.UpdateFamiliar(Familiar);
                                        }

                                        // Agregar los pacientes familiares
                                        if(fc["ids"].ToString() != ""){
                                            addPacientes(newFamiliar, fc["ids"].ToString(), fc["parentescos"].ToString());
                                        }
                                        break;
                    }
                    inserted = 1;
                }
                catch(Exception e){
                    inserted = 2;
                    Console.Write(e);
                }
            else{
               inserted = 2; 
            }

            Pacientes = repositorioPaciente.GetAllPacientes(); // Se vuelven a cargar los hijos
            
            if(fc["IdPersona"].ToString() == ""){
                return Page();
            }
            else{
                return Redirect("/Personas/List");
            }
        }


        //  Método que actualiza los pacientes de un Familiar
        public void addPacientes(Familiar newFamiliar, string ids, string parentescos){
            List<Parentesco> pacientesParentesco = new List<Parentesco>();
            string[] idPacientes = ids.Split(","); // Divide la cadena de ids en un array
            string[] parentescoPacientes = parentescos.Split(","); // Divide la cadena de parentescos en un array

            var contArray = 1; // Bandera que contará las posiciones del array
            foreach(var id in idPacientes){
                if(contArray < idPacientes.Length){ // Esto es porque la última posicioń del array es un campo vacío
                    var paciente = repositorioPaciente.GetPaciente(Convert.ToInt32(id));
                    var parentesco = new Parentesco{
                        Paciente = paciente,
                        Vinculo = parentescoPacientes[contArray - 1]
                    };
                    pacientesParentesco.Add(parentesco); 
                }
                contArray++;
            }
            updateFamiliarPacientes(newFamiliar, pacientesParentesco);
        }

        // Método que actualiza los pacientes de un familiar y el familiar de un paciente
        private void updateFamiliarPacientes(Familiar familiar, List<Parentesco> pacientesParentesco){
            // var nuevofamiliar = _repoFamiliar.GetFamiliar(familiar.Id);
            familiar.Pacientes = pacientesParentesco;
            repositorioFamiliar.UpdateFamiliar(familiar); // Se actualizan los hijos del familiar

            // Actualización del Familiar en los hijos
            foreach(var paciente in pacientesParentesco){
                paciente.Paciente.Familiar = familiar;
                repositorioPaciente.UpdatePaciente(paciente.Paciente);

            }
        }

        // Método para validar la información que llega desde el formulario
        private bool validateForm(IFormCollection fc, string tipoPersona){
            var validation = false;
            if(
                fc["Documento"].ToString().Trim().Length > 0 &&
                fc["Nombre"].ToString().Trim().Length > 0 &&
                fc["Apellido"].ToString().Trim().Length > 0 &&
                fc["Telefono"].ToString().Trim().Length > 0

             )
                try{
                    if(Convert.ToDecimal(fc["Telefono"].ToString()) > 1000){
                    switch(tipoPersona){
                        case "Especialista":if(Especialista.Registro.Trim().Length > 0) {
                                                validation = true;
                                                msn = "Usuario Registrado";
                                            }
                                            else{
                                                msn = "Registro vacío";
                                            }
                                                
                                            break;  
                        case "Paciente":// TODO: Aquí hacer validaciones según Paciente
                                        validation = true;                                        
                                        break;  
                        case "Familiar":// TODO: Aquí hacer validaciones según Familiar 
                                        validation = true;
                                        break;  
                    }
                }
                else{
                    msn = "Número telefónico incorrecto";
                }

                }
                catch(Exception e){
                    Console.Write(e);
                    msn = "Formato telefónico incorrecto";
                }
                
            else{
                msn = "Todos los campos son obligatorios";
            }

            return validation;

        }

        // Método para cargar las ciudades
        private void loadCities(){
            StreamReader r = new StreamReader("wwwroot/json/colombia.json");
            string jsonString = r.ReadToEnd();
            jsonString = jsonString.Replace("\"", "");
            jsonString = jsonString.Replace("[", "");
            jsonString = jsonString.Replace("]", "");
            cities = jsonString.Split(',');
            Array.Sort(cities);
        }
    }
}
