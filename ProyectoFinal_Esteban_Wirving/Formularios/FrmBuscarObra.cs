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
    public partial class FrmBuscarObra : Form
    {
        DataTable DtLista { get; set; }

        Obra MiObraLocal { get; set; }

        public FrmBuscarObra()
        {
            InitializeComponent();
            DtLista = new DataTable();
            MiObraLocal = new Obra();
        }

        private void FrmBuscarObra_Load(object sender, EventArgs e)
        {
            LlenarLista();
        }

        private void LlenarLista()
        {
            DtLista = new DataTable();
            DtLista = MiObraLocal.Listar(true, TxtBuscar.Text.Trim());

            DgvLista.DataSource = DtLista;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if(TxtBuscar.Text.Count() > 2 || string.IsNullOrEmpty(TxtBuscar.Text.Trim()))
            {
                LlenarLista();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if(DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow row = DgvLista.SelectedRows[0];

                int IDObra = Convert.ToInt32(row.Cells["CID_Obra"].Value);
                
                string Estado = Convert.ToString(row.Cells["CEstado"].Value);

                Globales.MiFormRegistroMaterial.MiMaterialLocal.MiObra.Estado = Estado;
                Globales.MiFormRegistroMaterial.MiMaterialLocal.MiObra.IDObra= IDObra; 

            DialogResult = DialogResult.OK;
            }
        }
    }
}
