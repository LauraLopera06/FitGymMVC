namespace FitGymMVC.Models
{
    public class RutinaConEjerciciosModel
    {
        public int IdRutina { get; set; }
        public string NombreRutina { get; set; }
        public string Descripcion { get; set; }
        public string NivelDificultad { get; set; }
        public List<string> Ejercicios { get; set; } 
    }
}
