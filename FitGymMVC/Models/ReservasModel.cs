using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGymMVC.Models
{
    public class ReservasModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public int? IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo Clase es obligatorio")]
        public int? IdClase { get; set; }

    }
}


