using FitGymMVC.Models;

namespace FitGymMVC.Servicios.Interfaces
{
    public interface IUsuariosServicio
    {
        List<UsuariosModel> Listar();
        UsuariosModel Buscar(int id);
        UsuariosModel BuscarPorCedula(string cedula);
        bool Guardar(UsuariosModel usuario);
        Usuarioslogin Login(string correo, string contraseña);
    }
}
