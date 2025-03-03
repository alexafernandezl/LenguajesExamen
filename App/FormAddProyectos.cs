using BLL;
using DAL;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace App
{
    public partial class FormAddProyectos : Form
    {
        private readonly Conexion _conexion;
        public Proyectos proyectoMain;
        private FrmProyectos frmProyectos;
        private string estadoAnterior; // Guarda el estado anterior antes de cambiarlo

        // Constructor de la clase que recibe el ID del proyecto
        public FormAddProyectos(int id, FrmProyectos frmProyectos)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            proyectoMain = _conexion.BuscarProyectoPorId(id); // Valida si el form tiene un id para saber si debe insertar o modificar
            this.frmProyectos = frmProyectos;

            CargarResponsables(); // Carga la lista de empleados en el ComboBox

            if (proyectoMain != null)
            {
                CargarDatos(); // Carga los datos del proyecto si existe
                estadoAnterior = proyectoMain.Estado; // Guarda el estado actual
            }
            else
            {
                cb_Estado.SelectedItem = "Pendiente"; // Estado por defecto para nuevos proyectos
            }

            txt_Presupuesto.KeyPress += ValidarEntradaDecimal; // No se puede insertar letras  
            cal_Inicio.DateChanged += ValidarFechas; // Validar que la fecha de inicio sea menor
            cal_finEstimada.DateChanged += ValidarFechas; // Validar que la fecha de fin sea mayor

            cb_Estado.SelectedIndexChanged += cb_Estado_SelectedIndexChanged; // Evento en tiempo real
        }

        // Método para cargar los datos del proyecto en los campos del formulario
        private void CargarDatos()
        {
            txt_NombreProyecto.Text = proyectoMain.NombreProyecto;
            txt_Presupuesto.Text = proyectoMain.Presupuesto.ToString("N2", CultureInfo.InvariantCulture); // Muestra solo 2 dígitos
            cb_Estado.SelectedItem = proyectoMain.Estado;
            txt_Descripcion.Text = proyectoMain.Descripcion;
            cbResponsable.SelectedValue = proyectoMain.IdResponsable;
            cal_Inicio.SetDate(this.proyectoMain.FechaDeInicio);
            cal_finEstimada.SetDate(this.proyectoMain.FechaDeFinEstimada);


            cb_Estado.Items.Clear();

            switch (proyectoMain.Estado)
            {
                case "Pendiente":
                    cb_Estado.Items.Add("Pendiente");
                    cb_Estado.Items.Add("Aprobado");
                    cb_Estado.Items.Add("En Progreso");
                    cb_Estado.Items.Add("Cancelado");
                    break;

                case "Aprobado":
                    cb_Estado.Items.Add("Aprobado");
                    cb_Estado.Items.Add("En Progreso");
                    cb_Estado.Items.Add("Cancelado");
                    break;

                case "En Progreso":

                    cb_Estado.Items.Add("En Progreso");
                    cb_Estado.Items.Add("Completado");
                    cb_Estado.Items.Add("Cancelado");
                    break;

                case "Completado":
                    cb_Estado.Items.Add("Completado");
                    cb_Estado.Items.Add("Cancelado");
                    break;

                case "Cancelado":
                    // No se agregan opciones porque el proyecto ya está cancelado
                    break;
            }

            // Establecer el estado actual en el ComboBox
            if (cb_Estado.Items.Count > 0)
            {
                cb_Estado.SelectedIndex = 0; // Selecciona la primera opción disponible
            }
        }

        // Método para cargar la lista de empleados en el ComboBox
        private void CargarResponsables()
        {
            DataTable empleados = _conexion.CargarEmpleados();
            DataRow row = empleados.NewRow();
            row["IdEmpleado"] = DBNull.Value;
            row["NombreCompleto"] = "Seleccione un responsable";
            empleados.Rows.InsertAt(row, 0);

            cbResponsable.DataSource = empleados;
            cbResponsable.DisplayMember = "NombreCompleto"; // Muestra el nombre
            cbResponsable.ValueMember = "IdEmpleado"; // Almacena ID
            cbResponsable.SelectedIndex = 0;
        }

        // Validación para asegurar que la fecha de inicio no sea posterior a la fecha de fin en tiempo real
        private void ValidarFechas(object sender, EventArgs e)
        {
            if (cal_Inicio.SelectionStart > cal_finEstimada.SelectionStart)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin estimada. Se recomienda elegir primero la fecha final.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cal_Inicio.SetDate(DateTime.Today.AddDays(0));
            }
        }

        // Validación de campos antes de guardar el proyecto
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txt_NombreProyecto.Text))
            {
                MessageBox.Show("El campo Nombre del Proyecto no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_Presupuesto.Text) || !decimal.TryParse(txt_Presupuesto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("El campo Presupuesto debe ser un valor numérico válido y no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_Descripcion.Text))
            {
                MessageBox.Show("El campo Descripción no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_Estado.SelectedItem == null)
            {
                cb_Estado.SelectedItem = "Pendiente"; // Si no selecciona, asigna por defecto
            }
            if (cbResponsable.SelectedIndex == 0 || cbResponsable.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Debe seleccionar un responsable para el proyecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Validación para permitir solo números y punto decimal en el campo Presupuesto
        private void ValidarEntradaDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        // Evento que maneja cambios de estado en tiempo real
        private void cb_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proyectoMain == null) return; // Si es un nuevo proyecto, no validar estados

            string nuevoEstado = cb_Estado.SelectedItem.ToString();

            if (nuevoEstado == "En Progreso" && estadoAnterior == "Pendiente")
            {
                MessageBox.Show("Un proyecto no puede pasar de 'Pendiente' a 'En Progreso' sin aprobación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_Estado.SelectedItem = estadoAnterior;
                return;
            }

            if (nuevoEstado == "Completado" && !_conexion.TodasTareasFinalizadas(proyectoMain.IdProyecto))
            {
                MessageBox.Show("No se puede marcar el proyecto como 'Completado' hasta que todas sus tareas estén finalizadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_Estado.SelectedItem = estadoAnterior;
                return;
            }

            if (nuevoEstado == "Cancelado")
            {
                DialogResult confirmacion = MessageBox.Show("Al cancelar el proyecto, todas las tareas se marcarán como 'Canceladas' y los recursos serán liberados. ¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.Yes)
                {
                    _conexion.CancelarTareasAsociadas(proyectoMain.IdProyecto);
                    _conexion.LiberarRecursos(proyectoMain.IdProyecto);
                    MessageBox.Show("Todas las tareas han sido canceladas y los recursos liberados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cb_Estado.SelectedItem = estadoAnterior; // Revertir el cambio
                }
            }

            estadoAnterior = nuevoEstado; // Actualizar el estado
        }

        // Método para guardar o modificar un proyecto
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                string mensaje = proyectoMain != null
                    ? "¿Está seguro de que desea modificar este proyecto?"
                    : "¿Está seguro de que desea crear este proyecto?";

                DialogResult confirmacion = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes) return;

                Proyectos temp = new Proyectos()
                {
                    NombreProyecto = txt_NombreProyecto.Text,
                    Presupuesto = decimal.Parse(txt_Presupuesto.Text, CultureInfo.InvariantCulture),
                    FechaDeFinEstimada = cal_finEstimada.SelectionStart,
                    FechaDeInicio = cal_Inicio.SelectionStart,
                    Estado = cb_Estado.SelectedItem.ToString(),
                    Descripcion = txt_Descripcion.Text,
                    IdResponsable = Convert.ToInt32(cbResponsable.SelectedValue),
                };

                if (proyectoMain != null)
                {
                    temp.IdProyecto = proyectoMain.IdProyecto;
                    _conexion.ModificarProyecto(temp);
                }
                else
                {
                    _conexion.GuardarProyecto(temp);
                }

                frmProyectos.CargarDatos();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        

    }
}
