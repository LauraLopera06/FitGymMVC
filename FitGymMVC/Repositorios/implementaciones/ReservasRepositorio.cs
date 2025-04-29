using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones { 
public class ReservasRepositorio : IReservasRepositorio
{
    
        private readonly string _cadenaSQL;

    public ReservasRepositorio(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
    }

    public List<ReservasModel> Listar()
    {

            try
            {
                var lista = new List<ReservasModel>();

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarReservas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReservasModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdClase = Convert.ToInt32(dr["IdClase"])
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

    public ReservasModel Buscar(int id)
    {
            try
            {
                ReservasModel reserva = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtenerreserva", conexion);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reserva = new ReservasModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdClase = Convert.ToInt32(dr["IdClase"])
                            };
                        }
                    }
                }
                return reserva;
            }
            catch (Exception)
            {
                return null;
            }
    }


    public bool Guardar(ReservasModel reserva)
    {

        using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarReserva", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", reserva.IdUsuario);
                cmd.Parameters.AddWithValue("IdClase", reserva.IdClase);

                cmd.ExecuteNonQuery();
                return true;
            }
    }
}
}
