using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FrmRecursos : Form
    {

        private readonly Conexion _conexion; // Se declara la conexión a la base de datos.

        public FrmRecursos()
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString); // Se inicializa la conexión a la base de datos.
            CargarDatos(); // Se cargan los datos en el listado.


        }


        // El método 'CargarDatos' se encarga de cargar los recursos desde la base de datos
        // y mostrar los datos en un DataGridView. Si hay algún error al intentar cargar
        // los datos, se muestra un mensaje de error.

        public void CargarDatos()
        {
            try
            {

                var recursos = _conexion.CargarRecursos(); // Obtiene los recursos desde la base de datos a través de la conexión.
                dataGridView1.DataSource = recursos; // Asigna los recursos obtenidos como la fuente de datos para el DataGridView.


                // Verifica si la columna "FechaAdquisicion" es de tipo 'DateTime'.
                if (dataGridView1.Columns["FechaAdquisicion"].ValueType == typeof(DateTime))
                {
                    // Si es de tipo 'DateTime', aplica un formato de fecha "dd/MM/yyyy".
                    dataGridView1.Columns["FechaAdquisicion"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error al cargar los datos, muestra un mensaje de error.
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }

        }//Fin de CargarDatos

        //Cierra esta vista
        private void CerrarRecursos_Click(object sender, EventArgs e)
        {
            this.Close();
        }//Fin de cerrarRecursos_Click

        //Método que se encarga de eliminar un recurso seleccionado en el DataGridView.
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si se ha seleccionado al menos una fila en el DataGridView
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    // Muestra un mensaje de confirmación para eliminar el recurso.
                    DialogResult result = MessageBox.Show(
                        "¿Estás seguro de que deseas eliminar este recurso?", // Mensaje de confirmación
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo, // Botones de confirmación
                        MessageBoxIcon.Warning // Icono de advertencia
                    );

                    // Si el usuario confirma (responde "Sí"), se procede con la eliminación.
                    if (result == DialogResult.Yes)
                    {
                        int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdRecurso"].Value.ToString()); // Obtiene el ID del recurso de la fila seleccionada.
                        _conexion.EliminarRecurso(id); // Llama al método de la clase _conexion para eliminar el recurso de la base de datos.
                        CargarDatos();  // Vuelve a cargar los datos actualizados en el DataGridView
                        MessageBox.Show("Recurso eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje de éxito una vez
                                                                                                                                        // eliminado el recurso.
                    }
                }
                else
                {
                    // Si no se selecciona ningún recurso, muestra un mensaje de advertencia.
                    MessageBox.Show("Selecciona un recurso para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error al intentar eliminar el recurso, muestra un mensaje de error.
                MessageBox.Show("Ocurrió un error al eliminar el recurso: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin de eliminarToolStripMenuItem_Click


        // Método que maneja el evento de clic en el botón "Agregar". Este botón se utiliza para abrir
        // el formulario para agregar un nuevo recurso
        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            // Llama al método 'MostrarAddFrame' de la clase FrmPrincipal para mostrar el formulario
            // de agregar recursos. Se pasa un nuevo objeto FrmAddRecursos con el parámetro 0,
            // lo cual puede indicar que es un nuevo recurso (no editado) que será creado.
           

            FrmAddRecursos frm = new FrmAddRecursos(0, this); // Pasamos la referencia
            frm.ShowDialog(); // Modal respecto a FrmEmpleados

        }//Fin de btn_Agregar_Click

        // Método que maneja el evento de clic en el menú "Editar". Este método se ejecuta cuando el usuario selecciona un recurso
        // en el DataGridView y elige la opción de editar desde el menú.
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si se ha seleccionado al menos una fila en el DataGridView.
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int id = int.Parse(this.dataGridView1.SelectedRows[0].Cells["IdRecurso"].Value.ToString());
                    FrmAddRecursos frm = new FrmAddRecursos(id, this);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin de editarToolStripMenuItem_Click

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            

        }//Fin de btn_Imprimir_Click


        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_buscar.Text) || txt_buscar.Text == "Buscar recurso...")
                {
                    CargarDatos();
                }
                else
                {
                    DataTable resultados = _conexion.BuscarRecursoPorNombre(txt_buscar.Text);

                    if (resultados.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = resultados;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;

                        MessageBox.Show("No se encontraron recursos con ese nombre.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txt_buscar.Text == "Buscar recurso...")
            {
                txt_buscar.Text = "";
                txt_buscar.ForeColor = Color.Black;
            }
        }

        private void txt_buscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar.Text))
            {
                txt_buscar.Text = "Buscar recurso...";
                txt_buscar.ForeColor = Color.Gray;
            }
        }


    }//Fin de clase

}//Fin de namespace
