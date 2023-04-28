using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Detalle_Material
    {
        public int IDDetalle { get; set; }

        public DateTime Fecha_Compra { get; set; }
        public int Cantidad { get; set; }   
        public int PrecioUnitario { get; set; } 


        //composiciones simples

        public Obra MiObra { get; set; }

        public Material MiMaterial { get; set; }
        public Categoria_Obra MiCategoria { get; set; }

        public Detalle_Material()
        { 
            MiMaterial = new Material();    
        }


        //composiciones en lista o complejas


    }
}
