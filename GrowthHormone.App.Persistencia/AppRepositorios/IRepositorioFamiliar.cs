using System.Collections.Generic;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public interface IRepositorioFamiliar
    {
        IEnumerable<Familiar> GetAllFamiliares();

        Familiar AddFamiliar(Familiar familiar);

        Familiar UpdateFamiliar(Familiar familiar);

        void DeleteFamiliar(int idFamiliar );

        Familiar GetFamiliar(int idFamiliar);

    }
}