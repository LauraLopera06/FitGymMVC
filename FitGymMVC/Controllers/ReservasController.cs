using FitGymMVC.Models;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class ReservasController : Controller
    {
        private readonly IReservasServicio _servicio;
        private readonly IClasesServicio _clasesServicio;
        private readonly IUsuariosServicio _usuariosServicio;


        public ReservasController(IReservasServicio service, IClasesServicio clasesServicio, IUsuariosServicio usuariosServicio)
        {
            _servicio = service;
            _clasesServicio = clasesServicio;
            _usuariosServicio = usuariosServicio;
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
            var clases = _clasesServicio.Listar(); // servicio de clases
            ViewBag.ListaClases = clases;           // Mandamos las clases
            return View(new ReservasModel());
        }

        [HttpPost]
        public IActionResult Guardar(string CedulaUsuario, string NombreClaseSeleccionada)
        {
            var resultado = _servicio.Guardar(CedulaUsuario, NombreClaseSeleccionada);

            if (resultado.Exito)
            {
                return RedirectToAction("ReservaCreada");
            }

            var clases = _clasesServicio.Listar();
            ViewBag.ListaClases = clases;
            ModelState.AddModelError(string.Empty, resultado.Mensaje);
            return View();
        }

        public IActionResult ReservaCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }
    }

}
