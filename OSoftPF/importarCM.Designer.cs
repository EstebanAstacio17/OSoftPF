namespace OSoftPF
{
    partial class ImportarCM
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
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.dgvSeleccionados = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnValidar = new System.Windows.Forms.Button();
            this.btnEvaluar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeleccionados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSeleccionar.Location = new System.Drawing.Point(877, 569);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(75, 30);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // dgvSeleccionados
            // 
            this.dgvSeleccionados.AllowUserToAddRows = false;
            this.dgvSeleccionados.AllowUserToDeleteRows = false;
            this.dgvSeleccionados.BackgroundColor = System.Drawing.Color.White;
            this.dgvSeleccionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeleccionados.Location = new System.Drawing.Point(12, 12);
            this.dgvSeleccionados.Name = "dgvSeleccionados";
            this.dgvSeleccionados.ReadOnly = true;
            this.dgvSeleccionados.Size = new System.Drawing.Size(940, 551);
            this.dgvSeleccionados.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnGuardar.Location = new System.Drawing.Point(796, 569);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 30);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnValidar
            // 
            this.btnValidar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnValidar.Location = new System.Drawing.Point(715, 569);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(75, 30);
            this.btnValidar.TabIndex = 3;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // btnEvaluar
            // 
            this.btnEvaluar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnEvaluar.Location = new System.Drawing.Point(634, 569);
            this.btnEvaluar.Name = "btnEvaluar";
            this.btnEvaluar.Size = new System.Drawing.Size(75, 30);
            this.btnEvaluar.TabIndex = 4;
            this.btnEvaluar.Text = "Evaluar";
            this.btnEvaluar.UseVisualStyleBackColor = true;
            this.btnEvaluar.Click += new System.EventHandler(this.btnEvaluar_Click);
            // 
            // ImportarCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(964, 611);
            this.Controls.Add(this.btnEvaluar);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvSeleccionados);
            this.Controls.Add(this.btnSeleccionar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportarCM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "importarCM";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeleccionados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.DataGridView dgvSeleccionados;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.Button btnEvaluar;
    }
}