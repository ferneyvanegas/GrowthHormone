using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowthHormone.App.Dominio;
using GrowthHormone.App.Persistencia;

namespace GrowthHormone.App.Consola
{
    public class Especialistas
    {
        private static IRepositorioEspecialista _repoEspecialista = new RepositorioEspecialista(new ApplContext());
        public static Especialista addEspecialista(string especialidad, string registro){
            var especialista = new Especialista{
                Nombre = "Carlos",
                Apellido = "Pérez",
                Documento = "0435635938",
                Telefono = "3042583692",
                Genero = Genero.Masculino,
                Especialidad = especialidad,
                Registro = registro
            };
            var nuevoEspecialista = _repoEspecialista.AddEspecialista(especialista); 
            return nuevoEspecialista;
        }
    }
}