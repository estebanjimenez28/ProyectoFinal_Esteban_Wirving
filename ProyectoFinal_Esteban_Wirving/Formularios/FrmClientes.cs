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
    public partial class FrmClientes : Form
    {
        //por orden es mejor crear objetos locales que permitan 
        //la gestión del tema que estamos tratando. 
        //usar objetos individuales en las funcion puede provocar desorden y 
        //complicar más la lectura del código fuente. 

        //objeto local para usuario 
        private Logica.Models.Clientes MiClienteLocal { get; set; }

        //lista local de usuarios que se visualizan en el datagridview
        private DataTable ListaClientes { get; set; }


        public FrmClientes()
        {
            InitializeComponent();

            MiClienteLocal = new Logica.Models.Clientes();
            ListaClientes = new DataTable();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            //definimos el padre mdi 
            MdiParent = Globales.MiFormPrincipal;


            CargarListaDeClientes();

            ActivarAgregar();

        }

        private void CargarListaDeClientes()
        {
            //resetear la lista de usuarios haciendo re instancia del objeto
            ListaClientes = new DataTable();

            //si en el cuadro de texto de búsqueda hay 3 o más caracteres se filtra la lista
            string FiltroBusqueda = "";
            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                ListaClientes = MiClienteLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaClientes = MiClienteLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaClientes;

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

        private void DgLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //cuando seleccionemos una fila del datagrid se debe cargar la info de dicho usuario
            //en el usuario local y luego dibujar esa info en los controles graficos 

            if (DgLista.SelectedRows.Count == 1)
            {
                LimpiarFormulario();

                //de la colección de filas seleccionadas (que en este caso es solo una) 
                //seleccionamos la fila en indice 0, o sea la primera 
                DataGridViewRow MiFila = DgLista.SelectedRows[0];

                //lo que necesito es el valor del ID del usuario para realizaer la consulta 
                //y traer todos los datos para llenar el objeto de usuario local 
                int ID_Cliente = Convert.ToInt32(MiFila.Cells["CID_Cliente"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiClienteLocal = new Logica.Models.Clientes();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiClienteLocal.IDCliente = ID_Cliente;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiClienteLocal = MiClienteLocal.ConsultarPorIDRetornarCliente();

                //validamos que el usuario local tenga datos 

                if (MiClienteLocal != null && MiClienteLocal.IDCliente > 0)
                {
                    //si cargamos correctamente el usuario local llenamos los controles 

                    TxtID_Cliente.Text = Convert.ToString(MiClienteLocal.IDCliente);

                    TxtNombre_Completo.Text = MiClienteLocal.Nombre_Completo;

                    TxtCedula.Text = MiClienteLocal.Cedula;

                    Txt_Telefono.Text = MiClienteLocal.Telefono;

                    TxtCorreo.Text = MiClienteLocal.Correo;

                    TxtDireccion.Text = MiClienteLocal.Direccion;

                    ActivarEditarEliminar();

                }



            }


        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            DgLista.ClearSelection();

            ActivarAgregar();
        }

        private void LimpiarFormulario()
        {
            TxtID_Cliente.Clear();
            TxtNombre_Completo.Clear();
            TxtCedula.Clear();
            Txt_Telefono.Clear();
            TxtCorreo.Clear();
            TxtDireccion.Clear();
        }


        private bool ValidarDatosDigitados(bool OmitirDireccion = false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtNombre_Completo.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCedula.Text.Trim()) &&
                !string.IsNullOrEmpty(Txt_Telefono.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCorreo.Text.Trim()))

            {
                if (OmitirDireccion)
                {
                    //(PARA EDITAR) Si el password se omite entonces ya pasó la evaluacíón a este punto, retorna true
                    R = true;
                }
                else
                {
                    //(PARA AGREGAR) en caso en el que haya que evaluar la contraseña se debe agregar otra condición 
                    //logica
                    if (!string.IsNullOrEmpty(TxtDireccion.Text.Trim()))
                    {
                        R = true;

                    }
                    else
                    {
                        //en caso en el que haga falta la contraseña, se le indica al usuario
                        MessageBox.Show("Debe digitar una direccion para el cliente", "Error de validación", MessageBoxButtons.OK);
                        TxtDireccion.Focus();
                        return false;

                    }
                }
            }
            else {
                //qué pasa cuando algo falta? 
                if (string.IsNullOrEmpty(TxtNombre_Completo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un nombre para el cliente", "Error de validación", MessageBoxButtons.OK);
                    TxtNombre_Completo.Focus();
                    return false;
                }

                //cedula
                if (string.IsNullOrEmpty(TxtCedula.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una cédula para el cliente", "Error de validación", MessageBoxButtons.OK);
                    TxtCedula.Focus();
                    return false;
                }

                //telefono
                if (string.IsNullOrEmpty(Txt_Telefono.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un teléfono para el cliente", "Error de validación", MessageBoxButtons.OK);
                    Txt_Telefono.Focus();
                    return false;
                }

                //correo
                if (string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un correo para el cliente", "Error de validación", MessageBoxButtons.OK);
                    TxtCorreo.Focus();
                    return false;
                }

            }

            return R;
        }



        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {
                bool CedulaOK;
                bool EmailOK;

                MiClienteLocal = new Logica.Models.Clientes();

                MiClienteLocal.Nombre_Completo = TxtNombre_Completo.Text.Trim();
                MiClienteLocal.Cedula = TxtCedula.Text.Trim();
                MiClienteLocal.Telefono = Txt_Telefono.Text.Trim();
                MiClienteLocal.Correo = TxtCorreo.Text.Trim();
                MiClienteLocal.Direccion = TxtDireccion.Text.Trim();


                CedulaOK = MiClienteLocal.ConsultarPorCedula();

                EmailOK = MiClienteLocal.ConsultarPorEmail();

                if (CedulaOK == false && EmailOK == false)
                {
                    string msg = string.Format("Esta seguro que desea agregar al cliente {0}", MiClienteLocal.Nombre_Completo);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {
                        bool ok = MiClienteLocal.Agregar();
                        if (ok)
                        {
                            MessageBox.Show("Cliente guardado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeClientes();
                        }
                        else
                        {
                            MessageBox.Show("El Cliente no se pudo guardar!", ":/", MessageBoxButtons.OK);

                        }
                    }


                }

                else
                {
                    //indicar al usuari si falla alguna consulta

                    if (CedulaOK)
                    {
                        MessageBox.Show("Ya existe un cliente con la cédula digitada", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (EmailOK)
                    {
                        MessageBox.Show("Ya existe un cliente con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
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

        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarListaDeClientes();

            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "ELIMINAR";
            }
            else
            {
                BtnEliminar.Text = "ACTIVAR";
            }

        }

   

        private void BtnModificar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados(true))
            {
                //no es necesario capturar el ID desde el cuadro de texto ya que al consultarlo (cuando seleccionamos el usuario
                //del datagrid), ya tenemos datos en el ID
                //Este ID NO PUEDE SER MODIFICADO, los demás atributos si. 

                MiClienteLocal.Nombre_Completo = TxtNombre_Completo.Text.Trim();
                MiClienteLocal.Cedula = TxtCedula.Text.Trim();
                MiClienteLocal.Telefono = Txt_Telefono.Text.Trim();
                MiClienteLocal.Correo = TxtCorreo.Text.Trim();
                MiClienteLocal.Direccion = TxtDireccion.Text.Trim();


                if (MiClienteLocal.ConsultarPorID())
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de modificar el cliente?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (MiClienteLocal.Editar())
                        {
                            MessageBox.Show("El Cliente ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();
                            CargarListaDeClientes();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click_1(object sender, EventArgs e)
        {
            if (MiClienteLocal.IDCliente > 0 && MiClienteLocal.ConsultarPorID())
            {
                //tomando en cuenta que puedo estar viendo los usuario activos o inactivos
                //este botón podría servir tanto para activar como para desactivar los usuarios
                //El checlbox de la parte superior del forma me sirve para identificar esta 
                //acción

                if (CboxVerActivos.Checked)
                {
                    //DESACTIVAR Cliente
                    DialogResult r = MessageBox.Show("¿Está seguro de Eliminar al Cliente?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiClienteLocal.Eliminar())
                        {
                            MessageBox.Show("El Cliente ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeClientes();
                        }

                    }

                }
                else
                {
                    //ACTIVAR Cliente
                    DialogResult r = MessageBox.Show("¿Está seguro de Activar al Cliente?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiClienteLocal.Activar())
                        {
                            MessageBox.Show("El Cliente ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeClientes();
                        }

                    }




                }


            }
        }

        private void TxtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            CargarListaDeClientes();

        }
    }
}