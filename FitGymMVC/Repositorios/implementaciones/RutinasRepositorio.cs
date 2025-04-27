using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones { 
public class RutinasRepositorio : IRutinasRepositorio
{
    
        private readonly string _cadenaSQL;

    public RutinasRepositorio(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
    }

    public List<RutinasModel> Listar()
    {
            try
            {
                var lista = new List<RutinasModel>();

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarRutinas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RutinasModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                NivelDificultad = dr["NivelDificultad"].ToString(),

                            });
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
    }

    public RutinasModel Buscar(int id)
    {
            try
            {
                RutinasModel rutina = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerRutina", conexion);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rutina = new RutinasModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                NivelDificultad = dr["NivelDificultad"].ToString(),
                            };
                        }
                    }
                }
                return rutina;
            }
            catch (Exception)
            {
                return null;
            }
    }

    public bool Guardar(RutinasModel Rutina)
    {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarRutina", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", Rutina.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", Rutina.Descripcion);
                    cmd.Parameters.AddWithValue("NivelDificultad", Rutina.NivelDificultad);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
    }
}
}
