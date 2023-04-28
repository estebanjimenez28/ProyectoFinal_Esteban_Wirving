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
    public class Material
    {
        //propiedades simples
        public int CodigoMaterial { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public string TipoMaterial { get; set; }
        public int IDProveedor { get; set; }
        public bool Activo { get; set; }

        public Proveedor MiProveedor { get; set; }
        public Obra MiObra { get; set; }
        public Categoria_Obra MiCategoria { get; set; }

        public List<Detalle_Material> ListaDetalles { get; set; }






        public Material()
        {
            MiProveedor = new Proveedor();
            MiObra = new Obra();
            MiCategoria = new Categoria_Obra();
            ListaDetalles = new List<Detalle_Material>();



        }
        //Funciones
        public DataTable CargarEsquemaDetalle()
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            R = MiCnn.EjecutarSELECT("SPDetalleMaterialEsquema",true);

            R.PrimaryKey = null;
            return R;
        }

        //Funciones y metodos 
        public bool Agregar()
        {

            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@cantidad", this.Cantidad));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@precio", this.Precio));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@TipoMaterial", this.TipoMaterial));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDProveedor", this.IDProveedor));

            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPMaterialAgregar");

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
            MiCnn.ListaDeParametros.Add(new SqlParameter("@cantidad", this.Cantidad));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@precio", this.Precio));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@TipoMaterial", this.TipoMaterial));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@IDProveedor", this.IDProveedor));


            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoMaterial", this.CodigoMaterial));


            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPMaterialModificar");

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

            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoMaterial", this.CodigoMaterial));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPMaterialDesactivar");

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

            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoMaterial", this.CodigoMaterial));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPMaterialActivar");

            if (respuesta > 0)
            {
                R = true;
            }

            return R;
        }
 
        public bool ConsultarPorTipoMaterial()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@TipoMaterial", this.TipoMaterial));

            DataTable consulta = new DataTable();

            consulta = MiCnn.EjecutarSELECT("SPMaterialConsultarPorTipo");

            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorCodigo()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoMaterial", this.CodigoMaterial));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPMaterialConsultarPorCodigo");

            if (dt != null && dt.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public Material ConsultarPorCodigoRetornaMaterial()
        {
            Material R = new Material();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("CodigoMaterial", this.CodigoMaterial));

            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPMaterialConsultarPorCodigo");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.CodigoMaterial = Convert.ToInt32(dr["Codigo_Material"]);
                R.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                R.Precio = Convert.ToInt32(dr["Precio"]);
                R.TipoMaterial = Convert.ToString(dr["Tipo_Material"]);
                R.IDProveedor = Convert.ToInt32(dr["ID_Provedor"]);

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


            R = MiCnn.EjecutarSELECT("SPListarMaterial");

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

            R = MiCnn.EjecutarSELECT("SPListarMaterial");

            return R;
        }
        public DataTable ListarEnBusqueda()
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            R = MiCnn.EjecutarSELECT("SPMaterialObraBusquedaListar");
            return R;
        }






    }
}
