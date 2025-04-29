using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IClasesRepositorio
    {
        List<ClasesModel> Listar();
        ClasesModel Buscar(int id);
        bool Guardar(ClasesModel Clase);

        ClasesModel BuscarPorNombre(string nombre);
    }
}