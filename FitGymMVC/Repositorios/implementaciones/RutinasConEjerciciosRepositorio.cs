using FitGymMVC.Models;
using FitGymMVC.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FitGymMVC.Repositorios.Implementaciones
{
    public class RutinasConEjerciciosRepositorio : IRutinasConEjerciciosRepositorio
    {
        private readonly string _cadenaSQL;

        public RutinasConEjerciciosRepositorio(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public List<RutinaConEjerciciosModel> ListarConEjercicios()
        {
            var resultado = new Dictionary<int, RutinaConEjerciciosModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarRutinasConEjercicios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int idRutina = Convert.ToInt32(dr["IdRutina"]);
                        string nombreRutina = dr["NombreRutina"].ToString();
                        string descripcion = dr["Descripcion"].ToString();
                        string nivel = dr["NivelDificultad"].ToString();
                        string ejercicio = dr["NombreEjercicio"].ToString();

                        if (!resultado.ContainsKey(idRutina))
                        {
                            resultado[idRutina] = new RutinaConEjerciciosModel
                            {
                                IdRutina = idRutina,
                                NombreRutina = nombreRutina,
                                Descripcion = descripcion,
                                NivelDificultad = nivel,
                                Ejercicios = new List<string>()
                            };
                        }

                        resultado[idRutina].Ejercicios.Add(ejercicio);
                    }
                }
            }

            return resultado.Values.ToList();
        }
    }
}
