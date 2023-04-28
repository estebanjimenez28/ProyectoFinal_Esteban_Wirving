
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
    public partial class FrmEmpleados : Form
    {
        //por orden es mejor crear objetos locales que permitan 
        //la gestión del tema que estamos tratando. 
        //usar objetos individuales en las funcion puede provocar desorden y 
        //complicar más la lectura del código fuente. 

        //objeto local para el empleado
        private Logica.Models.Empleados MiEmpleadoLocal { get; set; }

        //lista local de usuarios que se visualizan en el datagridview
        private DataTable ListaEmpleados { get; set; }


        public FrmEmpleados()
        {
            InitializeComponent();

            MiEmpleadoLocal = new Logica.Models.Empleados();
            ListaEmpleados = new DataTable();
        }
   
        private void CargarListaDeEmpleados()
        {
            //resetear la lista de usuarios haciendo re instancia del objeto
            ListaEmpleados = new DataTable();

            string FiltroBusqueda = "";

            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                //si en el cuadro de texto de busqueda hay 3 o mas caracteres se filtra la lista




                ListaEmpleados = MiEmpleadoLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaEmpleados = MiEmpleadoLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaEmpleados;

        }



        private void CargarListaPuestos()
        {
            Logica.Models.Puesto_Empleado MiPuesto = new Logica.Models.Puesto_Empleado();

            DataTable dt = new DataTable();
            dt = MiPuesto.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbPuesto.ValueMember = "ID";
                CbPuesto.DisplayMember = "Descrip";
                CbPuesto.DataSource = dt;
                CbPuesto.SelectedIndex = -1;
            }
        }

       

      

   





        private bool ValidarDatosDigitados(bool OmitirDireccion = false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtNombre_Completo.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCedula.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtTelefono.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtCorreo.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtIDObra.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtDireccion.Text.Trim()) &&
                CbPuesto.SelectedIndex > -1)
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
                        MessageBox.Show("Debe digitar una direccion para el empleado", "Error de validación", MessageBoxButtons.OK);
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
                    MessageBox.Show("Debe digitar un nombre para el empleado", "Error de validación", MessageBoxButtons.OK);
                    TxtNombre_Completo.Focus();
                    return false;
                }

                //cedula
                if (string.IsNullOrEmpty(TxtCedula.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una cédula para el empleado", "Error de validación", MessageBoxButtons.OK);
                    TxtCedula.Focus();
                    return false;
                }

                //telefono
                if (string.IsNullOrEmpty(TxtTelefono.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un teléfono para el empleado", "Error de validación", MessageBoxButtons.OK);
                    TxtTelefono.Focus();
                    return false;
                }

                //correo
                if (string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un correo para el empleado", "Error de validación", MessageBoxButtons.OK);
                    TxtCorreo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtIDObra.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una obra para el empleado", "Error de validación", MessageBoxButtons.OK);
                    TxtIDObra.Focus();
                    return false;
                }


                //rol de usuario
                if (CbPuesto.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un puesto para el empleado", "Error de validación", MessageBoxButtons.OK);
                    CbPuesto.Focus();
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

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
        }

        private void TxtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

        private void TxtUsuarioIDObra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
        }

        private void TxtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);
        }

        private void TxtCorreo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
            {
                if (Validaciones.ValidarEmail(TxtCorreo.Text.Trim()))
                {
                    MessageBox.Show("El formato del correo electronico es incorrecto", "Error de Validacion", MessageBoxButtons.OK);
                    TxtCorreo.Focus();
                }
            }
        }

        private void TxtUsuarioCorreo_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCorreo.Text.Trim()))
            {
                TxtCorreo.SelectAll();
            }
        }

   

        

    

        private void FrmEmpleados_Load_1(object sender, EventArgs e)
        {
            //definimos el padre mdi 
            MdiParent = Globales.MiFormPrincipal;

            CargarListaPuestos();

            CargarListaDeEmpleados();

            ActivarAgregar();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DgLista_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
                int IdEmpleado = Convert.ToInt32(MiFila.Cells["CID_Empleado"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiEmpleadoLocal = new Logica.Models.Empleados();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiEmpleadoLocal.IDEmpleado = IdEmpleado;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiEmpleadoLocal = MiEmpleadoLocal.ConsultarPorIDRetornaEmpleado();

                //validamos que el usuario local tenga datos 

                if (MiEmpleadoLocal != null && MiEmpleadoLocal.IDEmpleado > 0)
                {
                    //si cargamos correctamente el usuario local llenamos los controles 


                    Txt_IDEmpleado.Text = Convert.ToString(MiEmpleadoLocal.IDEmpleado);

                    TxtNombre_Completo.Text = MiEmpleadoLocal.NombreCompleto;

                    TxtCedula.Text = MiEmpleadoLocal.Cedula;

                    TxtTelefono.Text = MiEmpleadoLocal.Telefono;

                    TxtCorreo.Text = MiEmpleadoLocal.Correo;

                    TxtDireccion.Text = MiEmpleadoLocal.Direccion;

                    TxtIDObra.Text = Convert.ToString(MiEmpleadoLocal.IDObra);

                    //combobox 
                    CbPuesto.SelectedValue = MiEmpleadoLocal.MiPuestoTipo.IDPuestoEmpleado;

                    ActivarEditarEliminar();

                }



            }
        }

        private void CboxVerActivos_CheckedChanged_1(object sender, EventArgs e)
        {
            CargarListaDeEmpleados();

            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "ELIMINAR";
            }
            else
            {
                BtnEliminar.Text = "ACTIVAR";
            }
        }

        private void DgLista_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
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

        private void TxtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            CargarListaDeEmpleados();
        }

        private void BtnAgregar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {

                //estas variables almacenan el resultado de las consultas por correo y cedula

                bool CedulaOK;
                bool EmailOK;

                //pasos 1.1 y 1.2
                MiEmpleadoLocal = new Logica.Models.Empleados();

                //llenar los valores de los atributos con los datos digitados en el form
                MiEmpleadoLocal.NombreCompleto = TxtNombre_Completo.Text.Trim();
                MiEmpleadoLocal.Cedula = TxtCedula.Text.Trim();
                MiEmpleadoLocal.Telefono = TxtTelefono.Text.Trim();
                MiEmpleadoLocal.Correo = TxtCorreo.Text.Trim();
                MiEmpleadoLocal.IDObra = Convert.ToInt32(TxtIDObra.Text.Trim());
                //composición del rol 
                MiEmpleadoLocal.MiPuestoTipo.IDPuestoEmpleado = Convert.ToInt32(CbPuesto.SelectedValue);
                MiEmpleadoLocal.Direccion = TxtDireccion.Text.Trim();

                //Realizar las consultas por email y por cedula 
                //pasos 1.3 y 1.3.6
                CedulaOK = MiEmpleadoLocal.ConsultarPorCedula();

                //pasos 1.4 y 1.4.6 
                EmailOK = MiEmpleadoLocal.ConsultarPorEmail();

                //paso 1.5
                if (CedulaOK == false && EmailOK == false)
                {
                    //se puede agregar el usuario ya que no existe un usuario con la cedula y correo
                    //digitados. 

                    //se solicita al usuario confirmación de si queire agregar o no al usuario 

                    string msg = string.Format("¿Está seguro que desea agregar al empleado {0}?", MiEmpleadoLocal.NombreCompleto);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MiEmpleadoLocal.Agregar();

                        if (ok)
                        {
                            MessageBox.Show("Empleado guardado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeEmpleados();

                        }
                        else
                        {
                            MessageBox.Show("El Empleado no se pudo guardar!", ":/", MessageBoxButtons.OK);
                        }



                    }


                }
                else
                {
                    //indicar al usuari si falla alguna consulta

                    if (CedulaOK)
                    {
                        MessageBox.Show("Ya existe un empleado con la cédula digitada", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (EmailOK)
                    {
                        MessageBox.Show("Ya existe un empleado con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                }


            }
        }

        private void BtnModificar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados(true))
            {
                //no es necesario capturar el ID desde el cuadro de texto ya que al consultarlo (cuando seleccionamos el usuario
                //del datagrid), ya tenemos datos en el ID
                //Este ID NO PUEDE SER MODIFICADO, los demás atributos si. 

                MiEmpleadoLocal.NombreCompleto = TxtNombre_Completo.Text.Trim();
                MiEmpleadoLocal.Cedula = TxtCedula.Text.Trim();
                MiEmpleadoLocal.Telefono = TxtTelefono.Text.Trim();
                MiEmpleadoLocal.Correo = TxtCorreo.Text.Trim();
                MiEmpleadoLocal.IDObra = Convert.ToInt32(TxtIDObra.Text.Trim());

                MiEmpleadoLocal.MiPuestoTipo.IDPuestoEmpleado = Convert.ToInt32(CbPuesto.SelectedValue);

                MiEmpleadoLocal.Direccion = TxtDireccion.Text.Trim();

                //según el diagrama de casos de uso expandido y secuencia normal para un CRUD (editar)
                //es habitual consultar por el ID el item que se va a modificar, para asegurarse que 
                //en el lapso de tiempo entre que se seleccionó el usuario y se modificaron los datos en pantalla
                //aún exista el registro en la base de datos. (existe una posibilidad de que ya no exista debido a que
                //en entornos donde hayan muchos usuario trabajando en el sistema algún otro esté modificando el mismo registro) 
                //esto se llama concurrencia. 

                if (MiEmpleadoLocal.ConsultarPorID())
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de modificar el empleado?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (MiEmpleadoLocal.Editar())
                        {
                            MessageBox.Show("El Empleado ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();
                            CargarListaDeEmpleados();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click_1(object sender, EventArgs e)
        {
            if (MiEmpleadoLocal.IDEmpleado > 0 && MiEmpleadoLocal.ConsultarPorID())
            {
                //tomando en cuenta que puedo estar viendo los usuario activos o inactivos
                //este botón podría servir tanto para activar como para desactivar los usuarios
                //El checlbox de la parte superior del forma me sirve para identificar esta 
                //acción

                if (CboxVerActivos.Checked)
                {
                    //DESACTIVAR USUARIO
                    DialogResult r = MessageBox.Show("¿Está seguro de Eliminar al Empleado?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiEmpleadoLocal.Eliminar())
                        {
                            MessageBox.Show("El empleado ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeEmpleados();
                        }

                    }

                }
                else
                {
                    //DESACTIVAR USUARIO
                    DialogResult r = MessageBox.Show("¿Está seguro de Activar al Empleado?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiEmpleadoLocal.Activar())
                        {
                            MessageBox.Show("El empleado ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeEmpleados();
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
            Txt_IDEmpleado.Clear();
            TxtNombre_Completo.Clear();
            TxtCedula.Clear();
            TxtTelefono.Clear();
            TxtCorreo.Clear();
            TxtIDObra.Clear();

            CbPuesto.SelectedIndex = -1;

            TxtDireccion.Clear();

        }
    }
}