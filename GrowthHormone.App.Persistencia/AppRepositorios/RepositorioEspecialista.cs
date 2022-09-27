using System.Collections.Generic;
using System.Linq;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public class RepositorioEspecialista : IRepositorioEspecialista
    {
        private readonly ApplContext _appContext;
        public RepositorioEspecialista(ApplContext appContext){
            _appContext= appContext;
        }

        public IEnumerable<Especialista> GetAllEspecialistas(){
                return _appContext.Especialista;
        }
        
        public Especialista AddEspecialista(Especialista especialista){
           var especialistaAdicionado =_appContext.Especialista.Add(especialista);
           _appContext.SaveChanges();
           return especialistaAdicionado.Entity; 
        }

        public Especialista UpdateEspecialista(Especialista especialista){
            var especialistaEncontrado=_appContext.Especialista.FirstOrDefault(p => p.Id == especialista.Id);
            if(especialistaEncontrado!=null){
                especialistaEncontrado.Nombre=especialista.Nombre;
                especialistaEncontrado.Apellido=especialista.Apellido;
                especialistaEncontrado.Telefono=especialista.Telefono;
                especialistaEncontrado.Documento=especialista.Documento;
                especialistaEncontrado.Genero=especialista.Genero;
                especialistaEncontrado.Especialidad=especialista.Especialidad;
                especialistaEncontrado.Registro=especialista.Registro;
                _appContext.SaveChanges();
           }
           return especialistaEncontrado;
            
        }

        public void DeleteEspecialista(int idEspecialista){
             var especialistaEncontrado =_appContext.Especialista.FirstOrDefault(p => p.Id == idEspecialista);
             if(especialistaEncontrado == null){
                return;
            }
             _appContext.Especialista.Remove(especialistaEncontrado);
             _appContext.SaveChanges();
        }
         
        public Especialista GetEspecialista(int idEspecialista){
            return _appContext.Especialista.FirstOrDefault(p => p.Id == idEspecialista);
        }

    }
}