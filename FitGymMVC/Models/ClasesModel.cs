using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGymMVC.Models
{
    public class ClasesModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Horario de inicio es obligatorio")]
        public TimeSpan? HorarioInicio { get; set; }

        [Required(ErrorMessage = "El campo Horario de finalización es obligatorio")]
        public TimeSpan? HorarioFin { get; set; }

        [Required(ErrorMessage = "El campo Cupos Limites es obligatorio")]
        public int? CuposLimites { get; set; }
        public string CedulaEntrenador { get; set; }

        public string Descripcion { get; set; }
        public string? Estado { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public string? Fecha { get; set; }

    }
}
