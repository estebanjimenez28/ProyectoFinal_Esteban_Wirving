using Logica.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Logica.Models
{
    public class Empleados
    {
        //propiedades simples
        public int IDEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public int IDObra { get; set; }
        public bool Activo { get; set; }

        //propiedaes compuestas 
        public Puesto_Empleado MiPuestoTipo { get; set; }

        //normalmente cuando tenemos propiedades compuestas con tipos que 
        //hemos programado nosotros mismos, debemos instanciar dichas propiedaes
        //ya que son Objetos. 
        //para esto recomiendo hacerlo en el constructor de la clase 
        public Empleados()
        {
            //al crear una nueva instancia de la clase Usuario, se ejecuta el código
            //de este constructor, y también se crea una nueva instacia de la clase
            //usuario_rol para el objeto MiRolTipo.
            MiPuestoTipo = new Puesto_Empleado();
        }

        //Funciones y métodos 
        public bool Agregar()
        {
            //cuando la función devuelve un bool me gusta inicializar la variable de 
            //retorno en false (tiende a negativo el resultado de la fn) 
            //y SOLO si la operación (en este caso Insert) sale correcta 
            // se cambia el valor de R a true.
            bool R = false;

            //pasos 1.6.1 y 1.6.2 
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@NombreCompleto", this.NombreCompleto));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));
            //normalmente los foreign keys tienen que ver con composiciones, en este caso 
            //tenemos que extraer el valor del objeto compuesto 'MiPuestoTipo'

            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDPuesto", this.MiPuestoTipo.IDPuestoEmpleado));

            //pasos 1.6.3 y 1.6.4
            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPEmpleadoAgregar");

            //paso 1.6.5
            if (resultado > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Editar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@NombreCompleto", this.NombreCompleto));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));

            //normalmente los foreign keys tienen que ver con composiciones, en este caso 
            //tenemos que extraer el valor del objeto compuesto 'MiRolTipo'
            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDPuesto", this.MiPuestoTipo.IDPuestoEmpleado));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDEmpleado));

            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPEmpleadoModificar");

            if (resultado > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Eliminar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDEmpleado));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPEmpleadoDesactivar");

            if (respuesta > 0)
            {
                R = true;
            }

            return R;
        }

        public bool Activar()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDEmpleado));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPEmpleadoActivar");

            if (respuesta > 0)
            {
                R = true;
            }

            return R;
        }

        public bool ConsultarPorID()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDEmpleado));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPEmpleadoConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public Empleados ConsultarPorIDRetornaEmpleado()
        {
            Empleados R = new Empleados();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDEmpleado));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPEmpleadoConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                //esta consulta debería tener solo un registro, 
                //se crea un objeto de tipo datarow para capturar la info 
                //contenida en index 0 del dt (datatable)
                DataRow dr = dt.Rows[0];

                R.IDEmpleado = Convert.ToInt32(dr["IDEmpleado"]);
                R.NombreCompleto = Convert.ToString(dr["NombreCompleto"]);
                R.Cedula = Convert.ToString(dr["Cedula"]);
                R.Correo = Convert.ToString(dr["Correo"]);
                R.Telefono = Convert.ToString(dr["Telefono"]);
                R.Direccion = Convert.ToString(dr["Direccion"]);
                R.IDObra = Convert.ToInt32(dr["IDObra"]);

                //composiciones
                R.MiPuestoTipo.IDPuestoEmpleado = Convert.ToInt32(dr["IDPuestoEmpleado"]);
                R.MiPuestoTipo.Descripcion= Convert.ToString(dr["Descripcion"]);
               
            }


            return R;
        }



        public bool ConsultarPorCedula()
        {
            bool R = false;

            //paso 1.3.1 y 1.3.2
            Conexion MiCnn = new Conexion();

            //agregamos el parametro de cedula 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));

            DataTable consulta = new DataTable();
            //paso 1.3.3 y 1.3.4
            consulta = MiCnn.EjecutarSELECT("SPEmpleadoConsultarPorCedula");

            //paso 1.3.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public bool ConsultarPorEmail()
        {
            bool R = false;

            //paso 1.4.1 y 1.4.2
            Conexion MiCnn = new Conexion();

            //agregamos el parametro de correo 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));

            DataTable consulta = new DataTable();
            //paso 1.4.3 y 1.4.4
            consulta = MiCnn.EjecutarSELECT("SPEmpleadoConsultarPorEmail");

            //paso 1.4.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;

        }

        public DataTable ListarActivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", true));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));

            R = MiCnn.EjecutarSELECT("SPEmpleadoListar");

            return R;
        }

        public DataTable ListarInactivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();
            Conexion MiCnn = new Conexion();

            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", false));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));


            R = MiCnn.EjecutarSELECT("SPEmpleadoListar");

            return R;
        }


    }
}