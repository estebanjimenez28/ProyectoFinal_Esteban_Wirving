using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_Esteban_Wirving
{
    public static class Globales
    {
        public static Form MiFormPrincipal = new Formularios.FrmMDI();

        public static Logica.Models.Usuario MiUsuarioGlobal = new Logica.Models.Usuario();


        public static Formularios.FrmClientes MiFormClientes = new Formularios.FrmClientes();

        public static Formularios.FrmProveedores MiFormProveedores = new Formularios.FrmProveedores();
        public static Formularios.FrmMateriales MiFormMateriales = new Formularios.FrmMateriales();
        public static Formularios.FrmObras MiFormObras = new Formularios.FrmObras();
        public static Formularios.FrmEmpleados MiFormEmpleados = new Formularios.FrmEmpleados();
        public static Formularios.FrmUsuarios MiFormUsuarios = new Formularios.FrmUsuarios();
        public static Formularios.FrmRegistroDetalle MiFormRegistroMaterial = new Formularios.FrmRegistroDetalle();
    }
}
