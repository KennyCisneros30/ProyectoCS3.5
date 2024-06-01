using ProyectoCS.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos.Interface
{
    public class Propietario
    {
        private readonly ConeccionSQL _conexionSQL;// Objeto de conexión SQL

        // Constructor que recibe la conexión SQL
        public Propietario(ConeccionSQL conexionSQL)
        {
            _conexionSQL = conexionSQL;
        }

        // Inserta un nuevo propietario en la base de datos
        public void InsertarPropietario(string dni, string nombres, string apellidos, string correo, string telefono, string direccion)
        {
            using (SqlConnection connection = _conexionSQL.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand("InsertarPropietario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DNI", dni);
                    command.Parameters.AddWithValue("@Nombres", nombres);
                    command.Parameters.AddWithValue("@Apellidos", apellidos);
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Modifica un propietario existente en la base de datos
        public void ModificarPropietario(string dni, string nombres, string apellidos, string correo, string telefono, string direccion)
        {
            using (SqlConnection connection = _conexionSQL.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand("ModificarPropietario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DNI", dni);
                    command.Parameters.AddWithValue("@NOMBRES", nombres);
                    command.Parameters.AddWithValue("@APELLIDOS", apellidos);
                    command.Parameters.AddWithValue("@CORREO", correo);
                    command.Parameters.AddWithValue("@TELEFONO", telefono);
                    command.Parameters.AddWithValue("@DIRECCION", direccion);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Elimina un propietario de la base de datos
        public void EliminarPropietario(string dni)
        {
            using (SqlConnection connection = _conexionSQL.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand("EliminarPropietario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DNI", dni);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Busca un propietario por DNI y devuelve los resultados en un DataTable
        public DataTable BuscarPropietarioPorDNI(string dni)
        {
            using (SqlConnection connection = _conexionSQL.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand("BuscarPropietarioPorDNI", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DNI", dni);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}
