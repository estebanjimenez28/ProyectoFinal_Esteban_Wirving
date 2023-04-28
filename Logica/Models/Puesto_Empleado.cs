using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Puesto_Empleado
    {
        //Primero se digita las propiedades de la clase  
        public int IDPuestoEmpleado { get; set; }

  
        public string Descripcion { get; set; }

        //luego de escribir las porpiedades simples se digitan
        // las propiedades compuestas(en este caso no hay )

        //Por ultimo se escriben las funciones y metodos.

        public DataTable Listar()
        {

            DataTable R = new DataTable();
            Services.Conexion MiCnn = new Services.Conexion();
            R = MiCnn.EjecutarSELECT("SPEmpleadoPuestoListar");

            return R;



        }
    }
}
