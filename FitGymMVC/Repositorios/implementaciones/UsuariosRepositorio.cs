using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones { 
public class UsuariosRepositorio : IUsuariosRepositorio
{
    
        private readonly string _cadenaSQL;

    public UsuariosRepositorio(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
    }

    public List<UsuariosModel> Listar()
    {

            try
            {
                var lista = new List<UsuariosModel>();

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarUsuarios", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new UsuariosModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"])
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

    public UsuariosModel Buscar(int id)
    {
            try
            {
                UsuariosModel usuario = null;

                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuario", conexion);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuariosModel()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"])
                            };
                        }
                    }
                }
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
    }

    public bool Guardar(UsuariosModel usuario)
    {

        using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("Cedula", usuario.Cedula);
                cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("FechaNacimiento", usuario.FechaNacimiento);

                cmd.ExecuteNonQuery();
                return true;
            }
    }
}
}
