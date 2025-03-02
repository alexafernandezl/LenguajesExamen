namespace App
{
    partial class FrmAddTareas
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
            this.txt_cantidad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_recurso = new System.Windows.Forms.TextBox();
            this.label_recurso = new System.Windows.Forms.Label();
            this.txt_idResponsable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_idProyecto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_requiere = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Prioridad = new System.Windows.Forms.ComboBox();
            this.txt_Descripcion = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Estado = new System.Windows.Forms.ComboBox();
            this.cal_finEstimada = new System.Windows.Forms.MonthCalendar();
            this.finLabel = new System.Windows.Forms.Label();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.cal_Inicio = new System.Windows.Forms.MonthCalendar();
            this.label_telefono = new System.Windows.Forms.Label();
            this.label_correo = new System.Windows.Forms.Label();
            this.incioLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.txt_cantidad);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_recurso);
            this.panel1.Controls.Add(this.label_recurso);
            this.panel1.Controls.Add(this.txt_idResponsable);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_idProyecto);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cb_requiere);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_Prioridad);
            this.panel1.Controls.Add(this.txt_Descripcion);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_Estado);
            this.panel1.Controls.Add(this.cal_finEstimada);
            this.panel1.Controls.Add(this.finLabel);
            this.panel1.Controls.Add(this.btn_Cancelar);
            this.panel1.Controls.Add(this.btn_Guardar);
            this.panel1.Controls.Add(this.cal_Inicio);
            this.panel1.Controls.Add(this.label_telefono);
            this.panel1.Controls.Add(this.label_correo);
            this.panel1.Controls.Add(this.incioLabel);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 597);
            this.panel1.TabIndex = 5;
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Location = new System.Drawing.Point(169, 133);
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(163, 20);
            this.txt_cantidad.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(12, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Cantidad:";
            // 
            // txt_recurso
            // 
            this.txt_recurso.Location = new System.Drawing.Point(169, 248);
            this.txt_recurso.Name = "txt_recurso";
            this.txt_recurso.Size = new System.Drawing.Size(163, 20);
            this.txt_recurso.TabIndex = 29;
            // 
            // label_recurso
            // 
            this.label_recurso.AutoSize = true;
            this.label_recurso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_recurso.ForeColor = System.Drawing.SystemColors.Control;
            this.label_recurso.Location = new System.Drawing.Point(12, 252);
            this.label_recurso.Name = "label_recurso";
            this.label_recurso.Size = new System.Drawing.Size(86, 16);
            this.label_recurso.TabIndex = 28;
            this.label_recurso.Text = "Id Recurso:";
            // 
            // txt_idResponsable
            // 
            this.txt_idResponsable.Location = new System.Drawing.Point(169, 209);
            this.txt_idResponsable.Name = "txt_idResponsable";
            this.txt_idResponsable.Size = new System.Drawing.Size(163, 20);
            this.txt_idResponsable.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(12, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Id Responsable:";
            // 
            // txt_idProyecto
            // 
            this.txt_idProyecto.Location = new System.Drawing.Point(169, 169);
            this.txt_idProyecto.Name = "txt_idProyecto";
            this.txt_idProyecto.Size = new System.Drawing.Size(163, 20);
            this.txt_idProyecto.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Id Proyecto:";
            // 
            // cb_requiere
            // 
            this.cb_requiere.FormattingEnabled = true;
            this.cb_requiere.Items.AddRange(new object[] {
            "Gerente",
            "Supervisor",
            "Técnico",
            "Administrativo",
            "Cualquier"});
            this.cb_requiere.Location = new System.Drawing.Point(169, 97);
            this.cb_requiere.Name = "cb_requiere";
            this.cb_requiere.Size = new System.Drawing.Size(163, 21);
            this.cb_requiere.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Requiere:";
            // 
            // cb_Prioridad
            // 
            this.cb_Prioridad.FormattingEnabled = true;
            this.cb_Prioridad.Items.AddRange(new object[] {
            "Baja ",
            "Media",
            "Alta"});
            this.cb_Prioridad.Location = new System.Drawing.Point(169, 60);
            this.cb_Prioridad.Name = "cb_Prioridad";
            this.cb_Prioridad.Size = new System.Drawing.Size(163, 21);
            this.cb_Prioridad.TabIndex = 21;
            // 
            // txt_Descripcion
            // 
            this.txt_Descripcion.Location = new System.Drawing.Point(11, 296);
            this.txt_Descripcion.Name = "txt_Descripcion";
            this.txt_Descripcion.Size = new System.Drawing.Size(321, 180);
            this.txt_Descripcion.TabIndex = 20;
            this.txt_Descripcion.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Prioridad:";
            // 
            // cb_Estado
            // 
            this.cb_Estado.FormattingEnabled = true;
            this.cb_Estado.Items.AddRange(new object[] {
            "Pendiente por defecto",
            "Aprobado",
            "En Progreso",
            "Completado",
            "Cancelado"});
            this.cb_Estado.Location = new System.Drawing.Point(169, 24);
            this.cb_Estado.Name = "cb_Estado";
            this.cb_Estado.Size = new System.Drawing.Size(163, 21);
            this.cb_Estado.TabIndex = 17;
            // 
            // cal_finEstimada
            // 
            this.cal_finEstimada.Location = new System.Drawing.Point(344, 245);
            this.cal_finEstimada.Name = "cal_finEstimada";
            this.cal_finEstimada.TabIndex = 16;
            // 
            // finLabel
            // 
            this.finLabel.AutoSize = true;
            this.finLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.finLabel.Location = new System.Drawing.Point(341, 220);
            this.finLabel.Name = "finLabel";
            this.finLabel.Size = new System.Drawing.Size(164, 16);
            this.finLabel.TabIndex = 15;
            this.finLabel.Text = "Fecha de fin estimada:";
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(496, 453);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancelar.TabIndex = 14;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.Location = new System.Drawing.Point(393, 453);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_Guardar.TabIndex = 13;
            this.btn_Guardar.Text = "Guardar";
            this.btn_Guardar.UseVisualStyleBackColor = true;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // cal_Inicio
            // 
            this.cal_Inicio.Location = new System.Drawing.Point(344, 49);
            this.cal_Inicio.Name = "cal_Inicio";
            this.cal_Inicio.TabIndex = 11;
            // 
            // label_telefono
            // 
            this.label_telefono.AutoSize = true;
            this.label_telefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_telefono.ForeColor = System.Drawing.SystemColors.Control;
            this.label_telefono.Location = new System.Drawing.Point(12, 277);
            this.label_telefono.Name = "label_telefono";
            this.label_telefono.Size = new System.Drawing.Size(94, 16);
            this.label_telefono.TabIndex = 7;
            this.label_telefono.Text = "Descripción:";
            // 
            // label_correo
            // 
            this.label_correo.AutoSize = true;
            this.label_correo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_correo.ForeColor = System.Drawing.SystemColors.Control;
            this.label_correo.Location = new System.Drawing.Point(8, 24);
            this.label_correo.Name = "label_correo";
            this.label_correo.Size = new System.Drawing.Size(139, 16);
            this.label_correo.TabIndex = 5;
            this.label_correo.Text = "Estado de la tarea:";
            // 
            // incioLabel
            // 
            this.incioLabel.AutoSize = true;
            this.incioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incioLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.incioLabel.Location = new System.Drawing.Point(341, 23);
            this.incioLabel.Name = "incioLabel";
            this.incioLabel.Size = new System.Drawing.Size(117, 16);
            this.incioLabel.TabIndex = 3;
            this.incioLabel.Text = "Fecha de inicio:";
            // 
            // FrmAddTareas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(593, 503);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAddTareas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddTareas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_recurso;
        private System.Windows.Forms.Label label_recurso;
        private System.Windows.Forms.TextBox txt_idResponsable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_idProyecto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_requiere;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Prioridad;
        private System.Windows.Forms.RichTextBox txt_Descripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Estado;
        private System.Windows.Forms.MonthCalendar cal_finEstimada;
        private System.Windows.Forms.Label finLabel;
        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.MonthCalendar cal_Inicio;
        private System.Windows.Forms.Label label_telefono;
        private System.Windows.Forms.Label label_correo;
        private System.Windows.Forms.Label incioLabel;
        private System.Windows.Forms.TextBox txt_cantidad;
        private System.Windows.Forms.Label label6;
    }
}