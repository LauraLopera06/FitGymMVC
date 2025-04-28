using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IEjerciciosRepositorio
    {
        List<EjerciciosModel> Listar();
        EjerciciosModel Buscar(int id);
        bool Guardar(EjerciciosModel Ejercicio);
    }
}
