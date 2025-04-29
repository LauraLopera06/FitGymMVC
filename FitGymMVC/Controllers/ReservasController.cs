using FitGymMVC.Models;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservasServicio _servicio;
        private readonly ClasesServicio _clasesServicio;
        private readonly UsuariosServicio _usuariosServicio;


        public ReservasController(ReservasServicio service, ClasesServicio clasesServicio, UsuariosServicio usuariosServicio)
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
            if (string.IsNullOrEmpty(CedulaUsuario) || string.IsNullOrEmpty(NombreClaseSeleccionada))
            {
                var clases = _clasesServicio.Listar();
                ViewBag.ListaClases = clases;
                ViewBag.MensajeError = "Debes llenar todos los campos.";
                return View();
            }

            // 1. Buscar el ID del usuario
            var usuario = _usuariosServicio.BuscarPorCedula(CedulaUsuario); 
            if (usuario == null)
            {
                var clases = _clasesServicio.Listar();
                ViewBag.ListaClases = clases;
                ViewBag.MensajeError = "Usuario no encontrado.";
                return View();
            }

            // 2. Buscar el ID de la clase
            var clase = _clasesServicio.BuscarPorNombre(NombreClaseSeleccionada); // 👈 Este método también lo agregaremos
            if (clase == null)
            {
                var clases = _clasesServicio.Listar();
                ViewBag.ListaClases = clases;
                ViewBag.MensajeError = "Clase no encontrada.";
                return View();
            }
            //verificar si no está ya en la clase
            var reservasExistentes = _servicio.Listar();
            bool yaReservado = reservasExistentes.Any(r => r.IdUsuario == usuario.Id && r.IdClase == clase.Id);

            if (yaReservado)
            {
                var clases = _clasesServicio.Listar();
                ViewBag.ListaClases = clases;
                ViewBag.MensajeError = "Ya tienes una reserva para esta clase.";
                return View();
            }
            // 3. Crear el objeto ReservasModel
            var reserva = new ReservasModel
            {
                IdUsuario = usuario.Id,
                IdClase = clase.Id,
            };

            // 4. Guardar la reserva
            var respuesta = _servicio.Guardar(reserva);

            if (respuesta)
            {
                return RedirectToAction("CuentaCreada");
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }


    }

}
