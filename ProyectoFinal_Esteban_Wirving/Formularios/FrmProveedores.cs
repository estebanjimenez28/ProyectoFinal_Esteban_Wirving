using Logica.Models;
using ProyectoFinal_Esteban_Wirving;
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
    public partial class FrmProveedores : Form
    {
        //por orden es mejor crear objetos locales que permitan 
        //la gestión del tema que estamos tratando. 
        //usar objetos individuales en las funcion puede provocar desorden y 
        //complicar más la lectura del código fuente. 

        //objeto local para usuario 
        private Logica.Models.Proveedor MiProveedorLocal { get; set; }

        //lista local de usuarios que se visualizan en el datagridview
        private DataTable ListaProveedores { get; set; }


        public FrmProveedores()
        {
            InitializeComponent();

            MiProveedorLocal = new Logica.Models.Proveedor();
            ListaProveedores = new DataTable();
        }

     

        private void CargarListaDeProveedores()
        {
            //resetear la lista de usuarios haciendo re instancia del objeto
            ListaProveedores = new DataTable();

            //si en el cuadro de texto de búsqueda hay 3 o más caracteres se filtra la lista
            string FiltroBusqueda = "";
            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                ListaProveedores = MiProveedorLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaProveedores = MiProveedorLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaProveedores;

        }




        private void DgLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgLista.ClearSelection();
        }

        private void ActivarAgregar()
        {
            BtnAgregar.Enabled = true;
            BtnModificar.Enabled = false;
            BtnEliminar.Enabled = false;
        }

        private void ActivarEditarEliminar()
        {
            BtnAgregar.Enabled = false;
            BtnModificar.Enabled = true;
            BtnEliminar.Enabled = true;
        }

   


        private bool ValidarDatosDigitados(bool OmitirDireccion = false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtNombre_Completo.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCedula.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtTelefono.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCorreo.Text.Trim()))

            {
                if (OmitirDireccion)
                {
                   
                    R = true;
                }
                else
                {
                
                    if (!string.IsNullOrEmpty(TxtDireccion.Text.Trim()))
                    {
                        R = true;

                    }
                    else
                    {
                       
                        MessageBox.Show("Debe digitar una direccion para el proveedor", "Error de validación", MessageBoxButtons.OK);
                        TxtDireccion.Focus();
                        return false;

                    }
                }
            }
            else
            {
                //qué pasa cuando algo falta? 
                if (string.IsNullOrEmpty(TxtNombre_Completo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un nombre para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtNombre_Completo.Focus();
                    return false;
                }

                //cedula
                if (string.IsNullOrEmpty(TxtCedula.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una cédula para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtCedula.Focus();
                    return false;
                }

                //telefono
                if (string.IsNullOrEmpty(TxtTelefono.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un teléfono para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtTelefono.Focus();
                    return false;
                }

                //correo
                if (string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un correo para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtCorreo.Focus();
                    return false;
                }

            }

            return R;
        }



        private void TxtNombre_Completo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void Txt_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
        }

        private void TxtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void TxtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);
        }

        private void TxtCorreo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
            {
                if (!Validaciones.ValidarEmail(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("El formato del correo electrónico es incorrecto", "Error de validación", MessageBoxButtons.OK);

                    TxtCorreo.Focus();
                }
            }
        }

        private void TxtCorreo_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
            {
                TxtCorreo.DeselectAll();

                TxtCorreo.SelectAll();
            }
        }





        private void BtnAgregar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {
                bool CedulaOK;
                bool EmailOK;

                MiProveedorLocal = new Logica.Models.Proveedor();

                MiProveedorLocal.Nombre_Completo = TxtNombre_Completo.Text.Trim();
                MiProveedorLocal.Cedula = TxtCedula.Text.Trim();
                MiProveedorLocal.Telefono = TxtTelefono.Text.Trim();
                MiProveedorLocal.Correo = TxtCorreo.Text.Trim();
                MiProveedorLocal.Direccion = TxtDireccion.Text.Trim();


                CedulaOK = MiProveedorLocal.ConsultarPorCedula();

                EmailOK = MiProveedorLocal.ConsultarPorEmail();

                if (CedulaOK == false && EmailOK == false)
                {
                    string msg = string.Format("Esta seguro que desea agregar al proveedor {0}", MiProveedorLocal.Nombre_Completo);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {
                        bool ok = MiProveedorLocal.Agregar();
                        if (ok)
                        {
                            MessageBox.Show("Proveedor guardado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeProveedores();
                        }
                        else
                        {
                            MessageBox.Show("El Proveedor no se pudo guardar!", ":/", MessageBoxButtons.OK);

                        }
                    }


                }

                else
                {
                    //indicar al usuari si falla alguna consulta

                    if (CedulaOK)
                    {
                        MessageBox.Show("Ya existe un proveedor con la cédula digitada", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (EmailOK)
                    {
                        MessageBox.Show("Ya existe un proveedor con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados(true))
            {
                //no es necesario capturar el ID desde el cuadro de texto ya que al consultarlo (cuando seleccionamos el usuario
                //del datagrid), ya tenemos datos en el ID
                //Este ID NO PUEDE SER MODIFICADO, los demás atributos si. 

                MiProveedorLocal.Nombre_Completo = TxtNombre_Completo.Text.Trim();
                MiProveedorLocal.Cedula = TxtCedula.Text.Trim();
                MiProveedorLocal.Telefono = TxtTelefono.Text.Trim();
                MiProveedorLocal.Correo = TxtCorreo.Text.Trim();
                MiProveedorLocal.Direccion = TxtDireccion.Text.Trim();


                if (MiProveedorLocal.ConsultarPorID())
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de modificar el proveedor?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (MiProveedorLocal.Editar())
                        {
                            MessageBox.Show("El Proveedor ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();
                            CargarListaDeProveedores();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiProveedorLocal.IDProveedor > 0 && MiProveedorLocal.ConsultarPorID())
            {
                //tomando en cuenta que puedo estar viendo los usuario activos o inactivos
                //este botón podría servir tanto para activar como para desactivar los usuarios
                //El checlbox de la parte superior del forma me sirve para identificar esta 
                //acción

                if (CboxVerActivos.Checked)
                {
                    //DESACTIVAR Cliente
                    DialogResult r = MessageBox.Show("¿Está seguro de Eliminar al Proveedor?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiProveedorLocal.Eliminar())
                        {
                            MessageBox.Show("El Proveedor ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeProveedores();
                        }

                    }

                }
                else
                {
                    DialogResult r = MessageBox.Show("¿Está seguro de activar al Proveedor?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiProveedorLocal.Activar())
                        {
                            MessageBox.Show("El Proveedor ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeProveedores();
                        }

                    }




                }


            }
        }

        private void BtnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarFormulario();

            DgLista.ClearSelection();

            ActivarAgregar();
        }
        private void LimpiarFormulario()
        {

            Txt_IDProveedor.Clear();
            TxtNombre_Completo.Clear();
            TxtCedula.Clear();
            TxtTelefono.Clear();
            TxtCorreo.Clear();
            TxtDireccion.Clear();
        }

        private void DgLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgLista.SelectedRows.Count == 1)
            {
                LimpiarFormulario();

                //de la colección de filas seleccionadas (que en este caso es solo una) 
                //seleccionamos la fila en indice 0, o sea la primera 
                DataGridViewRow MiFila = DgLista.SelectedRows[0];

                //lo que necesito es el valor del ID del usuario para realizaer la consulta 
                //y traer todos los datos para llenar el objeto de usuario local 
                int ID_Proveedor = Convert.ToInt32(MiFila.Cells["CID_Provedor"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiProveedorLocal = new Logica.Models.Proveedor();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiProveedorLocal.IDProveedor = ID_Proveedor;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiProveedorLocal = MiProveedorLocal.ConsultarPorIDRetornarProveedor();

                //validamos que el usuario local tenga datos 

                if (MiProveedorLocal != null && MiProveedorLocal.IDProveedor > 0)
                {
                    //si cargamos correctamente el usuario local llenamos los controles 

                    Txt_IDProveedor.Text = Convert.ToString(MiProveedorLocal.IDProveedor);

                    TxtNombre_Completo.Text = MiProveedorLocal.Nombre_Completo;

                    TxtCedula.Text = MiProveedorLocal.Cedula;

                    TxtTelefono.Text = MiProveedorLocal.Telefono;

                    TxtCorreo.Text = MiProveedorLocal.Correo;

                    TxtDireccion.Text = MiProveedorLocal.Direccion;

                    ActivarEditarEliminar();

                }



            }
        }

        private void FrmProveedores_Load_1(object sender, EventArgs e)
        {
            //definimos el padre mdi 
            MdiParent = Globales.MiFormPrincipal;


            CargarListaDeProveedores();

            ActivarAgregar();
        }

        private void CboxVerActivos_CheckedChanged_1(object sender, EventArgs e)
        {
            CargarListaDeProveedores();

            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "ELIMINAR";
            }
            else
            {
                BtnEliminar.Text = "ACTIVAR";
            }
        }

        private void TxtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            CargarListaDeProveedores();
        }
    }
}
