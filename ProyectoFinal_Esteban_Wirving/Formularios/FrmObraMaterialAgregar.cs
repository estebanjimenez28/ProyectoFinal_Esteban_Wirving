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
    public partial class FrmObraMaterialAgregar : Form
    {
        DataTable ListaMateriales { get; set; }

        Material MiMaterialLocal { get; set; }  
        public FrmObraMaterialAgregar()
        {
            InitializeComponent();
            ListaMateriales= new DataTable();   
            MiMaterialLocal = new Material();   
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if(DgvLista.SelectedRows.Count==1)
            {
                DataGridViewRow row = DgvLista.SelectedRows[0];

                int IDMaterial = Convert.ToInt32(row.Cells["CCodigo_Material"].Value);

                string TipoMaterial = Convert.ToString(row.Cells["CTipo_Material"].Value);
                Decimal Precio = Convert.ToDecimal(row.Cells["CPrecio"].Value);
           
                decimal Cantidad = NumUDCantidad.Value;

                DataRow Mifila = Globales.MiFormRegistroMaterial.ListaMateriales.NewRow();

                Mifila["MaterialCodigo_Material"] = IDMaterial;
                Mifila["Precio"] = Precio;
                Mifila["Cantidad"] = Cantidad;
                Mifila["Tipo_Material"] = TipoMaterial;

                Globales.MiFormRegistroMaterial.ListaMateriales.Rows.Add(Mifila);
                DialogResult = DialogResult.OK;
            }


            DialogResult = DialogResult.OK;
        }

        private void FrmObraMaterialAgregar_Load(object sender, EventArgs e)
        {
            LlenarLista();
        }
        private void LlenarLista()
        {
            ListaMateriales = new DataTable();
            ListaMateriales = MiMaterialLocal.ListarEnBusqueda();

            DgvLista.DataSource = ListaMateriales;
            DgvLista.ClearSelection();
        }
    }
}
