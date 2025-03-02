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
    public partial class FrmEmpleados : Form
    {

        private readonly Conexion _conexion;
        public FrmEmpleados()
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            CargarDatos();
        }

        private void pb_Box_Click(object sender, EventArgs e)
        {
        }

        private void pb_CerrarEmpleados_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btn_Empleados_Click(object sender, EventArgs e)
        {

        }
        public void CargarDatos()
        {
            try
            {
                this.dataGridView1.DataSource = _conexion.CargarEmpleados();

                if (dataGridView1.Columns["FechaNacimiento"].ValueType == typeof(DateTime))
                {
                    dataGridView1.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult resultado = MessageBox.Show(
                        "¿Está seguro que desea eliminar este empleado?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (resultado == DialogResult.Yes)
                    {
                        int idEmpleado = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdEmpleado"].Value.ToString());
                        bool eliminado = _conexion.EliminarEmpleado(idEmpleado);

                        if (eliminado)
                        {
                            MessageBox.Show("Empleado eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            FrmAddEmpleado frm = new FrmAddEmpleado(0, this); // Pasamos la referencia
            frm.ShowDialog(); // Modal respecto a FrmEmpleados
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdEmpleado"].Value.ToString());
                    FrmAddEmpleado frm = new FrmAddEmpleado(id, this);
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
                if (string.IsNullOrWhiteSpace(txt_buscar.Text) || txt_buscar.Text == "Buscar empleado...")
                {
                    CargarDatos();
                }
                else
                {
                    DataTable resultados = _conexion.BuscarEmpleadoNombre(txt_buscar.Text);

                    if (resultados.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = resultados;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;

                        MessageBox.Show("No se encontraron empleados con ese nombre.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txt_buscar.Text == "Buscar empleado...")
            {
                txt_buscar.Text = "";
                txt_buscar.ForeColor = Color.Black;
            }
        }
        private void txt_buscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar.Text))
            {
                txt_buscar.Text = "Buscar empleado...";
                txt_buscar.ForeColor = Color.Gray;
            }
        }


    }
}
