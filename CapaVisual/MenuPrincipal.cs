using CapaNegocio;
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
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPropietario formuPropietario = new frmPropietario();
            formuPropietario.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmVehiculos formuVehiculos = new frmVehiculos();
            formuVehiculos.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "Este programa fue realizado para ser presentado como proyecto de Construcción de Software del curso MA6-3 por parte del Grupo F:\n\n" +
                             "- Armijos Moreira Kenneth Fernando\n\n" +
                             "- Cisneros Alcivar Kenny Geampiere (LÍDER)\n\n" +
                             "- Mendoza Mendoza Cesar Oscar\n\n" +
                             "- Sánchez Marcillo Angelo Paul\n\n" +
                             "- Solís Salazar Alejandro Sebastian\n\n" +
                             "- Yagual Alarcón Mariana de Jesús";
            MessageBox.Show(mensaje, "Información del Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
