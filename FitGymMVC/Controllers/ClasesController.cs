using FitGymMVC.Models;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class ClasesController : Controller
    {
        private readonly IClasesServicio _servicio;
        private readonly IEntrenadorServicio _servicioEntrenador;
        private readonly IUsuariosServicio _servicioUsuario;
        private readonly IEmailServicio _emailServicio;

        public ClasesController(IClasesServicio service, IEntrenadorServicio servicioEntrenador, IUsuariosServicio servicioUsuario, IEmailServicio emailServicio) 
        {
            _servicio = service; //inyeccion de dependencias.
            _servicioEntrenador = servicioEntrenador;
            _servicioUsuario = servicioUsuario;
            _emailServicio = emailServicio;
        }
        public IActionResult Listar() //es una vista.
        {
            try
            {
                var objLista = _servicio.Listar();
                _servicio.ActualizarEstadosAutomaticamente();

                foreach (var clase in objLista)
                {
                    var entrenador = _servicioUsuario.BuscarPorCedula(clase.CedulaEntrenador);
                    clase.CedulaEntrenador = entrenador?.Nombre ?? "Entrenador no encontrado"; // reutilizamos el campo para mostrar el nombre
                }

                return View(objLista);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public IActionResult Guardar() //mostrar formulario para crear usuario.
        {
            var entrenadores = _servicioEntrenador.Listar(); 
            ViewBag.ListaEntrenadores = entrenadores;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(ClasesModel objClase)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaEntrenadores = _servicioEntrenador.Listar();
                return View(objClase);
            }

            var respuesta = _servicio.Guardar(objClase);

            if (respuesta.Exito)
            {
                var entrenador = _servicioUsuario.BuscarPorCedula(objClase.CedulaEntrenador);

                var correoEnviado = await _emailServicio.EnviarEmail(
                emailReceptor: entrenador.Correo,
                tema: "Nueva clase asignada - FitGym",
                cuerpo: $"<h3>¡Hola {entrenador.Nombre}!</h3><p>Se te ha asignado la clase <strong>{objClase.Nombre}</strong> para el día <strong>{objClase.Fecha}</strong> de <strong>{objClase.HorarioInicio}</strong> a <strong>{objClase.HorarioFin}</strong>.</p>"
                );

                if (!correoEnviado)
                {
                    ViewBag.ListaEntrenadores = _servicioEntrenador.Listar();
                    ModelState.AddModelError(string.Empty, "La clase fue creada, pero no se pudo enviar el correo al entrenador.");
                    return View(objClase);
                }

                return RedirectToAction("ClaseCreada");
            }
            else
            {
                ViewBag.ListaEntrenadores = _servicioEntrenador.Listar();
                ModelState.AddModelError(string.Empty, respuesta.Mensaje); // mostramos el mensaje que venga
                return View(objClase); // volvemos a mostrar el formulario
            }
        }

        public IActionResult ClaseCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

        public IActionResult Cancelar(int id)
        {
            _servicio.CambiarEstado(id, "Cancelada");
            return RedirectToAction("Listar");
        }

        public IActionResult Programar(int id)
        {
            _servicio.CambiarEstado(id, "Programada");
            return RedirectToAction("Listar");
        }


    }

}
