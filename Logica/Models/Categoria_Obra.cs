using Logica.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Categoria_Obra
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } 
        public string Descrpcion { get; set; }  

        public DataTable Listar()
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            R = MiCnn.EjecutarSELECT("SPCategoriaObraListar");

            return R;
        }
    }
}
