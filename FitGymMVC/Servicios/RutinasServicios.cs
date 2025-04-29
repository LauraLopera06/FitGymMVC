using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class RutinasServicio
    {
        private readonly IRutinasRepositorio _repository;
        private readonly IRutinasConEjerciciosRepositorio _rutinasConEjerciciosRepo;
        public RutinasServicio(IRutinasRepositorio repository, IRutinasConEjerciciosRepositorio rutinasConEjerciciosRepo)
        {
            _repository = repository;
            _rutinasConEjerciciosRepo = rutinasConEjerciciosRepo;
        }

        public List<RutinasModel> Listar()
        {
            return _repository.Listar();
        }

        public RutinasModel Buscar(int id)
        {
            return _repository.Buscar(id);
        }
        public int Guardar(RutinasModel Rutina) 
        { 
            return _repository.Guardar(Rutina); 
        }

        public List<RutinaConEjerciciosModel> ListarConEjercicios()
        {
            return _rutinasConEjerciciosRepo.ListarConEjercicios();
        }

    }
}