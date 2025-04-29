var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Registrar controladores
builder.Services.AddControllersWithViews();

// 2️⃣ Registrar servicios y repositorios aquí — ANTES de builder.Build()
builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IUsuariosRepositorio, FitGymMVC.Repositorios.Implementaciones.UsuariosRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.UsuariosServicio>();

builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IRutinasRepositorio, FitGymMVC.Repositorios.Implementaciones.RutinasRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.RutinasServicio>();

builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IEjerciciosRepositorio, FitGymMVC.Repositorios.Implementaciones.EjerciciosRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.EjerciciosServicio>();

builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IRutinaEjercicioRepositorio, FitGymMVC.Repositorios.Implementaciones.RutinaEjercicioRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.RutinaEjercicioServicio>();

builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IClasesRepositorio, FitGymMVC.Repositorios.Implementaciones.ClasesRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.ClasesServicio>();

builder.Services.AddScoped<FitGymMVC.Repositorios.Interfaces.IReservasRepositorio, FitGymMVC.Repositorios.Implementaciones.ReservasRepositorio>();
builder.Services.AddScoped<FitGymMVC.Servicios.ReservasServicio>();

// 3️⃣ Construir la app después de registrar servicios
var app = builder.Build();


// 4️⃣ Configurar el pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error"); // Redirige errores a una vista
    app.UseHsts();
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Bienvenida}/{id?}");
app.Run();
