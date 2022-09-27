using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public class RepositorioHistoria : IRepositorioHistoria
    {
        private readonly ApplContext _appContext;
        public RepositorioHistoria(ApplContext appContext){
            _appContext = appContext;
        }

        public Historia AddHistory(Historia history)
        {
            var historiaAdicionada = _appContext.Historia.Add(history);
           _appContext.SaveChanges();
           return historiaAdicionada.Entity;
        }

        public Historia GetHistory(int idHistoria)
        {
            return _appContext.Historia.Where(h => h.Id == idHistoria).Include(h => h.RegistrosPatrones).FirstOrDefault();
        }

        public Historia AddPatron(int idHistoria, RegistroPatron patron){
            var historia = _appContext.Historia.FirstOrDefault(h => h.Id == idHistoria);
            if(historia != null){
                if(historia.RegistrosPatrones == null){
                    List<RegistroPatron> registroPatrones = new List<RegistroPatron>();
                    registroPatrones.Add(patron);
                    historia.RegistrosPatrones = registroPatrones;
                }
                else{
                    historia.RegistrosPatrones.Add(patron);
                }
                _appContext.SaveChanges();
            }
            return historia;
        }
    }
}