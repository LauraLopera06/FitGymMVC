using System.ComponentModel.DataAnnotations;
namespace FitGymMVC.Models
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Nombre es obligatorio")] // Validación obligatoria en formularios
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Cedula es obligatorio")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio")]
        public DateTime? FechaNacimiento { get; set; }
        public string? TipoUsuario { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Contraseña { get; set; }

        public int Edad // Cálculo automático de la edad basado en la fecha de nacimiento
        {
            get
            {
                if (!FechaNacimiento.HasValue)
                    return 0; 

                var hoy = DateTime.Today;
                var nacimiento = FechaNacimiento.Value;
                var edad = hoy.Year - nacimiento.Year;
                if (nacimiento.Date > hoy.AddYears(-edad)) edad--;
                return edad;
            }
        }
    }
}
