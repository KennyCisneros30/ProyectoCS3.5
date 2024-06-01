using System;
using System.Windows.Forms;
using CapaDatos.Interface;
using CapaDatos;
using CapaNegocio;
using ProyectoCS.Controlador;

namespace CapaVisual
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Crear instancia de la capa de datos
            ConeccionSQL conexionSQL = new ConeccionSQL();

            // Crear instancia de la capa de negocio
            Propietario propietarioRepositorio = new Propietario(conexionSQL);

            // Crear instancia del formulario y pasar la capa de negocio
            Application.Run(new MenuPrincipal());
        }
    }
}
