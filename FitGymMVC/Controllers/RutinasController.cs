using FitGymMVC.Models;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class RutinasController : Controller
    {
        private readonly RutinasServicio _servicio;
        private readonly EjerciciosServicio _ejerciciosServicio;
        private readonly RutinaEjercicioServicio _ejerciciosRutinaServicio;

        public RutinasController(RutinasServicio servicio, EjerciciosServicio ejerciciosServicio, RutinaEjercicioServicio rutinaEjercicioRepositorio)
        {
            _servicio = servicio;
            _ejerciciosServicio = ejerciciosServicio;
            _ejerciciosRutinaServicio = rutinaEjercicioRepositorio;
        }


        public IActionResult Listar()
        {
            try 
            {
                var objLista = _servicio.Listar();
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
                var ejercicios = _ejerciciosServicio.Listar(); // 🔥 Recargar ejercicios
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

                        _ejerciciosRutinaServicio.Guardar(nuevaRelacion); // 🔥 Así usas tu método existente
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
    }

}
