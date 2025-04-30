using FitGymMVC.Models;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class RutinaEjercicioController : Controller
    {
        private readonly IRutinaEjercicioServicio _servicio;

        public RutinaEjercicioController(IRutinaEjercicioServicio servicio)
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
        public IActionResult Guardar(RutinaEjercicioModel objRutinaEjercicio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = _servicio.Guardar(objRutinaEjercicio);

            if (respuesta)
            {
                return RedirectToAction("RutinaEjercicioCreado");
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public IActionResult RutinaEjercicioCreado()
        {
            return View();
        }
    }
}
