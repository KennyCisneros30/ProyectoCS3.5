using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaDatos.Interface;
using System.Data;


namespace CapaNegocio
{
    public class NPropietario
    {
        public readonly Propietario _propietario; // Objeto de la clase Propietario

        // Constructor que recibe una instancia de Propietario
        public NPropietario(Propietario propietarioRepositorio)
        {
            _propietario = propietarioRepositorio;
        }

        // Guarda un nuevo propietario después de validar los campos
        public void GuardarPropietario(string dni, string nombres, string apellidos, string correo, string telefono, string direccion)
        {

            // Validación de que todos los campos están completados
            if (string.IsNullOrWhiteSpace(dni) ||
                string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion))
            {
                throw new ArgumentException("Todos los campos deben ser completados.");
            }

            // Llama al método InsertarPropietario del objeto Propietario
            _propietario.InsertarPropietario(dni, nombres, apellidos, correo, telefono, direccion);
        }

        // Modifica un propietario existente después de validar los campos
        public void ModificarPropietario(string dni, string nombres, string apellidos, string correo, string telefono, string direccion)
        {
            if (string.IsNullOrWhiteSpace(dni) ||
                string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion))
            {
                throw new ArgumentException("Todos los campos deben ser completados.");
            }
            // Llama al método ModificarPropietario del objeto Propietario
            _propietario.ModificarPropietario(dni, nombres, apellidos, correo, telefono, direccion);
        }

        // Elimina un propietario después de validar el campo DNI
        public void EliminarPropietario(string dni)
        {

            // Validación de que el campo DNI está completado
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("El DNI debe ser proporcionado.");
            }

            _propietario.EliminarPropietario(dni);
        }

        // Busca un propietario por DNI después de validar el campo DNI
        public DataTable BuscarPropietarioPorDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("El DNI debe ser proporcionado.");
            }

            // Llama al método BuscarPropietarioPorDNI del objeto Propietario y devuelve el resultado
            return _propietario.BuscarPropietarioPorDNI(dni);
        }
    }
}
