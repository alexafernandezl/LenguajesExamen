using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FrmPrincipal : Form
    {
        private readonly Conexion _conexion;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pb_Box_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quieres cerrar el sistema?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.None)
                == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }
        private void CargarDatos()
        {


        }
        private void btn_Empleados_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarAddFrame(new FrmEmpleados()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pueden eliminar Empleados asociados a un proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recursos_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarAddFrame(new FrmRecursos());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                MostrarAddFrame(new FrmProyectos());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarAddFrame(new FrmTareas());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public static void MostrarAddFrame(Form frm)
        {
            try
            {
                using (frm)
                {
                    frm.ShowDialog();
                }
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
