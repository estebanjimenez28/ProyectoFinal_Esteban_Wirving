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
    public partial class FrmMateriales : Form
    {
        //por orden es mejor crear objetos locales que permitan 
        //la gestión del tema que estamos tratando. 
        //usar objetos individuales en las funcion puede provocar desorden y 
        //complicar más la lectura del código fuente. 

        //objeto local para usuario 
        private Logica.Models.Material MiMaterialLocal { get; set; }

        //lista local de usuarios que se visualizan en el datagridview
        private DataTable ListaMateriales { get; set; }


        public FrmMateriales()
        {
            InitializeComponent();

            MiMaterialLocal = new Logica.Models.Material();
            ListaMateriales = new DataTable();
        }


        private void CargarListaDeMateriales()
        {
            //resetear la lista de usuarios haciendo re instancia del objeto
            ListaMateriales = new DataTable();

            string FiltroBusqueda = "";

            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                //si en el cuadro de texto de busqueda hay 3 o mas caracteres se filtra la lista




                ListaMateriales = MiMaterialLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaMateriales = MiMaterialLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaMateriales;

        }


  
  


        private bool ValidarDatosDigitados(bool OmitirTipoMaterial = false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtCantidad.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtPrecio.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtTipoMaterial.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtIDProveedor.Text.Trim())
                )
            {

                if (OmitirTipoMaterial)
                {
                    R = true;
                }
                else
                {
                    //(PARA AGREGAR) en caso en el que haya que evaluar la contraseña se debe agregar otra condición 
                    //logica
                    if (!string.IsNullOrEmpty(TxtTipoMaterial.Text.Trim()))
                    {
                        R = true;
                    }
                    else
                    {
                        //en caso en el que haga falta la contraseña, se le indica al usuario
                        MessageBox.Show("Debe digitar un tipo para el material", "Error de validación", MessageBoxButtons.OK);
                        TxtTipoMaterial.Focus();
                        return false;

                    }

                }

            }
            else
            {
                //qué pasa cuando algo falta? 
                if (string.IsNullOrEmpty(TxtCantidad.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una cantidad para el material", "Error de validación", MessageBoxButtons.OK);
                    TxtCantidad.Focus();
                    return false;
                }

            
                if (string.IsNullOrEmpty(TxtPrecio.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un precio para el material", "Error de validación", MessageBoxButtons.OK);
                    TxtPrecio.Focus();
                    return false;
                }

                
                if (string.IsNullOrEmpty(TxtTipoMaterial.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un tipo de material", "Error de validación", MessageBoxButtons.OK);
                    TxtTipoMaterial.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtIDProveedor.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el ID del Proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtTipoMaterial.Focus();
                    return false;
                }



            }

            return R;
        }



     

        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);

        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void TxtTipoMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
        }


      

    


        private void BtnAgregar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {



                bool CodigoOK;
                bool TipoMaterialOK;

                //pasos 1.1 y 1.2
                MiMaterialLocal = new Logica.Models.Material();



                MiMaterialLocal.Cantidad = Convert.ToInt32(TxtCantidad.Text.Trim());
                MiMaterialLocal.Precio = Convert.ToInt32(TxtPrecio.Text.Trim());
                MiMaterialLocal.TipoMaterial = TxtTipoMaterial.Text.Trim();
                MiMaterialLocal.IDProveedor = Convert.ToInt32(TxtIDProveedor.Text.Trim());



                //Realizar las consultas por email y por cedula 
                //pasos 1.3 y 1.3.6
                CodigoOK = MiMaterialLocal.ConsultarPorCodigo();

                //pasos 1.4 y 1.4.6 
                TipoMaterialOK = MiMaterialLocal.ConsultarPorTipoMaterial();

                //paso 1.5
                if (CodigoOK == false && TipoMaterialOK == false)
                {
                    //se puede agregar el usuario ya que no existe un usuario con la cedula y correo
                    //digitados. 

                    //se solicita al usuario confirmación de si queire agregar o no al usuario 

                    string msg = string.Format("¿Está seguro que desea agregar al material {0}?", MiMaterialLocal.TipoMaterial);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MiMaterialLocal.Agregar();

                        if (ok)
                        {
                            MessageBox.Show("Material guardado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeMateriales();

                        }
                        else
                        {
                            MessageBox.Show("El Material no se pudo guardar!", ":/", MessageBoxButtons.OK);
                        }



                    }


                }
                else
                {
                    //indicar al material si falla alguna consulta

                    if (CodigoOK)
                    {
                        MessageBox.Show("Ya existe un material con el codigo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (TipoMaterialOK)
                    {
                        MessageBox.Show("Ya existe un material con el tipo digitado", "Error de Validación", MessageBoxButtons.OK);
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



                MiMaterialLocal.Cantidad = Convert.ToInt32(TxtCantidad.Text.Trim());
                MiMaterialLocal.Precio = Convert.ToInt32(TxtPrecio.Text.Trim());
                MiMaterialLocal.TipoMaterial = TxtTipoMaterial.Text.Trim();
                MiMaterialLocal.IDProveedor = Convert.ToInt32(TxtIDProveedor.Text.Trim());




                //según el diagrama de casos de uso expandido y secuencia normal para un CRUD (editar)
                //es habitual consultar por el ID el item que se va a modificar, para asegurarse que 
                //en el lapso de tiempo entre que se seleccionó el usuario y se modificaron los datos en pantalla
                //aún exista el registro en la base de datos. (existe una posibilidad de que ya no exista debido a que
                //en entornos donde hayan muchos usuario trabajando en el sistema algún otro esté modificando el mismo registro) 
                //esto se llama concurrencia. 

                if (MiMaterialLocal.ConsultarPorCodigo())
                {
                    DialogResult respuesta = MessageBox.Show("¿Está seguro de modificar el material?", "???",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (MiMaterialLocal.Editar())
                        {
                            MessageBox.Show("El Material ha sido modificado correctamente", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();
                            CargarListaDeMateriales();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click_1(object sender, EventArgs e)
        {

            if (MiMaterialLocal.CodigoMaterial > 0 && MiMaterialLocal.ConsultarPorCodigo())
            {
                //tomando en cuenta que puedo estar viendo los usuario activos o inactivos
                //este botón podría servir tanto para activar como para desactivar los usuarios
                //El checlbox de la parte superior del forma me sirve para identificar esta 
                //acción

                if (CboxVerActivos.Checked)
                {
                    //DESACTIVAR USUARIO
                    DialogResult r = MessageBox.Show("¿Está seguro de Eliminar el material?",
                                                     "???",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiMaterialLocal.Eliminar())
                        {
                            MessageBox.Show("El material ha sido eliminado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeMateriales();
                        }

                    }

                }
                else
                {
                    DialogResult r = MessageBox.Show("¿Está seguro de activar el material?",
                                                    "???",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        if (MiMaterialLocal.Activar())
                        {
                            MessageBox.Show("El material ha sido activado correctamente.", "!!!", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            CargarListaDeMateriales();
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
            Txt_CodigoMaterial.Clear();

            TxtCantidad.Clear();
            TxtPrecio.Clear();
            TxtTipoMaterial.Clear();
            TxtIDProveedor.Clear();



        }

      

        private void TxtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            CargarListaDeMateriales();
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

        private void FrmMateriales_Load_1(object sender, EventArgs e)
        {
            //definimos el padre mdi 
            MdiParent = Globales.MiFormPrincipal;

            CargarListaDeMateriales();

            ActivarAgregar();
        }

        private void CboxVerActivos_CheckedChanged_1(object sender, EventArgs e)
        {
            CargarListaDeMateriales();

            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "ELIMINAR";
            }
            else
            {
                BtnEliminar.Text = "ACTIVAR";
            }
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
                int CodigoMaterial = Convert.ToInt32(MiFila.Cells["CCodigo_Material"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiMaterialLocal = new Logica.Models.Material();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiMaterialLocal.CodigoMaterial = CodigoMaterial;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiMaterialLocal = MiMaterialLocal.ConsultarPorCodigoRetornaMaterial();

                //validamos que el usuario local tenga datos 

                if (MiMaterialLocal != null && MiMaterialLocal.CodigoMaterial > 0)
                {
                    //si cargamos correctamente el material local llenamos los controles 

                    Txt_CodigoMaterial.Text = Convert.ToString(MiMaterialLocal.CodigoMaterial);

                    TxtCantidad.Text = Convert.ToString(MiMaterialLocal.Cantidad);
                    TxtPrecio.Text = Convert.ToString(MiMaterialLocal.Precio);
                    TxtTipoMaterial.Text = MiMaterialLocal.TipoMaterial;
                    TxtIDProveedor.Text = Convert.ToString(MiMaterialLocal.IDProveedor);


                    ActivarEditarEliminar();

                }



            }
        }
    }
    }

