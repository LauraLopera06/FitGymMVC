using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class EntrenadorServicio : IEntrenadorServicio
    {
        private readonly IUsuariosRepositorio _repository;
        private readonly IUsuariosServicio _uservicio;

        public EntrenadorServicio(IUsuariosRepositorio repository, IUsuariosServicio uservicio)
        {
            _repository = repository;//inyeccion de dependencias
            _uservicio = uservicio;
        }
        public List<UsuariosModel> Listar()
        {
            return _uservicio.Listar().Where(u => u.TipoUsuario == "Entrenador").ToList(); 

        }


    }
}