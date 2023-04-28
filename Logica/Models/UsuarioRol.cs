using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class UsuarioRol
    {
        //Primero se digitan las propiedades de la clase 

        public int IDRol { get; set; }

        //private int usuarioRolID;
        //public int UsuarioRolID
        //{
        //    get { return usuarioRolID; }
        //    set { usuarioRolID = value; }
        //}

        public string Descripcion { get; set; }

        //luego de escribir las propiedades simples se digitan 
        //las propiedades compuestas (en este caso no hay)  

        //Por ultimo se escriben las funciones y métodos. 
        public DataTable Listar()
        {
            DataTable R = new DataTable();

            Services.Conexion MiCnn = new Services.Conexion();

            R = MiCnn.EjecutarSELECT("SPUsuarioRolListar");

            return R;

        }


    }
}
