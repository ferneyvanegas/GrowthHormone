using System.Collections.Generic;
using System.Linq;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public class RepositorioPatron : IRepositorioPatron
    {
        private readonly ApplContext _appContext;
        public RepositorioPatron(ApplContext appContext){
            _appContext = appContext;
        }
        public RegistroPatron GetPatron(int idPatron)
        {
            return _appContext.RegistroPatron.FirstOrDefault(r => r.Id == idPatron);
        }
    }
}