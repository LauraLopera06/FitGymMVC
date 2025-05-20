using Microsoft.Data.SqlClient;
using System.Data;
using FitGymMVC.Repositorios.Interfaces;
using FitGymMVC.Models;

namespace FitGymMVC.Repositorios.Implementaciones { 
public class UsuariosRepositorio : IUsuariosRepositorio
    {
    
        private readonly string _cadenaSQL;

    //inyeccion de dependencia
    public UsuariosRepositorio(IConfiguration configuration)
    {
        _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
    }

    public List<UsuariosModel> Listar()
    {

            try
            {
                var lista = new List<UsuariosModel>();

                using (var conexion = new SqlConnection(_cadenaSQL))//se crea la conexion
                {
                    conexion.Open();//se abre
                    SqlCommand cmd = new SqlCommand("sp_ListarUsuarios", conexion);//ejecucion de consulta o procedimiento
                    cmd.CommandType = CommandType.StoredProcedure; //indica que se ejecuta un procedimiento almacenado

                    using (var dr = cmd.ExecuteReader())//ejecuta la consulta
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
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                TipoUsuario = dr["TipoUsuario"].ToString()
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
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                TipoUsuario = dr["TipoUsuario"].ToString()
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

    public UsuariosModel BuscarPorCedula(string cedula)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarUsuarioPorCedula", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Cedula", cedula);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new UsuariosModel
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Cedula = dr["Cedula"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            TipoUsuario = dr["TipoUsuario"].ToString()
                        };
                    }
                }
            }
            return null;
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
                cmd.Parameters.AddWithValue("Contraseña", usuario.Contraseña);

                cmd.ExecuteNonQuery();
                return true;
            }
    }

        public Usuarioslogin ValidarUsuario(string correo, string contraseña)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                conexion.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuarioslogin
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Correo = reader["Correo"].ToString(),
                            TipoUsuario = reader["TipoUsuario"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        public bool EditarUsuario(UsuariosModel usuario)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EditarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Cedula", usuario.Cedula);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0; // true si se actualizó correctamente
            }
        }
        public bool EliminarCliente(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
        public bool AgregarCliente(int idUsuario)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_AgregarCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", idUsuario);

                int filasAfectadas = cmd.ExecuteNonQuery();

                return filasAfectadas > 0; // true si se insertó correctamente
            }
        }


    }
}
