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
    public partial class FrmTareas: Form
    {
        private readonly Conexion _conexion;
        public FrmTareas()
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            CargarDatos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void CargarDatos()
        {
            try
            {
                this.dataGridView1.DataSource = _conexion.CargarTareas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void pb_CerrarTareas_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void eliminarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    _conexion.EliminarTarea(int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdTarea"].Value.ToString()));
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
            FrmAddTareas frm = new FrmAddTareas(0, this); // Pasamos la referencia
            frm.ShowDialog(); // Modal respecto a FrmEmpleados
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdTarea"].Value.ToString());
                    FrmAddTareas frm = new FrmAddTareas(id, this);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_buscar.Text) || txt_buscar.Text == "Buscar Tarea...")
                {
                    CargarDatos();
                }
                else
                {
                    DataTable resultados = _conexion.BuscarTareaDescripcion(txt_buscar.Text);

                    if (resultados.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = resultados;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;

                        MessageBox.Show("No se encontraron tareas con ese nombre.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("El formato de búsqueda no es válido. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_buscar_Enter(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "Buscar Tarea...")
            {
                txt_buscar.Text = "";
                txt_buscar.ForeColor = Color.Black;
            }
        }
        private void txt_buscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar.Text))
            {
                txt_buscar.Text = "Buscar Tarea...";
                txt_buscar.ForeColor = Color.Gray;
            }
        }
    }
}
