using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Interface;

namespace CapaNegocio
{
    public class NVehiculo
    {
        public readonly Vehiculo _vehiculo; // Objeto de la clase Vehiculo

        // Constructor que recibe una instancia de Vehiculo
        public NVehiculo(Vehiculo vehiculoRepositorio)
        {
            _vehiculo = vehiculoRepositorio;
        }
        // Guarda un nuevo vehículo después de validar los campos
        public void GuardarVehiculo(string placa, decimal valor, int año, int cilindraje, string modelo, string color, int idPropietario)
        {
            // Validación de que los campos obligatorios están completados
            if (string.IsNullOrWhiteSpace(placa) ||
                string.IsNullOrWhiteSpace(modelo) ||
                string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Todos los campos obligatorios deben ser completados.");
            }

            // Llama al método InsertarVehiculo del objeto Vehiculo
            _vehiculo.InsertarVehiculo(placa, valor, año, cilindraje, modelo, color, idPropietario);
        }

        // Modifica un vehículo existente después de validar los campos
        public void ModificarVehiculo(string placa, decimal valor, int año, int cilindraje, string modelo, string color, int idPropietario)
        {
            // Validación de que los campos obligatorios están completados
            if (string.IsNullOrWhiteSpace(placa) ||
                string.IsNullOrWhiteSpace(modelo) ||
                string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Todos los campos obligatorios deben ser completados.");
            }

            _vehiculo.ModificarVehiculo(placa, valor, año, cilindraje, modelo, color, idPropietario);
        }

        // Elimina un vehículo después de validar el campo placa
        public void EliminarVehiculo(string placa)
        {
            // Validación de que el campo placa está completado
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("La placa debe ser proporcionada.");
            }

            // Llama al método EliminarVehiculo del objeto Vehiculo
            _vehiculo.EliminarVehiculo(placa);
        }

        // Busca un vehículo por placa y devuelve los resultados en un DataTable
        public DataTable BuscarVehiculoPorPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("La placa debe ser proporcionada.");
            }

            return _vehiculo.BuscarVehiculoPorPlaca(placa);
        }

    }
}
