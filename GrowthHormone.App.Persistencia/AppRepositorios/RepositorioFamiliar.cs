using System.Collections.Generic;
using System.Linq;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public class RepositorioFamiliar : IRepositorioFamiliar
    {
        private readonly ApplContext _appContext;
        public RepositorioFamiliar(ApplContext appContext){
            _appContext= appContext;
        }

        public IEnumerable<Familiar> GetAllFamiliares(){
                return _appContext.Familiar;
        }
        
        public Familiar AddFamiliar(Familiar familiar){
           var familiarAdicionado =_appContext.Familiar.Add(familiar);
           _appContext.SaveChanges();
           return familiarAdicionado.Entity; 
        }

        public Familiar UpdateFamiliar(Familiar familiar){
            var familiarEncontrado=_appContext.Familiar.FirstOrDefault(p => p.Id == familiar.Id);
            if(familiarEncontrado!=null){
                familiarEncontrado.Nombre=familiar.Nombre;
                familiarEncontrado.Apellido=familiar.Apellido;
                familiarEncontrado.Telefono=familiar.Telefono;
                familiarEncontrado.Documento=familiar.Documento;
                familiarEncontrado.Genero=familiar.Genero;
                familiarEncontrado.Correo=familiar.Correo;
                familiarEncontrado.Pacientes=familiar.Pacientes;
                _appContext.SaveChanges();
           }
           return familiarEncontrado;
            
        }

        public void DeleteFamiliar(int idFamiliar ){
             var familiarEncontrado =_appContext.Familiar.FirstOrDefault(p => p.Id == idFamiliar);
             if(familiarEncontrado == null){
                return;
            }
             _appContext.Familiar.Remove(familiarEncontrado);
             _appContext.SaveChanges();
        }
         
        public Familiar GetFamiliar(int idFamiliar){
            return _appContext.Familiar.FirstOrDefault(p => p.Id == idFamiliar);
        }
    }
}
