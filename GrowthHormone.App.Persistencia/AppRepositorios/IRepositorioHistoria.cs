using System.Collections.Generic;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public interface IRepositorioHistoria
    {
        Historia AddHistory(Historia history);
        Historia GetHistory(int idHistoria);
        Historia AddPatron(int idHistoria, RegistroPatron patron);
    }
}