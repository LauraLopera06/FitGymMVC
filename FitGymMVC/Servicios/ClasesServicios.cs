﻿using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;

namespace FitGymMVC.Servicios
{
    public class ClasesServicio : IClasesServicio
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
        public ClasesModel BuscarPorNombre(string nombre)
        {
            return _repository.BuscarPorNombre(nombre);
        }

        public (bool Exito, string Mensaje) Guardar(ClasesModel Clase)
        {
            var clasesExistentes = _repository.Listar();

            //de la lista de clases revisa que la clase nueva no exista ya en la BD (que no haya un duplicado)
            bool Conflicto = clasesExistentes.Any(c =>
                 c.Fecha == Clase.Fecha &&
                ((Clase.HorarioInicio.Value < c.HorarioFin.Value && Clase.HorarioFin.Value > c.HorarioInicio.Value) ||
                (Clase.HorarioFin.Value > c.HorarioInicio.Value && Clase.HorarioInicio.Value < c.HorarioFin.Value)
                ));

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