﻿namespace App
{
    partial class FormAddProyectos
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
            this.cbResponsable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Descripcion = new System.Windows.Forms.RichTextBox();
            this.txt_Presupuesto = new System.Windows.Forms.TextBox();
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
            this.txt_NombreProyecto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.cbResponsable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_Descripcion);
            this.panel1.Controls.Add(this.txt_Presupuesto);
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
            this.panel1.Controls.Add(this.txt_NombreProyecto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 546);
            this.panel1.TabIndex = 4;
            // 
            // cbResponsable
            // 
            this.cbResponsable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResponsable.FormattingEnabled = true;
            this.cbResponsable.Location = new System.Drawing.Point(164, 159);
            this.cbResponsable.Name = "cbResponsable";
            this.cbResponsable.Size = new System.Drawing.Size(163, 21);
            this.cbResponsable.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(6, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Responsable:";
            // 
            // txt_Descripcion
            // 
            this.txt_Descripcion.Location = new System.Drawing.Point(13, 223);
            this.txt_Descripcion.Name = "txt_Descripcion";
            this.txt_Descripcion.Size = new System.Drawing.Size(314, 202);
            this.txt_Descripcion.TabIndex = 20;
            this.txt_Descripcion.Text = "";
            // 
            // txt_Presupuesto
            // 
            this.txt_Presupuesto.Location = new System.Drawing.Point(164, 108);
            this.txt_Presupuesto.Name = "txt_Presupuesto";
            this.txt_Presupuesto.Size = new System.Drawing.Size(163, 20);
            this.txt_Presupuesto.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Presupuesto:";
            // 
            // cb_Estado
            // 
            this.cb_Estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Estado.FormattingEnabled = true;
            this.cb_Estado.Items.AddRange(new object[] {
            "Pendiente",
            "Aprobado",
            "En Progreso",
            "Completado",
            "Cancelado"});
            this.cb_Estado.Location = new System.Drawing.Point(164, 57);
            this.cb_Estado.Name = "cb_Estado";
            this.cb_Estado.Size = new System.Drawing.Size(163, 21);
            this.cb_Estado.TabIndex = 17;
            this.cb_Estado.SelectedIndexChanged += new System.EventHandler(this.cb_Estado_SelectedIndexChanged);
            // 
            // cal_finEstimada
            // 
            this.cal_finEstimada.Location = new System.Drawing.Point(348, 229);
            this.cal_finEstimada.Name = "cal_finEstimada";
            this.cal_finEstimada.ShowToday = false;
            this.cal_finEstimada.ShowTodayCircle = false;
            this.cal_finEstimada.TabIndex = 16;
            // 
            // finLabel
            // 
            this.finLabel.AutoSize = true;
            this.finLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.finLabel.Location = new System.Drawing.Point(345, 204);
            this.finLabel.Name = "finLabel";
            this.finLabel.Size = new System.Drawing.Size(164, 16);
            this.finLabel.TabIndex = 15;
            this.finLabel.Text = "Fecha de fin estimada:";
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(500, 403);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancelar.TabIndex = 14;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.Location = new System.Drawing.Point(414, 403);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_Guardar.TabIndex = 13;
            this.btn_Guardar.Text = "Guardar";
            this.btn_Guardar.UseVisualStyleBackColor = true;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // cal_Inicio
            // 
            this.cal_Inicio.Location = new System.Drawing.Point(348, 33);
            this.cal_Inicio.Name = "cal_Inicio";
            this.cal_Inicio.ShowToday = false;
            this.cal_Inicio.ShowTodayCircle = false;
            this.cal_Inicio.TabIndex = 11;
            // 
            // label_telefono
            // 
            this.label_telefono.AutoSize = true;
            this.label_telefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_telefono.ForeColor = System.Drawing.SystemColors.Control;
            this.label_telefono.Location = new System.Drawing.Point(10, 204);
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
            this.label_correo.Location = new System.Drawing.Point(3, 57);
            this.label_correo.Name = "label_correo";
            this.label_correo.Size = new System.Drawing.Size(151, 16);
            this.label_correo.TabIndex = 5;
            this.label_correo.Text = "Estado del proyecto:";
            // 
            // incioLabel
            // 
            this.incioLabel.AutoSize = true;
            this.incioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incioLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.incioLabel.Location = new System.Drawing.Point(345, 8);
            this.incioLabel.Name = "incioLabel";
            this.incioLabel.Size = new System.Drawing.Size(117, 16);
            this.incioLabel.TabIndex = 3;
            this.incioLabel.Text = "Fecha de inicio:";
            // 
            // txt_NombreProyecto
            // 
            this.txt_NombreProyecto.Location = new System.Drawing.Point(164, 11);
            this.txt_NombreProyecto.Name = "txt_NombreProyecto";
            this.txt_NombreProyecto.Size = new System.Drawing.Size(163, 20);
            this.txt_NombreProyecto.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre del proyecto:";
            // 
            // FormAddProyectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(637, 449);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAddProyectos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAddProyectos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.TextBox txt_NombreProyecto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Presupuesto;
        private System.Windows.Forms.ComboBox cbResponsable;
    }
}