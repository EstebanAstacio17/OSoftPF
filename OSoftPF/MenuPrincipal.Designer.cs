namespace OSoftPF
{
    partial class MenuPrincipal
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPermiso = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.btnCD = new System.Windows.Forms.Button();
            this.btnAdministracion = new System.Windows.Forms.Button();
            this.btnVacio = new System.Windows.Forms.Button();
            this.btnCL = new System.Windows.Forms.Button();
            this.btnCR = new System.Windows.Forms.Button();
            this.btnCO = new System.Windows.Forms.Button();
            this.btnCE = new System.Windows.Forms.Button();
            this.btnControlUsuarios = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTemporada = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblHerramientas = new System.Windows.Forms.Label();
            this.btnMI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblPermiso
            // 
            this.lblPermiso.AutoSize = true;
            this.lblPermiso.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermiso.Location = new System.Drawing.Point(10, 75);
            this.lblPermiso.Name = "lblPermiso";
            this.lblPermiso.Size = new System.Drawing.Size(107, 20);
            this.lblPermiso.TabIndex = 1;
            this.lblPermiso.Text = "Administrador";
            this.lblPermiso.TextChanged += new System.EventHandler(this.labelPermiso_TextChanged);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(110, 5);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(79, 20);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "NOMBRE";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.btnConfiguracion);
            this.groupBox1.Controls.Add(this.btnCD);
            this.groupBox1.Controls.Add(this.btnAdministracion);
            this.groupBox1.Controls.Add(this.btnVacio);
            this.groupBox1.Controls.Add(this.btnCL);
            this.groupBox1.Controls.Add(this.btnCR);
            this.groupBox1.Controls.Add(this.btnCO);
            this.groupBox1.Controls.Add(this.btnCE);
            this.groupBox1.Controls.Add(this.btnControlUsuarios);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnMI);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(-10, -10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 662);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.BackColor = System.Drawing.Color.SteelBlue;
            this.btnConfiguracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracion.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracion.Location = new System.Drawing.Point(50, 569);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(257, 50);
            this.btnConfiguracion.TabIndex = 16;
            this.btnConfiguracion.Text = "Configuracion";
            this.btnConfiguracion.UseVisualStyleBackColor = false;
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // btnCD
            // 
            this.btnCD.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCD.ForeColor = System.Drawing.Color.White;
            this.btnCD.Location = new System.Drawing.Point(50, 260);
            this.btnCD.Name = "btnCD";
            this.btnCD.Size = new System.Drawing.Size(257, 50);
            this.btnCD.TabIndex = 14;
            this.btnCD.Text = "Discipulado";
            this.btnCD.UseVisualStyleBackColor = false;
            this.btnCD.Click += new System.EventHandler(this.btnCD_Click);
            // 
            // btnAdministracion
            // 
            this.btnAdministracion.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdministracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdministracion.ForeColor = System.Drawing.Color.White;
            this.btnAdministracion.Location = new System.Drawing.Point(50, 510);
            this.btnAdministracion.Name = "btnAdministracion";
            this.btnAdministracion.Size = new System.Drawing.Size(257, 50);
            this.btnAdministracion.TabIndex = 9;
            this.btnAdministracion.Text = "Administracion";
            this.btnAdministracion.UseVisualStyleBackColor = false;
            this.btnAdministracion.Click += new System.EventHandler(this.btnAdministracion_Click);
            // 
            // btnVacio
            // 
            this.btnVacio.BackColor = System.Drawing.Color.SteelBlue;
            this.btnVacio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVacio.ForeColor = System.Drawing.Color.White;
            this.btnVacio.Location = new System.Drawing.Point(50, 460);
            this.btnVacio.Name = "btnVacio";
            this.btnVacio.Size = new System.Drawing.Size(257, 50);
            this.btnVacio.TabIndex = 11;
            this.btnVacio.UseVisualStyleBackColor = false;
            // 
            // btnCL
            // 
            this.btnCL.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCL.ForeColor = System.Drawing.Color.White;
            this.btnCL.Location = new System.Drawing.Point(50, 410);
            this.btnCL.Name = "btnCL";
            this.btnCL.Size = new System.Drawing.Size(257, 50);
            this.btnCL.TabIndex = 8;
            this.btnCL.Text = "Logística";
            this.btnCL.UseVisualStyleBackColor = false;
            // 
            // btnCR
            // 
            this.btnCR.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCR.ForeColor = System.Drawing.Color.White;
            this.btnCR.Location = new System.Drawing.Point(50, 360);
            this.btnCR.Name = "btnCR";
            this.btnCR.Size = new System.Drawing.Size(257, 50);
            this.btnCR.TabIndex = 12;
            this.btnCR.Text = "Recursos";
            this.btnCR.UseVisualStyleBackColor = false;
            // 
            // btnCO
            // 
            this.btnCO.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCO.ForeColor = System.Drawing.Color.White;
            this.btnCO.Location = new System.Drawing.Point(50, 310);
            this.btnCO.Name = "btnCO";
            this.btnCO.Size = new System.Drawing.Size(257, 50);
            this.btnCO.TabIndex = 13;
            this.btnCO.Text = "Oración";
            this.btnCO.UseVisualStyleBackColor = false;
            // 
            // btnCE
            // 
            this.btnCE.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCE.ForeColor = System.Drawing.Color.White;
            this.btnCE.Location = new System.Drawing.Point(50, 160);
            this.btnCE.Name = "btnCE";
            this.btnCE.Size = new System.Drawing.Size(257, 50);
            this.btnCE.TabIndex = 15;
            this.btnCE.Text = "Coordinador de Equipo";
            this.btnCE.UseVisualStyleBackColor = false;
            // 
            // btnControlUsuarios
            // 
            this.btnControlUsuarios.BackColor = System.Drawing.Color.SteelBlue;
            this.btnControlUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControlUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnControlUsuarios.Location = new System.Drawing.Point(50, 110);
            this.btnControlUsuarios.Name = "btnControlUsuarios";
            this.btnControlUsuarios.Size = new System.Drawing.Size(257, 50);
            this.btnControlUsuarios.TabIndex = 6;
            this.btnControlUsuarios.Text = "Control De Usuarios";
            this.btnControlUsuarios.UseVisualStyleBackColor = false;
            this.btnControlUsuarios.Click += new System.EventHandler(this.btnControlUsuarios_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.lblTemporada);
            this.groupBox2.Controls.Add(this.lblId);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Controls.Add(this.lblPermiso);
            this.groupBox2.Controls.Add(this.lblApellido);
            this.groupBox2.Controls.Add(this.lblHerramientas);
            this.groupBox2.Controls.Add(this.lblNombre);
            this.groupBox2.Location = new System.Drawing.Point(107, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // lblTemporada
            // 
            this.lblTemporada.AutoSize = true;
            this.lblTemporada.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemporada.Location = new System.Drawing.Point(123, 75);
            this.lblTemporada.Name = "lblTemporada";
            this.lblTemporada.Size = new System.Drawing.Size(39, 20);
            this.lblTemporada.TabIndex = 7;
            this.lblTemporada.Text = "------";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(10, 30);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(26, 20);
            this.lblId.TabIndex = 6;
            this.lblId.Text = "ID";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(67, 30);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(84, 20);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "USUARIO";
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.Location = new System.Drawing.Point(10, 5);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(88, 20);
            this.lblApellido.TabIndex = 4;
            this.lblApellido.Text = "APELLIDO";
            // 
            // lblHerramientas
            // 
            this.lblHerramientas.AutoSize = true;
            this.lblHerramientas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHerramientas.Location = new System.Drawing.Point(10, 55);
            this.lblHerramientas.Name = "lblHerramientas";
            this.lblHerramientas.Size = new System.Drawing.Size(182, 16);
            this.lblHerramientas.TabIndex = 4;
            this.lblHerramientas.Text = "ZONA - EQUIPO - ROL - PAIS";
            // 
            // btnMI
            // 
            this.btnMI.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMI.ForeColor = System.Drawing.Color.White;
            this.btnMI.Location = new System.Drawing.Point(50, 210);
            this.btnMI.Name = "btnMI";
            this.btnMI.Size = new System.Drawing.Size(257, 50);
            this.btnMI.TabIndex = 7;
            this.btnMI.Text = "Movilización de Iglesias";
            this.btnMI.UseVisualStyleBackColor = false;
            this.btnMI.Click += new System.EventHandler(this.btnMI_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(974, 621);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrincipal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPermiso;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblHerramientas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Button btnControlUsuarios;
        private System.Windows.Forms.Button btnCE;
        private System.Windows.Forms.Button btnCD;
        private System.Windows.Forms.Button btnCO;
        private System.Windows.Forms.Button btnCR;
        private System.Windows.Forms.Button btnVacio;
        private System.Windows.Forms.Button btnAdministracion;
        private System.Windows.Forms.Button btnCL;
        private System.Windows.Forms.Button btnMI;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Label lblTemporada;
    }
}