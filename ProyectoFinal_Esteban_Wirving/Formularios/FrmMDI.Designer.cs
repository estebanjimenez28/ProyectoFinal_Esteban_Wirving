namespace ProyectoFinal_Esteban_Wirving.Formularios
{
    partial class FrmMDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.MnuMantenimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoMaterialesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoProveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoEmpleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoObrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuRegistros = new System.Windows.Forms.ToolStripMenuItem();
            this.registroMaterialesCompradosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosPorObraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresPorMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosPorPuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.MnuPrincipal.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MnuPrincipal
            // 
            this.MnuPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MnuPrincipal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MnuPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MnuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuMantenimientos,
            this.MnuRegistros,
            this.MnuReportes,
            this.MnuSalir,
            this.MnuAcercaDe});
            this.MnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.MnuPrincipal.Name = "MnuPrincipal";
            this.MnuPrincipal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MnuPrincipal.Size = new System.Drawing.Size(758, 36);
            this.MnuPrincipal.TabIndex = 1;
            this.MnuPrincipal.Text = "menuStrip1";
            // 
            // MnuMantenimientos
            // 
            this.MnuMantenimientos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mantenimientoClientesToolStripMenuItem,
            this.mantenimientoMaterialesToolStripMenuItem,
            this.mantenimientoProveedoresToolStripMenuItem,
            this.mantenimientoEmpleadosToolStripMenuItem,
            this.mantenimientoObrasToolStripMenuItem,
            this.mantenimientoUsuariosToolStripMenuItem});
            this.MnuMantenimientos.ForeColor = System.Drawing.Color.White;
            this.MnuMantenimientos.Name = "MnuMantenimientos";
            this.MnuMantenimientos.Size = new System.Drawing.Size(195, 32);
            this.MnuMantenimientos.Text = "MANTENIMIENTOS";
            // 
            // mantenimientoClientesToolStripMenuItem
            // 
            this.mantenimientoClientesToolStripMenuItem.Name = "mantenimientoClientesToolStripMenuItem";
            this.mantenimientoClientesToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoClientesToolStripMenuItem.Text = "Mantenimiento Clientes";
            this.mantenimientoClientesToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoClientesToolStripMenuItem_Click);
            // 
            // mantenimientoMaterialesToolStripMenuItem
            // 
            this.mantenimientoMaterialesToolStripMenuItem.Name = "mantenimientoMaterialesToolStripMenuItem";
            this.mantenimientoMaterialesToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoMaterialesToolStripMenuItem.Text = "Mantenimiento Materiales";
            this.mantenimientoMaterialesToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoMaterialesToolStripMenuItem_Click);
            // 
            // mantenimientoProveedoresToolStripMenuItem
            // 
            this.mantenimientoProveedoresToolStripMenuItem.Name = "mantenimientoProveedoresToolStripMenuItem";
            this.mantenimientoProveedoresToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoProveedoresToolStripMenuItem.Text = "Mantenimiento Proveedores";
            this.mantenimientoProveedoresToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoProveedoresToolStripMenuItem_Click);
            // 
            // mantenimientoEmpleadosToolStripMenuItem
            // 
            this.mantenimientoEmpleadosToolStripMenuItem.Name = "mantenimientoEmpleadosToolStripMenuItem";
            this.mantenimientoEmpleadosToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoEmpleadosToolStripMenuItem.Text = "Mantenimiento Empleados";
            this.mantenimientoEmpleadosToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoEmpleadosToolStripMenuItem_Click);
            // 
            // mantenimientoObrasToolStripMenuItem
            // 
            this.mantenimientoObrasToolStripMenuItem.Name = "mantenimientoObrasToolStripMenuItem";
            this.mantenimientoObrasToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoObrasToolStripMenuItem.Text = "Mantenimiento Obras";
            this.mantenimientoObrasToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoObrasToolStripMenuItem_Click);
            // 
            // mantenimientoUsuariosToolStripMenuItem
            // 
            this.mantenimientoUsuariosToolStripMenuItem.Name = "mantenimientoUsuariosToolStripMenuItem";
            this.mantenimientoUsuariosToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.mantenimientoUsuariosToolStripMenuItem.Text = "Mantenimiento Usuarios";
            this.mantenimientoUsuariosToolStripMenuItem.Click += new System.EventHandler(this.mantenimientoUsuariosToolStripMenuItem_Click);
            // 
            // MnuRegistros
            // 
            this.MnuRegistros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registroMaterialesCompradosToolStripMenuItem});
            this.MnuRegistros.ForeColor = System.Drawing.Color.White;
            this.MnuRegistros.Name = "MnuRegistros";
            this.MnuRegistros.Size = new System.Drawing.Size(126, 32);
            this.MnuRegistros.Text = "REGISTROS";
            this.MnuRegistros.Click += new System.EventHandler(this.MnuProcesos_Click);
            // 
            // registroMaterialesCompradosToolStripMenuItem
            // 
            this.registroMaterialesCompradosToolStripMenuItem.Name = "registroMaterialesCompradosToolStripMenuItem";
            this.registroMaterialesCompradosToolStripMenuItem.Size = new System.Drawing.Size(313, 32);
            this.registroMaterialesCompradosToolStripMenuItem.Text = "Registro Detalle Material";
            this.registroMaterialesCompradosToolStripMenuItem.Click += new System.EventHandler(this.registroMaterialesCompradosToolStripMenuItem_Click);
            // 
            // MnuReportes
            // 
            this.MnuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empleadosPorObraToolStripMenuItem,
            this.proveedoresPorMaterialToolStripMenuItem,
            this.empleadosPorPuestoToolStripMenuItem});
            this.MnuReportes.ForeColor = System.Drawing.Color.White;
            this.MnuReportes.Name = "MnuReportes";
            this.MnuReportes.Size = new System.Drawing.Size(116, 32);
            this.MnuReportes.Text = "REPORTES";
            // 
            // empleadosPorObraToolStripMenuItem
            // 
            this.empleadosPorObraToolStripMenuItem.Name = "empleadosPorObraToolStripMenuItem";
            this.empleadosPorObraToolStripMenuItem.Size = new System.Drawing.Size(320, 32);
            this.empleadosPorObraToolStripMenuItem.Text = "Empleados por Obra";
            // 
            // proveedoresPorMaterialToolStripMenuItem
            // 
            this.proveedoresPorMaterialToolStripMenuItem.Name = "proveedoresPorMaterialToolStripMenuItem";
            this.proveedoresPorMaterialToolStripMenuItem.Size = new System.Drawing.Size(320, 32);
            this.proveedoresPorMaterialToolStripMenuItem.Text = "Proveedores por Material";
            // 
            // empleadosPorPuestoToolStripMenuItem
            // 
            this.empleadosPorPuestoToolStripMenuItem.Name = "empleadosPorPuestoToolStripMenuItem";
            this.empleadosPorPuestoToolStripMenuItem.Size = new System.Drawing.Size(320, 32);
            this.empleadosPorPuestoToolStripMenuItem.Text = "Empleados por Puesto";
            // 
            // MnuSalir
            // 
            this.MnuSalir.ForeColor = System.Drawing.Color.White;
            this.MnuSalir.Name = "MnuSalir";
            this.MnuSalir.Size = new System.Drawing.Size(76, 32);
            this.MnuSalir.Text = "SALIR";
            // 
            // MnuAcercaDe
            // 
            this.MnuAcercaDe.ForeColor = System.Drawing.Color.White;
            this.MnuAcercaDe.Name = "MnuAcercaDe";
            this.MnuAcercaDe.Size = new System.Drawing.Size(127, 32);
            this.MnuAcercaDe.Text = "ACERCA DE";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.LblUsuario});
            this.statusStrip1.Location = new System.Drawing.Point(0, 546);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(758, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(74, 20);
            this.toolStripStatusLabel1.Text = "USUARIO:";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // LblUsuario
            // 
            this.LblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.Size = new System.Drawing.Size(61, 20);
            this.LblUsuario.Text = "usuario";
            // 
            // FrmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::ProyectoFinal_Esteban_Wirving.Properties.Resources.r;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(758, 572);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MnuPrincipal);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MnuPrincipal;
            this.Name = "FrmMDI";
            this.Text = "Sistema de Inventario Cajiaco Construcciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMDI_FormClosed);
            this.Load += new System.EventHandler(this.FrmMDI_Load);
            this.MnuPrincipal.ResumeLayout(false);
            this.MnuPrincipal.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem MnuMantenimientos;
        private System.Windows.Forms.ToolStripMenuItem MnuRegistros;
        private System.Windows.Forms.ToolStripMenuItem MnuSalir;
        private System.Windows.Forms.ToolStripMenuItem MnuAcercaDe;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoMaterialesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoProveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoEmpleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoObrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroMaterialesCompradosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuReportes;
        private System.Windows.Forms.ToolStripMenuItem empleadosPorObraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresPorMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosPorPuestoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoUsuariosToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel LblUsuario;
    }
}