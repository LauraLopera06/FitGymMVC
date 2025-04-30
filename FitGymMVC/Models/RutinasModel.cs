using FitGymMVC.Models.InterfacePrototype;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitGymMVC.Models.InterfacePrototype;
namespace FitGymMVC.Models
{
    public class RutinasModel : IPrototype<RutinasModel> //para el patron de diseño
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Nivel de dificultad es obligatorio")]
        public string NivelDificultad { get; set; }
        [NotMapped]
        public List<int> IdsEjerciciosSeleccionados { get; set; }

        public RutinasModel Clonar()//Implementacion para el patron de diseño
        {
            return new RutinasModel
            {
                Nombre = this.Nombre + " (Copia)",
                Descripcion = this.Descripcion,
                NivelDificultad = this.NivelDificultad,
                IdsEjerciciosSeleccionados = new List<int>(this.IdsEjerciciosSeleccionados ?? new List<int>())
            };
        }

    }
}



