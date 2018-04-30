using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE_Datos
{
    public class Conexion
    {
        public static GDatos GDatos;
        public static bool IniciarSesion(string nombreServidor, string baseDatos, string usuario, string password)
        {
            GDatos = new SqlServer(nombreServidor, baseDatos, usuario, password);
            return GDatos.Autenticar(usuario, password);
        }

        public static void FinalizarSesion()
        {
            GDatos.CerrarConexion();
        }
    }
}