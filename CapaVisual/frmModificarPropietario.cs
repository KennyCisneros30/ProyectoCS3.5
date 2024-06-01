using CapaDatos.Interface;
using CapaNegocio;
using ProyectoCS.Controlador;
using System;
using System.Data;
using System.Windows.Forms;
namespace CapaVisual
{
    public partial class frmModificarPropietario : Form
    {
        private NPropietario _propietarioNegocio;

        public frmModificarPropietario()
        {
            InitializeComponent();
            // Inicializar conexión y repositorio
            ConeccionSQL conexionSQL = new ConeccionSQL();
            Propietario propietarioRepositorio = new Propietario(conexionSQL);
            _propietarioNegocio = new NPropietario(propietarioRepositorio);
            // Suscribir el evento TextChanged de los TextBox
            MNombresTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MApellidosTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MCorreoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MTelefonoTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MDireccionTextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            MDNITextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            // Inicialmente deshabilitar el botón
            GuardarM.Enabled = false;
            EliminarPropietario.Enabled = false;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Habilitar botones si todos los TextBox tienen texto
            GuardarM.Enabled = !string.IsNullOrWhiteSpace(MNombresTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MApellidosTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MCorreoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MTelefonoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MDireccionTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MDNITextBox.Text);
            EliminarPropietario.Enabled = !string.IsNullOrWhiteSpace(MNombresTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MApellidosTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MCorreoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MTelefonoTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MDireccionTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(MDNITextBox.Text);
        }
        private void BuscarDNI_Click(object sender, EventArgs e)
        {
            try
            {
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Propietario propietarioRepositorio = new Propietario(conexionSQL);
                    _propietarioNegocio = new NPropietario(propietarioRepositorio);

                    // Buscar propietario por DNI
                    DataTable propietario = _propietarioNegocio.BuscarPropietarioPorDNI(MBuscarTextBox.Text);

                    if (propietario.Rows.Count > 0)
                    {
                        DataRow row = propietario.Rows[0];
                        // Llenar TextBox con datos del propietario encontrado
                        MDNITextBox.Text = row["DNI"].ToString();
                        MNombresTextBox.Text = row["Nombres"].ToString();
                        MApellidosTextBox.Text = row["Apellidos"].ToString();
                        MCorreoTextBox.Text = row["Correo"].ToString();
                        MTelefonoTextBox.Text = row["Telefono"].ToString();
                        MDireccionTextBox.Text = row["Direccion"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("DNI no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar propietario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarM_Click(object sender, EventArgs e)
        {
            try
            {
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Propietario propietarioRepositorio = new Propietario(conexionSQL);
                    _propietarioNegocio = new NPropietario(propietarioRepositorio);

                    // Modificar propietario con los datos de los TextBox
                    _propietarioNegocio.ModificarPropietario(
                        MDNITextBox.Text,
                        MNombresTextBox.Text,
                        MApellidosTextBox.Text,
                        MCorreoTextBox.Text,
                        MTelefonoTextBox.Text,
                        MDireccionTextBox.Text
                    );

                    MessageBox.Show("Modificación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los TextBox después de modificar exitosamente
                    LimpiarTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar propietario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarPropietario_Click(object sender, EventArgs e)
        {
            try
            {
                using (ConeccionSQL conexionSQL = new ConeccionSQL())
                {
                    Propietario propietarioRepositorio = new Propietario(conexionSQL);
                    _propietarioNegocio = new NPropietario(propietarioRepositorio);

                    // Eliminar propietario por DNI
                    _propietarioNegocio.EliminarPropietario(MDNITextBox.Text);

                    MessageBox.Show("Propietario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los TextBox después de eliminar exitosamente
                    LimpiarTextBoxes();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar propietario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarTextBoxes()
        {
            MBuscarTextBox.Clear();
            MDNITextBox.Clear();
            MNombresTextBox.Clear();
            MApellidosTextBox.Clear();
            MCorreoTextBox.Clear();
            MTelefonoTextBox.Clear();
            MDireccionTextBox.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Navegar a frmPropietario
            frmPropietario formuPropietario = new frmPropietario();
            formuPropietario.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Navegar a MenuPrincipal
            MenuPrincipal formuPrincipal = new MenuPrincipal();
            formuPrincipal.Show();
            this.Close();
        }
    }
}