using FitGymMVC.Models;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class RutinasController : Controller
    {
        private readonly IRutinasServicio _servicio;
        private readonly IEjerciciosServicio _ejerciciosServicio;
        private readonly IRutinaEjercicioServicio _ejerciciosRutinaServicio;

        public RutinasController(IRutinasServicio servicio, IEjerciciosServicio ejerciciosServicio, IRutinaEjercicioServicio rutinaEjercicioRepositorio)
        {
            _servicio = servicio;
            _ejerciciosServicio = ejerciciosServicio;
            _ejerciciosRutinaServicio = rutinaEjercicioRepositorio;
        }


        public IActionResult Listar()
        {
            try 
            {
                var objLista = _servicio.ListarConEjercicios();
                return View(objLista);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public IActionResult Guardar() //mostrar formulario solo devuelve la vista
        {
            try
            {
                var ejercicios = _ejerciciosServicio.Listar();
                ViewBag.ListaEjercicios = ejercicios; //  Mandamos la lista usando ViewBag
                return View(new RutinasModel()); //  Mandamos el modelo de Rutina
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Guardar(RutinasModel objRutina)
        {
            if (!ModelState.IsValid)
            {
                var ejercicios = _ejerciciosServicio.Listar(); // Recargar ejercicios
                ViewBag.ListaEjercicios = ejercicios;
                return View(objRutina);
            }

            var idRutinaNueva = _servicio.Guardar(objRutina);//se obtiene el Id de la rutina que guardamos

            if (idRutinaNueva > 0)
            {
                if (objRutina.IdsEjerciciosSeleccionados != null)
                {
                    foreach (var idEjercicio in objRutina.IdsEjerciciosSeleccionados)
                    {
                        var nuevaRelacion = new RutinaEjercicioModel
                        {
                            IdRutina = idRutinaNueva,
                            IdEjercicio = idEjercicio
                        };

                        _ejerciciosRutinaServicio.Guardar(nuevaRelacion); // Así usas tu método existente
                    }
                }


                return RedirectToAction("RutinaCreada");
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }


        public IActionResult RutinaCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }
        public IActionResult Clonar(int id)//clonear par el patron de diseño
        {
            var original = _servicio.Buscar(id);

            if (original == null)
                return View("~/Views/Shared/Error.cshtml");

            var clon = original.Clonar();

            // Obtener los ejercicios asociados a la rutina original
            var relaciones = _ejerciciosRutinaServicio.Listar()
                .Where(e => e.IdRutina == id)
                .Select(e => e.IdEjercicio)
                .ToList();

            // Asignarlos al clon
            clon.IdsEjerciciosSeleccionados = relaciones;

            // Cargar ejercicios disponibles
            var ejercicios = _ejerciciosServicio.Listar();
            ViewBag.ListaEjercicios = ejercicios;

            return View("Guardar", clon);
        }


    }

}
