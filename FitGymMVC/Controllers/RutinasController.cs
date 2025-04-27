using FitGymMVC.Models;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class RutinasController : Controller
    {
        private readonly RutinasServicio _servicio;

        public RutinasController(RutinasServicio service)
        {
            _servicio = service;
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
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(RutinasModel objRutina) //sacar datos del formulario para mandar a la BD
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var respuesta = _servicio.Guardar(objRutina);

            if (respuesta)
            {
                return RedirectToAction("RutinaCreada");
            }
            else {
                return View("~/Views/Shared/Error.cshtml");
            }

        }

        public IActionResult RutinaCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

    }

}
