using System.ComponentModel.DataAnnotations;
namespace FitGymMVC.Models
{
    public class Usuarioslogin
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string TipoUsuario { get; set; } // Esto funciona como "rol"
    }
}
