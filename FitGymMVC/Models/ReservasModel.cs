using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGymMVC.Models
{
    public class ReservasModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Cupos Disponibles es obligatorio")]
        public int CuposDisponibles { get; set; }
        [Required(ErrorMessage = "El campo Disponibilidad es obligatorio")]
        public bool Disponibilidad { get; set; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo Clase es obligatorio")]
        public int IdClase { get; set; }
        [ForeignKey("IdUsuario")] public UsuariosModel? _IdUsuario { get; set; }
        [ForeignKey("IdClase")] public UsuariosModel? _IdClase { get; set; }

    }
}


