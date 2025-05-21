namespace FitGymMVC.Servicios.Interfaces
{
    public interface IEmailServicio
    {
        Task<bool> EnviarEmail(string emailReceptor, string tema, string cuerpo);
    }
}