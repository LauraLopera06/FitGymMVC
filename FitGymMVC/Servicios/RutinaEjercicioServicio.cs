using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class RutinaEjercicioServicio
    {
        private readonly IRutinaEjercicioRepositorio _repository;

        public RutinaEjercicioServicio(IRutinaEjercicioRepositorio repository)
        {
            _repository = repository;
        }

        public List<RutinaEjercicioModel> Listar()
        {
            return _repository.Listar();
        }

        public RutinaEjercicioModel Buscar(int id)
        {
            return _repository.Buscar(id);
        }

        public bool Guardar(RutinaEjercicioModel rutinaEjercicio)
        {
            return _repository.Guardar(rutinaEjercicio);
        }
    }
}
