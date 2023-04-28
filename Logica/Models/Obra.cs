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
    public class Obra
    {
        //propiedades simples
        public int IDObra { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Estado { get; set; }
        public bool Activo { get; set; }

        public Clientes MiClienteTipo { get; set; }

        public Categoria_Obra MiCategoria;
        public Obra()
        {
            MiClienteTipo = new Clientes();

            MiCategoria = new Categoria_Obra();

        }

        //Funciones y metodos 
        public bool Agregar()
        {

            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Fecha_Inicio", this.FechaInicio));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Fecha_Final", this.FechaFinal));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Estado", this.Estado));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID_Cliente", this.MiClienteTipo.IDCliente));

            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPObraAgregar");

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
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FechaInicio", this.FechaInicio));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FechaFinal", this.FechaFinal));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Estado", this.Estado));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID_Cliente", this.MiClienteTipo.IDCliente));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));


            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPObraModificar");

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

            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPObraDesactivar");

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

            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPObraActivar");

            if (respuesta > 0)
            {
                R = true;
            }

            return R;
        }

        public bool ConsultarPorFecha()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@FechaFinal", this.FechaFinal));

            DataTable consulta = new DataTable();

            consulta = MiCnn.EjecutarSELECT("SPObraConsultarPorFecha");

            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorID()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDObra", this.IDObra));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPObraConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public Obra ConsultarPorIDRetornaObra()
        {
            Obra R = new Obra();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("IDObra", this.IDObra));

            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPObraConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.IDObra = Convert.ToInt32(dr["ID_Obra"]);
                R.FechaInicio = Convert.ToDateTime(dr["Fecha_Inicio"]);
                R.FechaFinal = Convert.ToDateTime(dr["Fecha_Final"]);
                R.Estado = Convert.ToString(dr["Estado"]);

                R.MiClienteTipo.IDCliente = Convert.ToInt32(dr["ID_Cliente"]);
               

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


            R = MiCnn.EjecutarSELECT("SPObraListar");

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

            R = MiCnn.EjecutarSELECT("SPObraListar");

            return R;
        }

        public DataTable Listar(bool verActivos = true, string FiltroBusqueda = "")
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("VerActivos", verActivos));
            MiCnn.ListaDeParametros.Add(new SqlParameter("FiltroBusqueda", FiltroBusqueda));

            R = MiCnn.EjecutarSELECT("SPBuscarObraListar");

            return R;
        }
        

       





    }
}

