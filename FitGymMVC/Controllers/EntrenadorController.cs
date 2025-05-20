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
    public class EntrenadorController : Controller
    {
        private readonly IUsuariosServicio _servicio;

        public EntrenadorController(IUsuariosServicio service)
        {
            _servicio = service; //ineyccion de dependencias
        }
        [Authorize(Roles = "Entrenador")]
        public IActionResult Listar() //debe haber una vista Listar en /views/Entrenador así con el resto
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

        [Authorize(Roles = "Entrenador")]
        public IActionResult InicioEntrenador()
        {
            return View();
        }
        
    }

}
