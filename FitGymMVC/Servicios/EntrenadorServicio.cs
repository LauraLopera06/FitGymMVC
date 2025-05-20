using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class EntrenadorServicio : IEntrenadorServicio
    {
        private readonly IUsuariosRepositorio _repository;

        public EntrenadorServicio(IUsuariosRepositorio repository)
        {
            _repository = repository;//inyeccion de dependencias
        }

    }
}