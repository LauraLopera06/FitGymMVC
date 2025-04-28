using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGymMVC.Models
{
    public class RutinaEjercicioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Id de Rutina es obligatorio")]
        public int IdRutina { get; set; }

        [Required(ErrorMessage = "El campo Id de Ejercicio es obligatorio")]
        public int IdEjercicio { get; set; }
    }
}
