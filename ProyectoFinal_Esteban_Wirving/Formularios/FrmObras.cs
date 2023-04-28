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
    public partial class FrmObras : Form
    {
        //por orden es mejor crear objetos locales que permitan 
        //la gestión del tema que estamos tratando. 
        //usar objetos individuales en las funcion puede provocar desorden y 
        //complicar más la lectura del código fuente. 

        //objeto local para usuario 
        private Logica.Models.Obra MiObraLocal { get; set; }

        //lista local de usuarios que se visualizan en el datagridview
        private DataTable ListaObras { get; set; }


        public FrmObras()
        {
            InitializeComponent();

            MiObraLocal = new Logica.Models.Obra();
            ListaObras = new DataTable();
        }


        private void CargarListaDeObras()
        {
            //resetear la lista de usuarios haciendo re instancia del objeto
            ListaObras= new DataTable();

            string FiltroBusqueda = "";

            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                //si en el cuadro de texto de busqueda hay 3 o mas caracteres se filtra la lista




                ListaObras = MiObraLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaObras = MiObraLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaObras;

        }

        private void CargarListaObraClientes()
        {
            Logica.Models.Clientes MiCliente = new Logica.Models.Clientes();

            DataTable dt = new DataTable();
            dt = MiCliente.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbNombreCliente.ValueMember = "ID";
                CbNombreCliente.DisplayMember = "Nombre";
                CbNombreCliente.DataSource = dt;
                CbNombreCliente.SelectedIndex = -1;
            }
        }






        private bool ValidarDatosDigitados(bool OmitirEstado = false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtFecha_Inicio.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtFecha_Final.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtEstado.Text.Trim()) &&
                CbNombreCliente.SelectedIndex >-1)
            {

                if (OmitirEstado)
                {
                    R = true;
                }
                else
                {
                    //(PARA AGREGAR) en caso en el que haya que evaluar la contraseña se debe agregar otra condición 
                    //logica
                    if (!string.IsNullOrEmpty(TxtEstado.Text.Trim()))
                    {
                        R = true;
                    }
                    else
                    {
                    
                        MessageBox.Show("Debe digitar un estado para la obra", "Error de validación", MessageBoxButtons.OK);
                        TxtEstado.Focus();
                        return false;

                    }

                }

            }
            else
            {
                //qué pasa cuando algo falta? 
                if (string.IsNullOrEmpty(TxtFecha_Inicio.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una fecha de inicio para la obra", "Error de validación", MessageBoxButtons.OK);
                    TxtFecha_Inicio.Focus();
                    return false;
                }


                if (string.IsNullOrEmpty(TxtFecha_Final.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una fecha de entrega para la obra", "Error de validación", MessageBoxButtons.OK);
                    TxtFecha_Final.Focus();
                    return false;
                }


                if (string.IsNullOrEmpty(TxtEstado.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un estado para la obra", "Error de validación", MessageBoxButtons.OK);
                    TxtEstado.Focus();
                    return false;
                }

                if (CbNombreCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un cliente para la obra", "Error de validación", MessageBoxButtons.OK);
                    CbPuestoCliente.Focus();
                    return false;
                }



            }

            return R;
        }





        private void TxtFecha_Inicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);

        }

        private void TxtFecha_Final_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void TxtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
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



    
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {



                bool IdOK;
                bool EstadoOK;

                //pasos 1.1 y 1.2
                MiObraLocal = new Logica.Models.Obra();



                MiObraLocal.FechaInicio = Convert.ToDateTime(TxtFecha_Inicio.Text.Trim());
                MiObraLocal.FechaFinal = Convert.ToDateTime(TxtFecha_Final.Text.Trim());
                MiObraLocal.Estado = TxtEstado.Text.Trim();
                MiObraLocal.MiClienteTipo.IDCliente = Convert.ToInt32(CbNombreCliente.SelectedValue);
                


                //Realizar las consultas por email y por cedula 
                //pasos 1.3 y 1.3.6
                IdOK = MiObraLocal.ConsultarPorID();

                //pasos 1.4 y 1.4.6 
                EstadoOK = MiObraLocal.ConsultarPorFecha();

                //paso 1.5
                if (IdOK == false && EstadoOK == false)
                {


                    string msg = string.Format("¿Está seguro que desea agregar la obra {0}?", MiObraLocal.Estado);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MiObraLocal.Agregar();

                        if (ok)
                        {
                            MessageBox.Show("Obra guardada correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeObras();

                        }
                        else
                        {
                            MessageBox.Show("La Obra no se pudo guardar!", ":/", MessageBoxButtons.OK);
                        }



                    }


                }
                else
                {
                    //indicar al material si falla alguna consulta

                    if (IdOK)
                    {
                        MessageBox.Show("Ya existe una obra con el id digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (EstadoOK)
                    {
                        MessageBox.Show("Ya existe una obra con el estado digitado", "Error de Validación", MessageBoxButtons.OK);
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



                MiObraLocal.FechaInicio = Convert.ToDateTime(TxtFecha_Inicio.Text.Trim());
                MiObraLocal.FechaFinal = Convert.ToDateTime(TxtFecha_Final.Text.Trim());
                MiObraLocal.Estado = TxtEstado.Text.Trim();
                MiObraLocal.MiClienteTipo.IDCliente = Convert.ToInt32(CbNombreCliente.SelectedValue);





                //según el diagrama de casos de uso expandido y secuencia normal para un CRUD (editar)
                //es habitual consultar por el ID el item que se va a modificar, para asegurarse que 
                //en el lapso de tiempo entre que se seleccionó el usuario y se modificaron los datos en pantalla
                //aún exista el registro en la base de datos. (existe una posibilidad de que ya no exista debido a que
                //en entornos donde hayan muchos usuario trabajando en el sistema algún otro esté modificando el mismo registro) 
                //esto se llama concurrencia. 

                if (MiObraLocal.ConsultarPorID())
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de modificar la Obra?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (MiObraLocal.Editar())
                        {
                            MessageBox.Show("La Obra ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();
                            CargarListaDeObras();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiObraLocal.IDObra > 0 && MiObraLocal.ConsultarPorID())
            {
                //tomando en cuenta que puedo estar viendo los usuario activos o inactivos
                //este botón podría servir tanto para activar como para desactivar los usuarios
                //El checlbox de la parte superior del forma me sirve para identificar esta 
                //acción

                if (CboxVerActivos.Checked)
                {
                    //DESACTIVAR USUARIO
                    DialogResult r = MessageBox.Show("¿Está seguro de Eliminar la Obra?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiObraLocal.Eliminar())
                        {
                            MessageBox.Show("La Obra ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeObras();
                        }

                    }

                }
                else
                {
                    DialogResult r = MessageBox.Show("¿Está seguro de Activar la Obra?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiObraLocal.Activar())
                        {
                            MessageBox.Show("La Obra ha sido activada correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeObras();
                        }

                    }

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
            TxtID_Obra.Clear();

            TxtFecha_Inicio.Clear();
            TxtFecha_Final.Clear();
            TxtEstado.Clear();
            CbNombreCliente.SelectedIndex = -1;




        }

        private void DgLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                int IDObra = Convert.ToInt32(MiFila.Cells["CID_Obra"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiObraLocal = new Logica.Models.Obra();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiObraLocal.IDObra = IDObra;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiObraLocal = MiObraLocal.ConsultarPorIDRetornaObra();

                //validamos que el usuario local tenga datos 

                if (MiObraLocal != null && MiObraLocal.IDObra > 0)
                {
                    //si cargamos correctamente el material local llenamos los controles 

                    TxtID_Obra.Text = Convert.ToString(MiObraLocal.IDObra);

                    TxtFecha_Inicio.Text = Convert.ToString(MiObraLocal.FechaInicio);
                    TxtFecha_Final.Text = Convert.ToString(MiObraLocal.FechaFinal);
                    TxtEstado.Text = MiObraLocal.Estado;
                    //combobox 
                    CbNombreCliente.SelectedValue = MiObraLocal.MiClienteTipo.IDCliente;

                    ActivarEditarEliminar();

                }



            }
        }

        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarListaDeObras();

            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "ELIMINAR";
            }
            else
            {
                BtnEliminar.Text = "ACTIVAR";
            }
        }

        private void FrmObras_Load(object sender, EventArgs e)
        {
            //definimos el padre mdi 
            MdiParent = Globales.MiFormPrincipal;

            CargarListaObraClientes();

            CargarListaDeObras();

            ActivarAgregar();
        }

        private void DgLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgLista.ClearSelection();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarListaDeObras();
        }
    }
}

