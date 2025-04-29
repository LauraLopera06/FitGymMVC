using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IReservasRepositorio
    {
        List<ReservasModel> Listar();
        ReservasModel Buscar(int id);
        bool Guardar(ReservasModel usuario);
    }
}