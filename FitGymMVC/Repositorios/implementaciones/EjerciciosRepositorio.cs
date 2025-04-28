using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones
{
    public class EjerciciosRepositorio : IEjerciciosRepositorio
    {
        private readonly string _cadenaSQL;

        public EjerciciosRepositorio(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public List<EjerciciosModel> Listar()
        {
            var lista = new List<EjerciciosModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarEjercicios", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EjerciciosModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                GrupoMuscular = dr["GrupoMuscular"].ToString()
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

        public bool Guardar(EjerciciosModel ejercicio)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarEjercicio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", ejercicio.Nombre);
                    cmd.Parameters.AddWithValue("GrupoMuscular", ejercicio.GrupoMuscular);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public EjerciciosModel Buscar(int id)
        {
            try
            {
                EjerciciosModel ejercicio = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerEjercicio", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ejercicio = new EjerciciosModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                GrupoMuscular = dr["GrupoMuscular"].ToString()
                            };
                        }
                    }
                }
                return ejercicio;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
