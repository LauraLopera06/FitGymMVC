using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class AdministradorServicio : IAdministradorServicio
    {
        private readonly IUsuariosRepositorio _repository;
        private readonly IUsuariosServicio Uservicio;

        public AdministradorServicio(IUsuariosRepositorio repository, IUsuariosServicio uservicio)
        {
            _repository = repository;//inyeccion de dependencias
            Uservicio = uservicio;
        }

        public bool CambiarRol(string cedula, string nuevoRol) 
        {

            var usuarioEncontrado = Uservicio.BuscarPorCedula(cedula); 

            if (usuarioEncontrado != null)
            {
                var rolAnterior = usuarioEncontrado.TipoUsuario;
                if (rolAnterior == nuevoRol)
                {                  
                    return false; // No hubo cambio de rol
                }
                usuarioEncontrado.TipoUsuario = nuevoRol;
                _repository.EditarUsuario(usuarioEncontrado);

                if (rolAnterior == "Cliente" )
                {                                                                  
                    _repository.EliminarCliente(usuarioEncontrado.Id);
                }
                if (nuevoRol == "Cliente") 
                {
                    _repository.AgregarCliente(usuarioEncontrado.Id);
                }
                return true;
            }
            return false;
        }
    }
}