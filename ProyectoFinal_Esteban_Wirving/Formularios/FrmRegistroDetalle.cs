using Logica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_Esteban_Wirving.Formularios
{
    public partial class FrmRegistroDetalle : Form

    {

       public Material MiMaterialLocal { get; set; }  

         public DataTable ListaMateriales { get; set; } 


        public FrmRegistroDetalle()
        {
            InitializeComponent();
            MiMaterialLocal = new Material();

                      
        }

        private void FrmRegistroDetalle_Load(object sender, EventArgs e)
        {
            
            MdiParent = Globales.MiFormPrincipal;

            CargarCategoriaObra();

            LimpiarForm();

          
            
        }
        private void CargarCategoriaObra()
        {
            DataTable dtCategoriaObra = new DataTable();
            dtCategoriaObra = MiMaterialLocal.MiCategoria.Listar();
            CboxCategoriaObra.ValueMember = "id";
            CboxCategoriaObra.DisplayMember = "descripcion";
            CboxCategoriaObra.DataSource = dtCategoriaObra;
            CboxCategoriaObra.SelectedIndex = -1;
        }


        private void LimpiarForm() 
        {
            TxtEstado.Clear();
            TxtNotas.Clear();
            TxtTotal.Text = "0";
            TxtTotalCantidad.Text = "0";
            CboxCategoriaObra.SelectedIndex = -1;

            ListaMateriales = MiMaterialLocal.CargarEsquemaDetalle();



        }

  

        private void BtnObraBuscar_Click(object sender, EventArgs e)
        {
            // se abrel un nuevo formulario de busqueda de la obra

            Form FormBusquedaObra = new FrmBuscarObra();  
            
            DialogResult respuesta = FormBusquedaObra.ShowDialog();

            if(respuesta ==DialogResult.OK) 
            {
                TxtEstado.Text = MiMaterialLocal.MiObra.Estado;

            }
        }

        private void BtnMaterialAgregar_Click(object sender, EventArgs e)
        {
            Form MiFormBusqueda = new FrmObraMaterialAgregar();

            DialogResult respuesta = MiFormBusqueda.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                DgvLista.DataSource = ListaMateriales;

                Totalizar();
            }
        }

            private void Totalizar()
            {
                if(ListaMateriales.Rows.Count > 0)
                {
                decimal totalItems = 0;
                decimal totalMonto = 0;

                foreach(DataRow row in ListaMateriales.Rows)
                {
                    totalItems += Convert.ToDecimal(row["Cantidad"]);
                    // totalItems = totalItems +algo

                    totalMonto += Convert.ToDecimal(row["Precio"]) * Convert.ToDecimal(row["Cantidad"]);

                }
                TxtTotalCantidad.Text = totalItems.ToString();
                TxtTotal.Text = string.Format("{0:C2}",totalMonto);
                }
            }
    }

        
    }

