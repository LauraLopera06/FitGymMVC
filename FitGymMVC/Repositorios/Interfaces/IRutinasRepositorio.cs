using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IRutinasRepositorio
    {
        List<RutinasModel> Listar();
        RutinasModel Buscar(int id);
        int Guardar(RutinasModel Rutina);//cambio hecho para que devuelva ID de la rutina (para los ejercicios)

    }
}