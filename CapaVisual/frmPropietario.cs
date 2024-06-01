using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos.Interface;
using CapaNegocio;
using ProyectoCS.Controlador;
using System.Data.SqlClient;

namespace CapaVisual
{
    public partial class frmPropietario : Form
    {
        public frmPropietario()
        {
            InitializeComponent();
            // Suscribir el evento TextChanged de los TextBox
            NombresTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            ApellidosTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            CorreoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            TelefonoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            DireccionTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            DNITextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            // Inicialmente deshabilitar el botón
            GuardarPropietario.Enabled = false;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Comprobar si todos los TextBox tienen texto
            GuardarPropietario.Enabled = !string.IsNullOrWhiteSpace(NombresTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(ApellidosTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(CorreoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(TelefonoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(DireccionTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(DNITextBox.Text);
        }
        private void GuardarPropietario_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(NombresTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ApellidosTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CorreoTextBox.Text) ||
                    string.IsNullOrWhiteSpace(TelefonoTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DireccionTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DNITextBox.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir la conexión antes de usarla
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    // Obtener valores de los TextBox
                    string NOMBRES = NombresTextBox.Text;
                    string APELLIDOS = ApellidosTextBox.Text;
                    string CORREO = CorreoTextBox.Text;
                    string TELEFONO = TelefonoTextBox.Text;
                    string DIRECCION = DireccionTextBox.Text;
                    string DNI = DNITextBox.Text;

                    // Ejecutar procedimiento almacenado para asignar datos del usuario
                    using (SqlConnection connection = conexionSQL.AbrirConexion())
                    {
                        using (SqlCommand command = new SqlCommand("InsertarPropietario", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Parámetros del procedimiento almacenado
                            command.Parameters.AddWithValue("@Nombres", NOMBRES);
                            command.Parameters.AddWithValue("@Apellidos", APELLIDOS);
                            command.Parameters.AddWithValue("@Correo", CORREO);
                            command.Parameters.AddWithValue("@Telefono", TELEFONO);
                            command.Parameters.AddWithValue("@Direccion", DIRECCION);
                            command.Parameters.AddWithValue("@DNI", DNI);

                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Propietario guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los TextBox después de guardar exitosamente
                DNITextBox.Clear();
                NombresTextBox.Clear();
                ApellidosTextBox.Clear();
                CorreoTextBox.Clear();
                TelefonoTextBox.Clear();
                DireccionTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar propietario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MenuPrincipal formuPrincipal = new MenuPrincipal();
            formuPrincipal.Show();
            this.Close();
        }

        private void ModificarPropietario_Click(object sender, EventArgs e)
        {
            frmModificarPropietario formuModificar = new frmModificarPropietario();
            formuModificar.Show();
            this.Close();
        }
    }
}