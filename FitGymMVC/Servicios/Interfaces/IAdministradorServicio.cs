using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IAdministradorServicio
    {
        bool CambiarRol(string cedula, string nuevoRol);
    }
}
