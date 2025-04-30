namespace FitGymMVC.Models
{
    public class RutinaConEjerciciosModel //para la vista, que nos liste las rutinas con nombres y no con Id se llena con un procedure
    {
        public int IdRutina { get; set; }
        public string NombreRutina { get; set; }
        public string Descripcion { get; set; }
        public string NivelDificultad { get; set; }
        public List<string> Ejercicios { get; set; } 
    }
}
