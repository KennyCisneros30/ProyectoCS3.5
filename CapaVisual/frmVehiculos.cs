using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ProyectoCS.Controlador;

namespace CapaVisual
{
    public partial class frmVehiculos : Form
    {
        public frmVehiculos()
        {
            InitializeComponent();
            // Suscribir el evento TextChanged de los TextBox
            PlacaTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            ValorTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            AñoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            CilindrajeTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            ModeloTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            ColorTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            DNITextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            // Inicialmente deshabilitar el botón
            GuardarVehiculo.Enabled = false;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Comprobar si todos los TextBox tienen texto
            GuardarVehiculo.Enabled = !string.IsNullOrWhiteSpace(PlacaTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(ValorTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(AñoTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(CilindrajeTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(ModeloTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(ColorTextBox.Text) &&
                                      !string.IsNullOrWhiteSpace(DNITextBox.Text);
        }

        private void GuardarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(PlacaTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ValorTextBox.Text) ||
                    string.IsNullOrWhiteSpace(AñoTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CilindrajeTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ModeloTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ColorTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DNITextBox.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir la conexión antes de usarla
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    // Obtener valores de los TextBox
                    string PLACA = PlacaTextBox.Text;
                    decimal VALOR = Convert.ToDecimal(ValorTextBox.Text);
                    int AÑO = Convert.ToInt32(AñoTextBox.Text);
                    int CILINDRAJE = Convert.ToInt32(CilindrajeTextBox.Text);
                    string MODELO = ModeloTextBox.Text;
                    string COLOR = ColorTextBox.Text;
                    string DNI = DNITextBox.Text;

                    // Ejecutar procedimiento almacenado para insertar vehículo
                    using (SqlConnection connection = conexionSQL.AbrirConexion())
                    {
                        using (SqlCommand command = new SqlCommand("InsertarVehiculo", connection))
                        {

                            command.CommandType = CommandType.StoredProcedure;

                            // Parámetros del procedimiento almacenado
                            command.Parameters.AddWithValue("@PLACA", PLACA);
                            command.Parameters.AddWithValue("@VALOR", VALOR);
                            command.Parameters.AddWithValue("@AÑO", AÑO);
                            command.Parameters.AddWithValue("@CILINDRAJE", CILINDRAJE);
                            command.Parameters.AddWithValue("@MODELO", MODELO);
                            command.Parameters.AddWithValue("@COLOR", COLOR);
                            command.Parameters.AddWithValue("@DNI_PROPIETARIO", DNI);

                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Vehículo guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpiar los TextBox después de guardar exitosamente
                        PlacaTextBox.Clear();
                        ValorTextBox.Clear();
                        AñoTextBox.Clear();
                        CilindrajeTextBox.Clear();
                        ModeloTextBox.Clear();
                        ColorTextBox.Clear();
                        DNITextBox.Clear();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MenuPrincipal formuPrincipal = new MenuPrincipal();
            formuPrincipal.Show();
            this.Close();
        }

        private void ModificarVehiculo_Click(object sender, EventArgs e)
        {
            frmModificarVehiculo formuModificar = new frmModificarVehiculo();
            formuModificar.Show();
            this.Close();
        }
    }
}
