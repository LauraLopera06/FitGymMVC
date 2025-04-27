using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class UsuariosServicio
    {
        private readonly IUsuariosRepositorio _repository;

        public UsuariosServicio(IUsuariosRepositorio repository)
        {
            _repository = repository;
        }

        public List<UsuariosModel> Listar()
        {
                return _repository.Listar();
            
        }
        public UsuariosModel Buscar(int id)
        {
                return _repository.Buscar(id);
            
        }
        public bool Guardar(UsuariosModel usuario) 
        {
                return _repository.Guardar(usuario);

        }
    }
}