using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IRutinasRepositorio
    {
        List<RutinasModel> Listar();
        RutinasModel Buscar(int id);
        bool Guardar(RutinasModel Rutina);
    }
}