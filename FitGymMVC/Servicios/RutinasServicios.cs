using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class RutinasServicio
    {
        private readonly IRutinasRepositorio _repository;

        public RutinasServicio(IRutinasRepositorio repository)
        {
            _repository = repository;
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
    }
}