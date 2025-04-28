using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IRutinaEjercicioRepositorio
    {
        List<RutinaEjercicioModel> Listar();
        RutinaEjercicioModel Buscar(int id);
        bool Guardar(RutinaEjercicioModel RutinaEjercicio);
    }
}
