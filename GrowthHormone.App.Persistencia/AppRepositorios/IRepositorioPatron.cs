using System.Collections.Generic;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public interface IRepositorioPatron
    {
        RegistroPatron GetPatron(int idPatron);
    }
}