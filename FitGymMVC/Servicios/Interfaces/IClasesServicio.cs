using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IClasesServicio
    {
        List<ClasesModel> Listar();
        ClasesModel Buscar(int id);
        ClasesModel BuscarPorNombre(string nombre);
        (bool Exito, string Mensaje) Guardar(ClasesModel Clase);
    }
}
