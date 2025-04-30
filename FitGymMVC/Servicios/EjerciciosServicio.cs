using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class EjerciciosServicio : IEjerciciosServicio
    {
        private readonly IEjerciciosRepositorio _repository;

        public EjerciciosServicio(IEjerciciosRepositorio repository)
        {
            _repository = repository;
        }

        public List<EjerciciosModel> Listar()
        {
            return _repository.Listar();
        }

        public EjerciciosModel Buscar(int id)
        {
            return _repository.Buscar(id);
        }

        public bool Guardar(EjerciciosModel ejercicio)
        {
            return _repository.Guardar(ejercicio);
        }
    }
}
