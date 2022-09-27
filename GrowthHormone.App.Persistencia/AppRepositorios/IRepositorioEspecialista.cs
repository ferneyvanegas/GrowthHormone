using System.Collections.Generic;
using GrowthHormone.App.Dominio;

namespace GrowthHormone.App.Persistencia
{
    public interface IRepositorioEspecialista
    {
        IEnumerable<Especialista> GetAllEspecialistas();

        Especialista AddEspecialista(Especialista especialista);

        Especialista UpdateEspecialista(Especialista especialista);

        void DeleteEspecialista(int idEspecialista );

        Especialista GetEspecialista(int idEspecialista);

    }
}