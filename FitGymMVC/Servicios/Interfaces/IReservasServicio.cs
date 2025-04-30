using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IReservasServicio
    {
        List<ReservasModel> Listar();
        ReservasModel Buscar(int id);
        (bool Exito, string Mensaje) Guardar(string cedulaUsuario, string nombreClaseSeleccionada);
    }
}
