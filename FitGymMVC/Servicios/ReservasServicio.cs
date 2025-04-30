using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class ReservasServicio
    {
        private readonly IReservasRepositorio _repository;
        private readonly IUsuariosRepositorio _usuariosrepository;
        private readonly IClasesRepositorio _clasesrepository;

        public ReservasServicio(IReservasRepositorio repository, IUsuariosRepositorio usuariosrepository, IClasesRepositorio clasesrepository)
        {
            _repository = repository;
            _usuariosrepository = usuariosrepository;
            _clasesrepository = clasesrepository;
        }

        public List<ReservasModel> Listar()
        {
                return _repository.Listar();
            
        }
        public ReservasModel Buscar(int id)
        {

                return _repository.Buscar(id);
            
        }
        public (bool Exito, string Mensaje) Guardar(string cedulaUsuario, string nombreClaseSeleccionada)
        {
            // Validar campos vacíos
            if (string.IsNullOrEmpty(cedulaUsuario) || string.IsNullOrEmpty(nombreClaseSeleccionada))
            {
                return (false, "Debes llenar todos los campos.");
            }

            // Buscar usuario
            var usuario = _usuariosrepository.BuscarPorCedula(cedulaUsuario);
            if (usuario == null)
            {
                return (false, "Usuario no encontrado.");
            }

            // Buscar clase
            var clase = _clasesrepository.BuscarPorNombre(nombreClaseSeleccionada);
            if (clase == null)
            {
                return (false, "Clase no encontrada.");
            }

            // Validar si ya tiene reserva
            var reservasExistentes = _repository.Listar();
            bool yaReservado = reservasExistentes.Any(r => r.IdUsuario == usuario.Id && r.IdClase == clase.Id);

            if (yaReservado)
            {
                return (false, "Ya tienes una reserva para esta clase.");
            }

            // Crear modelo y guardar
            var reserva = new ReservasModel
            {
                IdUsuario = usuario.Id,
                IdClase = clase.Id
            };

            bool guardado = _repository.Guardar(reserva);
            return guardado
                ? (true, "Reserva creada exitosamente.")
                : (false, "Error al guardar la reserva.");
        }

    }
}