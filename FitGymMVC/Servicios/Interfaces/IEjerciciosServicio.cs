using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IEjerciciosServicio
    {
        List<EjerciciosModel> Listar();
        EjerciciosModel Buscar(int id);
        bool Guardar(EjerciciosModel ejercicio);
    }
}
