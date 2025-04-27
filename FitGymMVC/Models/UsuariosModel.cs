using System.ComponentModel.DataAnnotations;
namespace FitGymMVC.Models
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Nombre es obligatorio")] 
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Cedula es obligatorio")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio")]
        public DateTime? FechaNacimiento { get; set; }


    }
}
