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
    public partial class FrmProyectos: Form
    {
        private readonly Conexion _conexion;
        public FrmProyectos()
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            CargarDatos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CargarDatos()
        {
            try
            {
                this.dataGridView1.DataSource = _conexion.CargarProyectos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void pb_CerrarProyectos_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    _conexion.EliminarProyecto(int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdProyecto"].Value.ToString()));
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
                FrmPrincipal.MostrarAddFrame(new FormAddProyectos(0));
        }
       

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdProyecto"].Value.ToString());
                    FrmPrincipal.MostrarAddFrame(new FormAddProyectos(id));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
