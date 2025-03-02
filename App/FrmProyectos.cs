using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            CargarDatos();// Cargar los datos de los proyectos al iniciar el formulario
        }

        // Método para cargar los proyectos en el DataGridView

        public void CargarDatos()
        {
            try
            {
                this.dataGridView1.DataSource = _conexion.CargarProyectos();

                if (dataGridView1.Columns["FechaDeInicio"].ValueType == typeof(DateTime))
                {
                    dataGridView1.Columns["FechaDeInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dataGridView1.Columns["FechaDeFinEstimada"].ValueType == typeof(DateTime))
                {
                    dataGridView1.Columns["FechaDeFinEstimada"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Método para cerrar el formulario
        private void pb_CerrarProyectos_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Método para eliminar un proyecto con confirmación y validación de dependencia con tareas
       
       private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int idProyecto = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdProyecto"].Value.ToString());

                    // Verificar si existen tareas asociadas a este proyecto
                    DataTable tareas = _conexion.CargarTareas();
                    bool tieneTareas = tareas.AsEnumerable().Any(row => row.Field<int>("IdProyecto") == idProyecto);

                    if (tieneTareas)
                    {
                        MessageBox.Show("No se puede eliminar el proyecto porque tiene tareas asociadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Confirmación antes de eliminar
                    DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este proyecto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        _conexion.EliminarProyecto(idProyecto);
                        CargarDatos();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un proyecto para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el proyecto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            FormAddProyectos frm = new FormAddProyectos(0, this); // Pasamos la referencia
            frm.ShowDialog();
        }


        // Método para editar un proyecto existente

       private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdProyecto"].Value.ToString());
                    FormAddProyectos frm = new FormAddProyectos(id, this);
                    
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Seleccione un proyecto para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar el proyecto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreBusqueda = txt_buscar.Text.Trim();

                if (!string.IsNullOrEmpty(nombreBusqueda))
                {
                    DataTable dt = _conexion.BuscarEmpleadoNombre(nombreBusqueda);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    // Si no hay texto, cargar todos los datos
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_buscar.Text))
                {
                    // Si el TextBox está vacío, carga todos los empleados
                    CargarDatos();
                }
                else
                {
                    // Buscar empleados por nombre parcial
                    DataTable resultados = _conexion.BuscarProyectoPorNombre(txt_buscar.Text);

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
            if (txt_buscar.Text == "Buscar proyecto...")
            {
                txt_buscar.Text = "";
                txt_buscar.ForeColor = Color.Black;
            }
        }

        private void txt_buscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar.Text))
            {
                txt_buscar.Text = "Buscar proyecto...";
                txt_buscar.ForeColor = Color.Gray;
            }
        }
    }
}
