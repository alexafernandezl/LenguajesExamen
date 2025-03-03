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
using BLL;
using DAL;

namespace App
{
    public partial class FrmAddTareas : Form
    {

        private Tareas tareaMain;
        private readonly Conexion _conexion;
        private FrmTareas frmTareas;
        public FrmAddTareas(int id, FrmTareas frmTareas)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            tareaMain = _conexion.BuscarTareaPorId(id);
            this.frmTareas = frmTareas;
            
            CargarResponsables();
            CargarProyectos();
            CargarRecursos();
            if (tareaMain != null)
            {
                CargarDatos(); // Carga los datos del proyecto si existe
                estadoAnterior = tareaMain.Estado;
            }
            else
            {
                cb_Estado.SelectedItem = "Pendiente"; // Estado por defecto
            }
            cal_Inicio.DateChanged += ValidarSolapamientoFechas;
            cal_finEstimada.DateChanged += ValidarSolapamientoFechas;

            cb_Estado.SelectedIndexChanged += cb_Estado_SelectedIndexChanged;
        }
       
        private void CargarResponsables()
        {
            DataTable empleados = _conexion.CargarEmpleados();
            DataRow row = empleados.NewRow();
            row["IdEmpleado"] = DBNull.Value;
            row["NombreCompleto"] = "Seleccione un responsable";
            empleados.Rows.InsertAt(row, 0);
            cb_idResponsable.DataSource = empleados;
            cb_idResponsable.DisplayMember = "NombreCompleto";//muestra el nombre
            cb_idResponsable.ValueMember = "IdEmpleado";//almacena id
            cb_idResponsable.SelectedIndex = 0;

        }

        private void CargarProyectos()
        {
            DataTable proyectos = _conexion.CargarProyectos(); DataRow row = proyectos.NewRow();
            row["IdProyecto"] = DBNull.Value; row["NombreProyecto"] = "Seleccione un proyecto";
            proyectos.Rows.InsertAt(row, 0);
            cb_idProyecto.DataSource = proyectos; cb_idProyecto.DisplayMember = "NombreProyecto";//muestra el nombre
            cb_idProyecto.ValueMember = "IdProyecto";//almacena id
            cb_idProyecto.SelectedIndex = 0;
        }

        private void CargarRecursos()
        {
            // Cargar todos los recursos desde la base de datos
            DataTable recursos = _conexion.CargarRecursos();

            // Filtrar los recursos para excluir los que tienen el estado "Inactivo"
            DataTable recursosFiltrados = recursos.Clone(); // Clona la estructura del DataTable
            foreach (DataRow row in recursos.Rows)
            {
                if (row["Estado"].ToString() != "Inactivo") // Verifica que el estado no sea "Inactivo"
                {
                    recursosFiltrados.ImportRow(row); // Agrega la fila al DataTable filtrado
                }
            }

            // Agregar la opción "Seleccione un recurso" al inicio
            DataRow nuevaFila = recursosFiltrados.NewRow();
            nuevaFila["IdRecurso"] = DBNull.Value;
            nuevaFila["NombreRecurso"] = "Seleccione un recurso";
            recursosFiltrados.Rows.InsertAt(nuevaFila, 0);

            // Asignar el DataTable filtrado como DataSource del ComboBox
            cb_idRecurso.DataSource = recursosFiltrados;
            cb_idRecurso.DisplayMember = "NombreRecurso"; // Muestra el nombre
            cb_idRecurso.ValueMember = "IdRecurso"; // Almacena el ID
            cb_idRecurso.SelectedIndex = 0; // Selecciona la opción por defecto

        }

