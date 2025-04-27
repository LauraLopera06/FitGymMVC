using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Servicios
{
    public class ClasesServicio
    {
        private readonly IClasesRepositorio _repository;

        public ClasesServicio(IClasesRepositorio repository)
        {
            _repository = repository;
        }

        public List<ClasesModel> Listar()
        {
            return _repository.Listar();
        }

        public ClasesModel Buscar(int id)
        {
            return _repository.Buscar(id);
        }
        public (bool Exito, string Mensaje) Guardar(ClasesModel Clase)
        {
            var clasesExistentes = _repository.Listar();

            bool Conflicto = clasesExistentes.Any(c =>
                c.Fecha == Clase.Fecha &&
                c.Horario.Value == Clase.Horario.Value);

            if (Conflicto)
            {
                return (false, "Error: Ya existe una clase registrada en esta fecha y horario.");
            }

            bool guardado = _repository.Guardar(Clase);

            if (guardado)
                return (true, "Clase guardada exitosamente.");
            else
                return (false, "Error al guardar la clase.");
        }
    }
}