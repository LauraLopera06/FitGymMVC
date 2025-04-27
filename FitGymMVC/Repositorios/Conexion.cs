using System.Data.SqlClient;

namespace FitGymMVC.Repositorios
{
    public class Conexion //usaremos ADO.net
    {
        private string cadenaSQL = String.Empty;
        public Conexion() {
            //se obtiene el String Conection que está en appsettings.json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }
        
        public string getCadenaSQL() { 
            return cadenaSQL;
        }
    }
}
