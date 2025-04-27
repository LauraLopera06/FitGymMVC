using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones { 
public class ClasesRepositorio : IClasesRepositorio
{
    
        private readonly string _cadenaSQL;

    public ClasesRepositorio(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
    }

    public List<ClasesModel> Listar()
    {
        var lista = new List<ClasesModel>();

        using (var conexion = new SqlConnection(_cadenaSQL))
        {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarClases", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ClasesModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                CuposLimites = Convert.ToInt32(dr["CuposLimites"]),
                                Fecha = dr["Fecha"].ToString(),
                                Horario = (TimeSpan)dr["Horario"],
                                Descripcion = dr["Descripcion"].ToString()
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

    public ClasesModel Buscar(int id)
    {
            try
            {
                ClasesModel clase = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerClase", conexion);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            clase = new ClasesModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                CuposLimites = Convert.ToInt32(dr["CuposLimites"]),
                                Fecha = dr["Fecha"].ToString(),
                                Horario = (TimeSpan)dr["Horario"],
                                Descripcion = dr["Descripcion"].ToString()
                            };
                        }
                    }
                }
                return clase;
            }
            catch (Exception)
            {
                return null;
            }
        
    }

    public bool Guardar(ClasesModel Clase)
    {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarClase", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", Clase.Nombre);
                    cmd.Parameters.AddWithValue("Fecha", Clase.Fecha);
                    cmd.Parameters.AddWithValue("Horario", Clase.Horario);
                    cmd.Parameters.AddWithValue("CuposLimites", Clase.CuposLimites);
                    cmd.Parameters.AddWithValue("Descripcion", Clase.Descripcion);
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
