using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones
{
    public class RutinaEjercicioRepositorio : IRutinaEjercicioRepositorio
    {
        private readonly string _cadenaSQL;

        public RutinaEjercicioRepositorio(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public List<RutinaEjercicioModel> Listar()
        {
            var lista = new List<RutinaEjercicioModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarRutinaEjercicios", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RutinaEjercicioModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdRutina = Convert.ToInt32(dr["IdRutina"]),
                                IdEjercicio = Convert.ToInt32(dr["IdEjercicio"])
                            });
                        }
                    }
                    return lista;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Guardar(RutinaEjercicioModel rutinaEjercicio)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarRutinaEjercicio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdRutina", rutinaEjercicio.IdRutina);
                    cmd.Parameters.AddWithValue("IdEjercicio", rutinaEjercicio.IdEjercicio);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public RutinaEjercicioModel Buscar(int id)
        {
            try
            {
                RutinaEjercicioModel rutinaEjercicio = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerRutinaEjercicio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rutinaEjercicio = new RutinaEjercicioModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdRutina = Convert.ToInt32(dr["IdRutina"]),
                                IdEjercicio = Convert.ToInt32(dr["IdEjercicio"])
                            };
                        }
                    }
                }
                return rutinaEjercicio;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
