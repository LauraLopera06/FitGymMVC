using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly IUsuariosRepositorio _repository;

        public UsuariosServicio(IUsuariosRepositorio repository)
        {
            _repository = repository;//inyeccion de dependencias
        }

        public List<UsuariosModel> Listar()
        {

                return _repository.Listar();
            
        }
        public UsuariosModel Buscar(int id)
        {
                return _repository.Buscar(id);
            
        }
        public UsuariosModel BuscarPorCedula(string cedula)
        {
            return _repository.BuscarPorCedula(cedula);
        }

        public bool Guardar(UsuariosModel usuario) 
        {
            var yaExiste = BuscarPorCedula(usuario.Cedula);
            if (yaExiste == null)
            {
                return _repository.Guardar(usuario);
            }
            return false;

        }

        public Usuarioslogin Login(string correo, string contraseña)
        {
            return _repository.ValidarUsuario(correo, contraseña);
        }

    }
}