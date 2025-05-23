using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IUsuariosRepositorio _repository;

        public ClienteServicio(IUsuariosRepositorio repository)
        {
            _repository = repository;//inyeccion de dependencias
        }



    }
}