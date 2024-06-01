using System;
using System.Data.SqlClient;

namespace ProyectoCS.Controlador
{
    public class ConeccionSQL : IDisposable
    {
        // Objeto para la conexión a la base de datos
        private SqlConnection conexion;
        // Cadena de conexión a la base de datos
        private string connectionString;

        public ConeccionSQL()
        {
            // Inicializa la cadena de conexión (modificar según configuración)
            connectionString = "Data Source=DESKTOP-UV3E916\\SQLEXPRESS;Initial Catalog=CS6-3;Integrated Security=True";
            conexion = new SqlConnection(connectionString);
        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                if (conexion == null)
                {
                    // Crear una nueva instancia de SqlConnection si es nula
                    conexion = new SqlConnection(connectionString);
                }

                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    // Abre la conexión si no está ya abierta
                    conexion.Open();
                }

                return conexion;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades (puedes lanzarla nuevamente o loggearla)
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                Console.WriteLine("Cadena de conexión: " + connectionString);
                throw new Exception("Error al abrir la conexión: " + ex.Message);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                    // Cierra la conexión si está abierta
                    conexion.Close();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades (puedes lanzarla nuevamente o loggearla)
                throw new Exception("Error al cerrar la conexión: " + ex.Message);
            }
            finally
            {
                conexion.Dispose(); // Liberar recursos
            }
        }

        public void Dispose()
        {
            // Llama a CerrarConexion para asegurar la liberación de recursos
            CerrarConexion();
            // Indica al recolector de basura que no llame al finalizador
            GC.SuppressFinalize(this);
        }
    }
}
