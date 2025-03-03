namespace App
{
    partial class FrmAddRecursos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_costoUnidad = new System.Windows.Forms.TextBox();
            this.label_recurso = new System.Windows.Forms.Label();
            this.txt_cantidadDisponible = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_nombreRecurso = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_EstadoRecurso = new System.Windows.Forms.ComboBox();
            this.txt_Descripcion = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_tipoRecurso = new System.Windows.Forms.ComboBox();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.cal_fechaAdquisicion = new System.Windows.Forms.MonthCalendar();
            this.label_telefono = new System.Windows.Forms.Label();
            this.incioLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.txt_costoUnidad);
            this.panel1.Controls.Add(this.label_recurso);
            this.panel1.Controls.Add(this.txt_cantidadDisponible);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_nombreRecurso);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_EstadoRecurso);
            this.panel1.Controls.Add(this.txt_Descripcion);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_tipoRecurso);
            this.panel1.Controls.Add(this.btn_Cancelar);
            this.panel1.Controls.Add(this.btn_Guardar);
            this.panel1.Controls.Add(this.cal_fechaAdquisicion);
            this.panel1.Controls.Add(this.label_telefono);
            this.panel1.Controls.Add(this.incioLabel);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 560);
            this.panel1.TabIndex = 7;
            // 
            // txt_costoUnidad
            // 
            this.txt_costoUnidad.Location = new System.Drawing.Point(169, 135);
            this.txt_costoUnidad.Name = "txt_costoUnidad";
            this.txt_costoUnidad.Size = new System.Drawing.Size(163, 20);
            this.txt_costoUnidad.TabIndex = 29;
            this.txt_costoUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_costoUnidad_KeyPress);
            // 
            // label_recurso
            // 
            this.label_recurso.AutoSize = true;
            this.label_recurso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_recurso.ForeColor = System.Drawing.SystemColors.Control;
            this.label_recurso.Location = new System.Drawing.Point(12, 135);
            this.label_recurso.Name = "label_recurso";
            this.label_recurso.Size = new System.Drawing.Size(129, 16);
            this.label_recurso.TabIndex = 28;
            this.label_recurso.Text = "Costo por unidad:";
            // 
            // txt_cantidadDisponible
            // 
            this.txt_cantidadDisponible.Location = new System.Drawing.Point(169, 76);
            this.txt_cantidadDisponible.Name = "txt_cantidadDisponible";
            this.txt_cantidadDisponible.Size = new System.Drawing.Size(163, 20);
            this.txt_cantidadDisponible.TabIndex = 27;
            this.txt_cantidadDisponible.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cantidadDisponible_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Cantidad disponible:";
            // 
            // txt_nombreRecurso
            // 
            this.txt_nombreRecurso.Location = new System.Drawing.Point(169, 16);
            this.txt_nombreRecurso.Name = "txt_nombreRecurso";
            this.txt_nombreRecurso.Size = new System.Drawing.Size(163, 20);
            this.txt_nombreRecurso.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Nombre del recurso:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Estado del recurso:";
            // 
            // cb_EstadoRecurso
            // 
            this.cb_EstadoRecurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_EstadoRecurso.FormattingEnabled = true;
            this.cb_EstadoRecurso.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cb_EstadoRecurso.Location = new System.Drawing.Point(169, 240);
            this.cb_EstadoRecurso.Name = "cb_EstadoRecurso";
            this.cb_EstadoRecurso.Size = new System.Drawing.Size(163, 21);
            this.cb_EstadoRecurso.TabIndex = 21;
            // 
            // txt_Descripcion
            // 
            this.txt_Descripcion.Location = new System.Drawing.Point(346, 264);
            this.txt_Descripcion.Name = "txt_Descripcion";
            this.txt_Descripcion.Size = new System.Drawing.Size(227, 185);
            this.txt_Descripcion.TabIndex = 20;
            this.txt_Descripcion.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tipo de recurso:";
            // 
            // cb_tipoRecurso
            // 
            this.cb_tipoRecurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tipoRecurso.FormattingEnabled = true;
            this.cb_tipoRecurso.Items.AddRange(new object[] {
            "Material",
            "Humano",
            "Equipamiento"});
            this.cb_tipoRecurso.Location = new System.Drawing.Point(169, 185);
            this.cb_tipoRecurso.Name = "cb_tipoRecurso";
            this.cb_tipoRecurso.Size = new System.Drawing.Size(163, 21);
            this.cb_tipoRecurso.TabIndex = 17;
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(169, 426);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancelar.TabIndex = 14;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.Location = new System.Drawing.Point(43, 426);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_Guardar.TabIndex = 13;
            this.btn_Guardar.Text = "Guardar";
            this.btn_Guardar.UseVisualStyleBackColor = true;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // cal_fechaAdquisicion
            // 
            this.cal_fechaAdquisicion.Location = new System.Drawing.Point(346, 59);
            this.cal_fechaAdquisicion.Name = "cal_fechaAdquisicion";
            this.cal_fechaAdquisicion.TabIndex = 11;
            this.cal_fechaAdquisicion.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.cal_fechaAdquisicion_DateSelected);
            // 
            // label_telefono
            // 
            this.label_telefono.AutoSize = true;
            this.label_telefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_telefono.ForeColor = System.Drawing.SystemColors.Control;
            this.label_telefono.Location = new System.Drawing.Point(343, 245);
            this.label_telefono.Name = "label_telefono";
            this.label_telefono.Size = new System.Drawing.Size(168, 16);
            this.label_telefono.TabIndex = 7;
            this.label_telefono.Text = "Descripción (opcional):";
            // 
            // incioLabel
            // 
            this.incioLabel.AutoSize = true;
            this.incioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incioLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.incioLabel.Location = new System.Drawing.Point(343, 34);
            this.incioLabel.Name = "incioLabel";
            this.incioLabel.Size = new System.Drawing.Size(160, 16);
            this.incioLabel.TabIndex = 3;
            this.incioLabel.Text = "Fecha de adquisición:";
            // 
            // FrmAddRecursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(610, 487);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAddRecursos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddRecursos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_costoUnidad;
        private System.Windows.Forms.Label label_recurso;
        private System.Windows.Forms.TextBox txt_cantidadDisponible;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_nombreRecurso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_EstadoRecurso;
        private System.Windows.Forms.RichTextBox txt_Descripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_tipoRecurso;
        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.MonthCalendar cal_fechaAdquisicion;
        private System.Windows.Forms.Label label_telefono;
        private System.Windows.Forms.Label incioLabel;
    }
}