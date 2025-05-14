using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        List<UsuariosModel> Listar();
        UsuariosModel Buscar(int id);
        bool Guardar(UsuariosModel usuario);

        Usuarioslogin ValidarUsuario(string correo, string contraseña);
        UsuariosModel BuscarPorCedula(string cedula);
    }
}