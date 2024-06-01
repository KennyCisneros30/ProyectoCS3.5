using CapaDatos.Interface;
using CapaNegocio;
using ProyectoCS.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVisual
{
    public partial class frmModificarVehiculo : Form
    {
        public frmModificarVehiculo()
        {
            InitializeComponent();
            // Inicializar conexión y repositorio
            ConeccionSQL conexionSQL = new ConeccionSQL();
            Vehiculo vehiculoRepositorio = new Vehiculo(conexionSQL);
            NVehiculo vehiculoNegocio = new NVehiculo(vehiculoRepositorio);

            // Suscribir el evento TextChanged de los TextBox
            MVPlacaTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVValorTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVAñoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVCilindrajeTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVModeloTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVColorTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MVDNITextBox.TextChanged += new EventHandler(TextBox_TextChanged);

            // Inicialmente deshabilitar el botón
            GuardarV.Enabled = false;
            EliminarVehiculo.Enabled = false;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Habilitar el botón Guardar si todos los TextBox tienen texto
            GuardarV.Enabled = !string.IsNullOrWhiteSpace(MVPlacaTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVValorTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVAñoTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVCilindrajeTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVModeloTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVColorTextBox.Text) &&
                             !string.IsNullOrWhiteSpace(MVDNITextBox.Text);
            // Habilitar el botón Eliminar si MVPlacaTextBox tiene texto
            EliminarVehiculo.Enabled = !string.IsNullOrWhiteSpace(MVPlacaTextBox.Text);
        }

        private void BuscarPlaca_Click(object sender, EventArgs e)
        {
            try
            {
                // Buscar vehículo por placa
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Vehiculo vehiculoRepositorio = new Vehiculo(conexionSQL);
                    NVehiculo vehiculoNegocio = new NVehiculo(vehiculoRepositorio);

                    DataTable vehiculo = vehiculoNegocio.BuscarVehiculoPorPlaca(MVPlacaBuscarTextBox.Text);

                    if (vehiculo.Rows.Count > 0)
                    {
                        DataRow row = vehiculo.Rows[0];
                        // Llenar TextBox con datos del vehículo encontrado
                        MVPlacaTextBox.Text = row["PLACA"].ToString();
                        MVValorTextBox.Text = row["VALOR"].ToString();
                        MVAñoTextBox.Text = row["AÑO"].ToString();
                        MVCilindrajeTextBox.Text = row["CILINDRAJE"].ToString();
                        MVModeloTextBox.Text = row["MODELO"].ToString();
                        MVColorTextBox.Text = row["COLOR"].ToString();
                        MVDNITextBox.Text = row["DNI"].ToString(); // Cambiar a "DNI" en lugar de "ID_PROPIETARIO"
                    }
                    else
                    {
                        MessageBox.Show("Vehículo no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar vehículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarV_Click(object sender, EventArgs e)
        {
            try
            {
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Vehiculo vehiculoRepositorio = new Vehiculo(conexionSQL);
                    NVehiculo vehiculoNegocio = new NVehiculo(vehiculoRepositorio);
                    // Modificar vehículo con los datos de los TextBox
                    vehiculoNegocio.ModificarVehiculo(
                        MVPlacaTextBox.Text,
                        Convert.ToDecimal(MVValorTextBox.Text),
                        Convert.ToInt32(MVAñoTextBox.Text),
                        Convert.ToInt32(MVCilindrajeTextBox.Text),
                        MVModeloTextBox.Text,
                        MVColorTextBox.Text,
                        Convert.ToInt32(MVDNITextBox.Text)
                    );

                    MessageBox.Show("Modificación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los TextBox después de modificar exitosamente
                    LimpiarTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar vehículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                // Eliminar vehículo por placa
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Vehiculo vehiculoRepositorio = new Vehiculo(conexionSQL);
                    NVehiculo vehiculoNegocio = new NVehiculo(vehiculoRepositorio);

                    vehiculoNegocio.EliminarVehiculo(MVPlacaTextBox.Text);

                    MessageBox.Show("Vehículo eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los TextBox después de eliminar exitosamente
                    LimpiarTextBoxes();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LimpiarTextBoxes()
        {
            MVPlacaBuscarTextBox.Clear();
            MVPlacaTextBox.Clear();
            MVValorTextBox.Clear();
            MVAñoTextBox.Clear();
            MVCilindrajeTextBox.Clear();
            MVModeloTextBox.Clear();
            MVColorTextBox.Clear();
            MVDNITextBox.Clear();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Navegar a frmVehiculos
            frmVehiculos formuVehiculos = new frmVehiculos();
            formuVehiculos.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Navegar a MenuPrincipal
            MenuPrincipal formuPrincipal = new MenuPrincipal();
            formuPrincipal.Show();
            this.Close();
        }
    }
}
