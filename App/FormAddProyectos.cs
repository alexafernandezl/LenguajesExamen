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
        private decimal presupuestoOriginal;


        // Constructor de la clase que recibe el ID del proyecto
        public FormAddProyectos(int id, FrmProyectos frmProyectos)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            proyectoMain = _conexion.BuscarProyectoPorId(id);
            this.frmProyectos = frmProyectos;

            CargarResponsables(); // Carga la lista de empleados en el ComboBox

            if (proyectoMain != null)
            {
                CargarDatos(); // Carga los datos del proyecto si existe
                estadoAnterior = proyectoMain.Estado;
            }
            else
            {
                cb_Estado.SelectedItem = "Pendiente"; // Estado por defecto
            }

            txt_Presupuesto.KeyPress += ValidarEntradaDecimal;
            cal_Inicio.DateChanged += ValidarFechas;
            cal_finEstimada.DateChanged += ValidarFechas;
            cb_Estado.SelectedIndexChanged += cb_Estado_SelectedIndexChanged;
            txt_Presupuesto.TextChanged += (s, e) => ValidarPresupuesto(); // Validar presupuesto en tiempo real
        }

        // Método para cargar los datos del proyecto en los campos del formulario
        private void CargarDatos()
        {
            txt_NombreProyecto.Text = proyectoMain.NombreProyecto;
            txt_Presupuesto.Text = proyectoMain.Presupuesto.ToString("N2", CultureInfo.InvariantCulture);
            cb_Estado.SelectedItem = proyectoMain.Estado;
            txt_Descripcion.Text = proyectoMain.Descripcion;
            cbResponsable.SelectedValue = proyectoMain.IdResponsable;
            cal_Inicio.SetDate(proyectoMain.FechaDeInicio);
            cal_finEstimada.SetDate(proyectoMain.FechaDeFinEstimada);

            ConfigurarOpcionesEstado(proyectoMain.Estado);
            presupuestoOriginal = proyectoMain.Presupuesto;
        }
        // Método para validar que solo se ingresen números y punto decimal en txt_Presupuesto
        private void ValidarEntradaDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquear cualquier entrada que no sea número o punto decimal
            }
        }

        // Método para configurar las opciones del ComboBox de Estado según el estado actual del proyecto
        private void ConfigurarOpcionesEstado(string estadoActual)
        {
            cb_Estado.Items.Clear();

            switch (estadoActual)
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
                    break; // No permite cambios
            }

            if (cb_Estado.Items.Count > 0)
            {
                cb_Estado.SelectedIndex = 0;
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
            cbResponsable.DisplayMember = "NombreCompleto";
            cbResponsable.ValueMember = "IdEmpleado";
            cbResponsable.SelectedIndex = 0;
        }

        // Validación de fechas en tiempo real
        private void ValidarFechas(object sender, EventArgs e)
        {
            if (cal_Inicio.SelectionStart > cal_finEstimada.SelectionStart)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin estimada. Se recomienda escoger primero la fecha final", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cal_Inicio.SetDate(DateTime.Today);
            }
        }

        // Validación de campos antes de guardar el proyecto
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txt_NombreProyecto.Text) )
            {
                MessageBox.Show("El campo Nombre del Proyecto no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_Descripcion.Text))
            {
                MessageBox.Show("El campo Descripción no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
          
           
            if (string.IsNullOrWhiteSpace(txt_Presupuesto.Text) || !decimal.TryParse(txt_Presupuesto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("El campo Presupuesto debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_Estado.SelectedItem == null)
            {
                cb_Estado.SelectedItem = "Pendiente";
            }
            if (cbResponsable.SelectedIndex == 0 || cbResponsable.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Debe seleccionar un responsable para el proyecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Evento que maneja cambios de estado en tiempo real
        private void cb_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proyectoMain == null) return;

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
                DialogResult confirmacion = MessageBox.Show("Todas las tareas se cancelarán y los recursos se liberarán. ¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.Yes)
                {
                    //_conexion.CancelarTareasAsociadas(proyectoMain.IdProyecto);
                    //_conexion.LiberarRecursos(proyectoMain.IdProyecto);
                    MessageBox.Show("Tareas canceladas y recursos liberados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cb_Estado.SelectedItem = estadoAnterior;
                }
            }

            estadoAnterior = nuevoEstado;
        }

        // Método para validar presupuesto en tiempo real
        private void ValidarPresupuesto()
        {
            if (proyectoMain == null) return;

            decimal costoTotalRecursos = _conexion.CalcularCostoTotalRecursos(proyectoMain.IdProyecto);
            decimal presupuesto;

            if (!decimal.TryParse(txt_Presupuesto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out presupuesto))
            {
                MessageBox.Show("Ingrese un presupuesto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Presupuesto.Text = presupuestoOriginal.ToString("N2", CultureInfo.InvariantCulture); // Restaurar presupuesto original
                return;
            }

            if (costoTotalRecursos > presupuesto)
            {
                MessageBox.Show($"El costo de los recursos (${costoTotalRecursos:N2}) excede el presupuesto (${presupuesto:N2}).\n" +
                                $"Reajuste el presupuesto a los recursos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Presupuesto.Text = presupuestoOriginal.ToString("N2", CultureInfo.InvariantCulture); // Restaurar el valor original
            }
        }

        private void cbResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbResponsable.SelectedIndex > 0)
            {
                int idEmpleado = Convert.ToInt32(cbResponsable.SelectedValue);
                DateTime fechaInicio = cal_Inicio.SelectionStart;
                DateTime fechaFin = cal_finEstimada.SelectionStart;
                int idProyecto = proyectoMain?.IdProyecto ?? 0;

                if (_conexion.VerificarDisponibilidadEmpleado(idEmpleado, fechaInicio, fechaFin, idProyecto))
                {
                    MessageBox.Show("Este empleado ya está asignado a otro proyecto con fechas solapadas. " +
                        "Por favor, ajuste las fechas o seleccione otro responsable.", "Conflicto de asignación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbResponsable.SelectedIndex = 0;
                }
            }
        }


        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

             

                    // Validar que el estado no sea "Cancelado" ni "Completado"
                    string estadoSeleccionado = cb_Estado.SelectedItem.ToString();
                if (estadoSeleccionado == "Cancelado" || estadoSeleccionado == "Completado")
                {
                    MessageBox.Show("No puede asignar un empleado a un proyecto cancelado o completado.",
                        "Estado inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar solapamiento antes de guardar
                int idEmpleado = Convert.ToInt32(cbResponsable.SelectedValue);
                DateTime fechaInicio = cal_Inicio.SelectionStart;
                DateTime fechaFin = cal_finEstimada.SelectionStart;
                int idProyecto = proyectoMain?.IdProyecto ?? 0;

                if (_conexion.VerificarDisponibilidadEmpleado(idEmpleado, fechaInicio, fechaFin, idProyecto))
                {
                    MessageBox.Show("Este empleado ya está asignado a otro proyecto con fechas solapadas. " +
                        "Por favor, reajuste las fechas o seleccione otro responsable.",
                        "Conflicto de asignación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string mensaje = proyectoMain != null
                    ? "¿Está seguro de que desea modificar este proyecto?"
                    : "¿Está seguro de que desea crear este proyecto?";

                DialogResult confirmacion = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes) return;

                Proyectos temp = new Proyectos()
                {
                    NombreProyecto = txt_NombreProyecto.Text,
                    Presupuesto = decimal.Parse(txt_Presupuesto.Text, CultureInfo.InvariantCulture),
                    FechaDeFinEstimada = fechaFin,
                    FechaDeInicio = fechaInicio,
                    Estado = estadoSeleccionado,
                    Descripcion = txt_Descripcion.Text,
                    IdResponsable = idEmpleado,
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
                if (estadoSeleccionado == "Cancelado")
                {
                    _conexion.CancelarTareasAsociadas(temp.IdProyecto);
                    MessageBox.Show("Todas las tareas del proyecto han sido canceladas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
