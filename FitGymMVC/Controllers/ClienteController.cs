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
    public class ClienteController : Controller
    {
        private readonly IUsuariosServicio _servicio;

        public ClienteController(IUsuariosServicio service)
        {
            _servicio = service; //ineyccion de dependencias
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult InicioCliente()
        {
            return View();
        }
        
    }

}
