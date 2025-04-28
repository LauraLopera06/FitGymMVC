using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGymMVC.Models
{
    public class EjerciciosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Grupo Muscular es obligatorio")]
        public string GrupoMuscular { get; set; }
    }
}
