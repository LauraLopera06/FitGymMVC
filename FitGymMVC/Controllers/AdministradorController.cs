using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FitGymMVC.Servicios;

namespace FitGymMVC.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly IUsuariosServicio _servicio;
        private readonly IAdministradorServicio _Adminservicio;

        public AdministradorController(IUsuariosServicio service, IAdministradorServicio adminservicio)
        {
            _servicio = service; //ineyccion de dependencias
            _Adminservicio = adminservicio;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Listar() //debe haber una vista Listar en /views/Administrador así con el resto
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

        [Authorize(Roles = "Administrador")]
        public IActionResult InicioAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarRol(string Cedula, string NuevoRol)
        {
            var resultado = _Adminservicio.CambiarRol(Cedula, NuevoRol);
            if (resultado)
            {
                TempData["Mensaje"] = "Rol actualizado correctamente.";
            }
            else
            {
                TempData["MensajeError"] = "No se pudo actualizar el rol.";
            }

            return RedirectToAction("Listar"); 
        }

    }

}
