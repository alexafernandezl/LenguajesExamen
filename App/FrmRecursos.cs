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
    public partial class FrmRecursos: Form
    {

        private readonly Conexion _conexion;
        public FrmRecursos()
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
                this.dataGridView1.DataSource = _conexion.CargarRecursos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarRecursos_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    _conexion.EliminarRecurso(int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdRecurso"].Value.ToString()));
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
            FrmPrincipal.MostrarAddFrame(new FrmAddRecursos(0));
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdRecurso"].Value.ToString());
                    FrmPrincipal.MostrarAddFrame(new FrmAddRecursos(id));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
