using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IRutinasServicio
    {
        List<RutinasModel> Listar();
        RutinasModel Buscar(int id);
        int Guardar(RutinasModel rutina);
        List<RutinaConEjerciciosModel> ListarConEjercicios();
    }
}
