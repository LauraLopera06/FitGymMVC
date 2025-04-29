using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class ReservasServicio
    {
        private readonly IReservasRepositorio _repository;

        public ReservasServicio(IReservasRepositorio repository)
        {
            _repository = repository;
        }

        public List<ReservasModel> Listar()
        {
                return _repository.Listar();
            
        }
        public ReservasModel Buscar(int id)
        {
                return _repository.Buscar(id);
            
        }
        public bool Guardar(ReservasModel usuario) 
        {
                return _repository.Guardar(usuario);

        }
    }
}