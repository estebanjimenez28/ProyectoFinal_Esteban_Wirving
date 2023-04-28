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
    public partial class FrmMDI : Form
    {
        public FrmMDI()
        {
            InitializeComponent();
        }

        private void FrmMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MnuProcesos_Click(object sender, EventArgs e)
        {

        }

        private void registroMaterialesCompradosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormRegistroMaterial.Visible)
            {
                Globales.MiFormRegistroMaterial = new FrmRegistroDetalle();
                Globales.MiFormRegistroMaterial.Show();
            }
        }

        private void mantenimientoClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormClientes.Visible)
            {
                Globales.MiFormClientes = new FrmClientes();
                Globales.MiFormClientes.Show();
            }
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            //mostrar el usuario logueado

            string InfoUsuario = string.Format("{0}-{1}({2})", Globales.MiUsuarioGlobal.Nombre,
                                                              Globales.MiUsuarioGlobal.Correo,
                                                              Globales.MiUsuarioGlobal.MiRolTipo.Descripcion);
       
        LblUsuario.Text = InfoUsuario;  
        }

        private void mantenimientoProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormProveedores.Visible)
            {
                Globales.MiFormProveedores = new FrmProveedores();
                Globales.MiFormProveedores.Show();
            }
        }

        private void mantenimientoMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormMateriales.Visible)
            {
                Globales.MiFormMateriales = new FrmMateriales();
                Globales.MiFormMateriales.Show();
            }
        }

        private void mantenimientoObrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormObras.Visible)
            {
                Globales.MiFormObras = new FrmObras();
                Globales.MiFormObras.Show();
            }
        }

        private void mantenimientoEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormEmpleados.Visible)
            {
                Globales.MiFormEmpleados = new FrmEmpleados();
                Globales.MiFormEmpleados.Show();
            }
        }

        private void mantenimientoUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormUsuarios.Visible)
            {
                Globales.MiFormUsuarios = new FrmUsuarios();
                Globales.MiFormUsuarios.Show();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
    }

