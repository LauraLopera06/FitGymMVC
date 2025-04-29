using FitGymMVC.Models;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class EjerciciosController : Controller
    {
        private readonly EjerciciosServicio _servicio;

        public EjerciciosController(EjerciciosServicio servicio)
        {
            _servicio = servicio;
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

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(EjerciciosModel objEjercicio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = _servicio.Guardar(objEjercicio);

            if (respuesta)
            {
                return RedirectToAction("Guardar","Rutinas");
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public IActionResult EjercicioCreado()
        {
            return View();
        }
    }
}
