using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSoftPF
{
    internal class UsuarioConectado
    {

        private static string _usuario;

        public static string NombreCompleto { get; set; }
        public static string ApellidoCompleto { get; set; }

        public static string Usuario
        {
            get { return _usuario; }
            set { _usuario = value.ToUpper(); }
        }

        public static string Permiso { get; set; }
        public static string Zona { get; set; }
        public static string Equipo { get; set; }
        public static string Rol { get; set; }
        public static string Pais { get; set; }
        public static int IdUsuario { get; set; }
    }
}