        private void CargarDatos()
        {
            cb_idRecurso.SelectedValue = Convert.ToInt32(this.tareaMain.IdRecurso);
            cb_idResponsable.SelectedValue = Convert.ToInt32(this.tareaMain.IdResponsable);
            cb_idProyecto.SelectedValue = Convert.ToInt32(this.tareaMain.IdProyecto);
            txt_Descripcion.Text = this.tareaMain.Descripcion;
            cb_Estado.SelectedItem = this.tareaMain.Estado;
            cal_Inicio.SetDate(this.tareaMain.FechaInicio);
            cal_finEstimada.SetDate(this.tareaMain.FechaFinEstimada);
            cb_Prioridad.SelectedItem = this.tareaMain.Prioridad;
            cb_requiere.SelectedItem = this.tareaMain.Requiere;
            txt_cantidad.Text = this.tareaMain.Cantidad.ToString();

            // Guardar estado anterior para validaciones
            estadoAnterior = this.tareaMain.Estado;

            // Limpiar opciones del ComboBox antes de agregar nuevas
            cb_Estado.Items.Clear();

            // Configurar opciones del estado según el estado actual
            switch (tareaMain.Estado)
            {
                case "Pendiente por defecto":
                    cb_Estado.Items.Add("Pendiente por defecto");
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
                    cb_Estado.Items.Add("Cancelado"); // No permitir cambios desde cancelado
                    break;
            }

            // Asegurar que el ComboBox tiene elementos antes de asignar el índice
            if (cb_Estado.Items.Count > 0)
            {
                // Si el estado de la tarea está en la lista, seleccionarlo
                if (cb_Estado.Items.Contains(this.tareaMain.Estado))
                {
                    cb_Estado.SelectedItem = this.tareaMain.Estado;
                }
                else
                {
                    cb_Estado.SelectedIndex = 0; // Solo asignar si hay elementos disponibles
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txt_Descripcion.Text) || txt_Descripcion.Text.Length > 500)
            {
                MessageBox.Show("La descripción es obligatoria y debe tener un máximo de 500 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false;
            }
            if (cb_Estado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado para la tarea.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_Prioridad.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una prioridad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_requiere.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el rol requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_idProyecto.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proyecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cb_idResponsable.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un responsable.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cal_Inicio.SelectionStart == DateTime.MinValue)
            {
                MessageBox.Show("Debe seleccionar una fecha de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cal_finEstimada.SelectionStart == DateTime.MinValue)
            {
                MessageBox.Show("Debe seleccionar una fecha de fin estimada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void VerificarSolapamientoFechas()
        {
            if (tareaMain == null) return;

            DateTime nuevaFechaInicio = cal_Inicio.SelectionStart;
            DateTime nuevaFechaFin = cal_finEstimada.SelectionStart;
            int idRecurso = Convert.ToInt32(cb_idRecurso.SelectedValue);
            int idTareaActual = tareaMain.IdTarea;

            // 🔍 Verificar si hay otras tareas que usan el mismo recurso en el mismo periodo
            List<Tareas> tareasSolapadas = _conexion.ObtenerTareasSolapadas(idRecurso, nuevaFechaInicio, nuevaFechaFin, idTareaActual);

            if (tareasSolapadas.Count > 0)
            {
                string mensaje = "El recurso seleccionado ya está siendo utilizado en las siguientes tareas:\n";
                foreach (var tarea in tareasSolapadas)
                {
                    mensaje += $"- {tarea.Descripcion} (Del {tarea.FechaInicio.ToShortDateString()} al {tarea.FechaFinEstimada.ToShortDateString()})\n";
                }

                mensaje += "\nSeleccione una opción:\n\n1. Ajustar la fecha automáticamente\n2. Seleccionar otro recurso\n3. Cancelar el cambio";

                DialogResult result = MessageBox.Show(mensaje, "Conflicto de Fechas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // 📌 Encuentra la próxima fecha disponible para el recurso
                    DateTime nuevaFechaDisponible = _conexion.ObtenerProximaFechaDisponible(idRecurso, nuevaFechaInicio);
                    cal_Inicio.SetDate(nuevaFechaDisponible);
                    cal_finEstimada.SetDate(nuevaFechaDisponible.AddDays((nuevaFechaFin - nuevaFechaInicio).Days));

                    MessageBox.Show("La fecha ha sido ajustada automáticamente.", "Ajuste realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.No)
                {
                    // 📌 Permite al usuario seleccionar otro recurso
                    MessageBox.Show("Seleccione otro recurso para esta tarea.", "Reasignación de recurso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cb_idRecurso.Focus();
                }
                else
                {
                    // ❌ Cancela el cambio y vuelve a la fecha anterior
                    cal_Inicio.SetDate(tareaMain.FechaInicio);
                    cal_finEstimada.SetDate(tareaMain.FechaFinEstimada);
                }
            }
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tareaMain != null)
                {
                    if (ValidarCampos())
                    {
                        Tareas ob = _conexion.BuscarTareaPorId(tareaMain.IdTarea);
                        Tareas t = _conexion.BuscarTareaPorFecha(tareaMain);

                        if (ob.IdTarea == this.tareaMain.IdTarea)
                        {
                            

                            // Validar conflicto de horarios con el responsable
                            DateTime fechaInicio = this.cal_Inicio.SelectionStart;
                            DateTime fechaFinEstimada = this.cal_finEstimada.SelectionStart;
                            int idResponsable = Convert.ToInt32(cb_idResponsable.SelectedValue);

                            if (t.IdTarea != tareaMain.IdTarea)
                            {
                                if (_conexion.EmpleadoTieneConflictoDeHorario(idResponsable, fechaInicio, fechaFinEstimada))
                                {
                                    MessageBox.Show("El empleado tiene un conflicto de horario con esta tarea.", "",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            if (!cb_Estado.SelectedItem.Equals("Cancelado"))
                            {
                                int idRecurso = Convert.ToInt32(cb_idRecurso.SelectedValue);

                                int cantidadRequerida = int.Parse(this.txt_cantidad.Text);
                                if (!_conexion.ValidarDisponibilidadRecurso(idRecurso, cantidadRequerida))
                                {
                                    MessageBox.Show("El recurso no tiene suficiente cantidad disponibilidad.", "",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Validar que la fecha de inicio sea menor a la fecha de fin estimada
                            if (fechaInicio > fechaFinEstimada)
                            {
                                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de finalización.",
                                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Validar existencia de tarea pendiente con mayor prioridad
                            string Prioridad = this.cb_Prioridad.SelectedItem.ToString();
                            if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 1)
                            {
                                MessageBox.Show("Hay tareas de alta prioridad pendientes.", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 2)
                            {
                                MessageBox.Show("Hay tareas de la misma prioridad de fechas anteriores.", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 3)
                            {
                                MessageBox.Show("Conflicto de horario con otra tarea.", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            //validaciones rol de usuario


                            string rolResponsable = _conexion.RolResponsable(idResponsable);

                            string rolRequerido = this.cb_requiere.SelectedItem.ToString();

                            if (rolResponsable != rolRequerido)
                            {
                                MessageBox.Show("El empleado no tiene el rol adecuado para esta tarea.", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            int cantidad;
                            int.TryParse(this.txt_cantidad.Text, out cantidad);

                            // Si todas las validaciones son exitosas, modificamos la tarea
                            Tareas temp = new Tareas
                            {

                                FechaInicio = fechaInicio,
                                FechaFinEstimada = fechaFinEstimada,
                                Descripcion = this.txt_Descripcion.Text,
                                Estado = this.cb_Estado.SelectedItem.ToString(),
                                Prioridad = this.cb_Prioridad.SelectedItem.ToString(),
                                Requiere = this.cb_requiere.SelectedItem.ToString(),
                                IdProyecto = Convert.ToInt32(cb_idProyecto.SelectedValue),
                                IdResponsable = Convert.ToInt32(cb_idResponsable.SelectedValue),
                                IdRecurso = Convert.ToInt32(cb_idRecurso.SelectedValue),
                                Cantidad = cantidad,
                                IdTarea = ob.IdTarea
                            };

                            if (t.IdTarea == temp.IdTarea) { 
                            _conexion.ModificarTarea(temp);
                                frmTareas.CargarDatos();
                            }
                            this.Close();

                            // Mostrar el formulario de tareas
                          
                        }
                        else
                        {
                            MessageBox.Show("No existe este recurso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (ValidarCampos())
                    {
                        // Validar conflicto de horarios con el responsable
                        DateTime fechaInicio = this.cal_Inicio.SelectionStart;
                        DateTime fechaFinEstimada = this.cal_finEstimada.SelectionStart;
                        if (cb_idRecurso.SelectedValue == null || cb_idResponsable.SelectedValue == DBNull.Value)
                        {
                            MessageBox.Show("Debe seleccionar un recurso válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int idResponsable = Convert.ToInt32(cb_idResponsable.SelectedValue);

                        // Validar que la fecha de inicio sea menor a la fecha de fin estimada
                        if (fechaInicio > fechaFinEstimada)
                        {
                            MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de finalización.",
                                            "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (_conexion.EmpleadoTieneConflictoDeHorario(idResponsable, fechaInicio, fechaFinEstimada))
                        {
                            MessageBox.Show("El empleado tiene un conflicto de horario con esta tarea.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        // Validar disponibilidad del recurso
                        int idRecurso = Convert.ToInt32(cb_idRecurso.SelectedValue);
                        int cantidadRequerida = int.Parse(this.txt_cantidad.Text);
                        if (!_conexion.ValidarDisponibilidadRecurso(idRecurso, cantidadRequerida))
                        {
                            MessageBox.Show("El recurso no tiene suficiente disponibilidad.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        // Validar existencia de tarea pendiente con mayor prioridad
                        string Prioridad = this.cb_Prioridad.SelectedItem.ToString();
                        if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 1)
                        {
                            MessageBox.Show("Hay tareas de alta prioridad pendientes.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 2)
                        {
                            MessageBox.Show("Hay tareas de la misma prioridad de fechas anteriores.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_conexion.VerificarAsignacionTarea(idResponsable, fechaInicio, fechaFinEstimada, Prioridad) == 3)
                        {
                            MessageBox.Show("Conflicto de horario con otra tarea.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        //validaciones rol de usuario
                        int Responsable = Convert.ToInt32(cb_idResponsable.SelectedValue);
                        string rolResponsable = _conexion.RolResponsable(Responsable);
                        string rolRequerido = this.cb_requiere.SelectedItem.ToString();

                        if (rolResponsable != rolRequerido)
                        {
                            MessageBox.Show("El empleado no tiene el rol adecuado para esta tarea.", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int idProyecto = Convert.ToInt32(cb_idResponsable.SelectedValue);
                        int cantidad;
                        int.TryParse(this.txt_cantidad.Text, out cantidad);
                        // Guardar nueva tarea
                        Tareas temp = new Tareas
                        {
                            FechaInicio = fechaInicio,
                            FechaFinEstimada = fechaFinEstimada,
                            Descripcion = this.txt_Descripcion.Text,
                            Estado = this.cb_Estado.SelectedItem.ToString(),
                            Prioridad = this.cb_Prioridad.SelectedItem.ToString(),
                            Requiere = this.cb_requiere.SelectedItem.ToString(),
                            IdProyecto = Convert.ToInt32(cb_idProyecto.SelectedValue),
                            IdResponsable = Convert.ToInt32(cb_idResponsable.SelectedValue),
                            IdRecurso = Convert.ToInt32(cb_idRecurso.SelectedValue),
                            Cantidad = cantidad,
                        };
                        _conexion.GuardarTarea(temp);
                        frmTareas.CargarDatos();

                        this.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }//fin del metodo

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cb_idProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números enteros y teclas de control (como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada del carácter
                MessageBox.Show("Solo se permiten números enteros.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cb_IdResponsable_SelectedIndex_Changed(object sender, EventArgs e)
        {

        }
        private string estadoAnterior;
        // Evento que maneja cambios de estado en tiempo real
        private void cb_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tareaMain == null) return;

            string nuevoEstado = cb_Estado.SelectedItem.ToString();

            if (nuevoEstado == "En Progreso" && estadoAnterior == "Pendiente por defecto")
            {
                MessageBox.Show("Un proyecto no puede pasar de 'Pendiente' a 'En Progreso' sin aprobación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_Estado.SelectedItem = estadoAnterior;
                return;
            }

            estadoAnterior = nuevoEstado;
        }

        private void ValidarSolapamientoFechas(object sender, DateRangeEventArgs e)
        {
            VerificarSolapamientoFechas();
        }
    }
}
