using FitGymMVC.Servicios.Interfaces;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Registrar servicios y repositorios 
// Repositorios
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IUsuariosRepositorio, FitGymMVC.Repositorios.Implementaciones.UsuariosRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IRutinasRepositorio, FitGymMVC.Repositorios.Implementaciones.RutinasRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IEjerciciosRepositorio, FitGymMVC.Repositorios.Implementaciones.EjerciciosRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IRutinaEjercicioRepositorio, FitGymMVC.Repositorios.Implementaciones.RutinaEjercicioRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IClasesRepositorio, FitGymMVC.Repositorios.Implementaciones.ClasesRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IReservasRepositorio, FitGymMVC.Repositorios.Implementaciones.ReservasRepositorio>();
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IRutinasConEjerciciosRepositorio, FitGymMVC.Repositorios.Implementaciones.RutinasConEjerciciosRepositorio>();

// Servicios
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IUsuariosServicio, FitGymMVC.Servicios.UsuariosServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IAdministradorServicio, FitGymMVC.Servicios.AdministradorServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IEntrenadorServicio, FitGymMVC.Servicios.EntrenadorServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IClienteServicio, FitGymMVC.Servicios.ClienteServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IRutinasServicio, FitGymMVC.Servicios.RutinasServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IEjerciciosServicio, FitGymMVC.Servicios.EjerciciosServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IRutinaEjercicioServicio, FitGymMVC.Servicios.RutinaEjercicioServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IClasesServicio, FitGymMVC.Servicios.ClasesServicio>();
builder.Services.AddScoped<FitGymMVC.Servicios.Interfaces.IReservasServicio, FitGymMVC.Servicios.ReservasServicio>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuarios/Login";             // Ruta donde se redirige si no está autenticado 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la cookie
    });

builder.Services.AddAuthorization();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error"); // Redirige errores a una vista
    app.UseHsts(); // Fuerza el uso de HTTPS en navegadores
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Importante
app.UseAuthorization(); //basada en el rol


// Define la ruta por defecto del proyecto: HomeController -> Bienvenida.cshtml
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Bienvenida}/{id?}");

app.Run();
