using FitGymMVC.Servicios.Interfaces;
using System.Net;
using System.Net.Mail;

namespace FitGymMVC.Servicios
{
    public class EmailServicio : IEmailServicio
    {
        private readonly IConfiguration configuration;//se va a leer valores de appserttings.json

        public EmailServicio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> EnviarEmail(string emailReceptor, string tema, string cuerpo)
        {
            try
            {
                var emailEmisor = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
                var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:password");
                var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:host");
                var puerto = configuration.GetValue<int>("CONFIGURACIONES_EMAIL:puerto");

                var smtpCliente = new SmtpClient(host, puerto);
                smtpCliente.EnableSsl = true;
                smtpCliente.UseDefaultCredentials = false;

                smtpCliente.Credentials = new NetworkCredential(emailEmisor, password);
                var mensaje = new MailMessage();
                mensaje.From = new MailAddress(emailEmisor!, "FitGym");
                mensaje.To.Add(emailReceptor);
                mensaje.Subject = tema;
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = true;

                await smtpCliente.SendMailAsync(mensaje);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
