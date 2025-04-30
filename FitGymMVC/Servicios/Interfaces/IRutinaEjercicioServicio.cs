using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IRutinaEjercicioServicio
    {
        List<RutinaEjercicioModel> Listar();
        RutinaEjercicioModel Buscar(int id);
        bool Guardar(RutinaEjercicioModel rutinaEjercicio);
    }
}
