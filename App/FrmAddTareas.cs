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
        public FrmAddTareas(int id)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            tareaMain = _conexion.BuscarTareaPorId(id);

            if (tareaMain != null)
            {
                CargarDatos();
            }
            CargarResponsables();
            CargarProyectos();
            CargarRecursos();
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
            DataTable recursos = _conexion.CargarRecursos(); DataRow row = recursos.NewRow();
            row["IdRecurso"] = DBNull.Value; row["NombreRecurso"] = "Seleccione un recurso";
            recursos.Rows.InsertAt(row, 0);
            cb_idRecurso.DataSource = recursos; cb_idRecurso.DisplayMember = "NombreRecurso";//muestra el nombre
            cb_idRecurso.ValueMember = "IdRecurso";//almacena id
            cb_idRecurso.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            cb_idRecurso.SelectedValue = this.tareaMain.IdRecurso;
            cb_idResponsable.SelectedValue = this.tareaMain.IdResponsable;
            cb_idProyecto.SelectedValue = this.tareaMain.IdProyecto;
            txt_Descripcion.Text = this.tareaMain.Descripcion;
            cb_Estado.SelectedItem = this.tareaMain.Estado;
            cal_Inicio.SetDate(this.tareaMain.FechaInicio);
            cal_finEstimada.SetDate(this.tareaMain.FechaFinEstimada);
            cb_Prioridad.SelectedItem = this.tareaMain.Prioridad;
            cb_requiere.SelectedItem = this.tareaMain.Requiere;
            txt_cantidad.Text = this.tareaMain.Cantidad.ToString();
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

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tareaMain != null)
                {
                    if (ValidarCampos())
                    {
                        Tareas ob = _conexion.BuscarTareaPorId(tareaMain.IdTarea);

                        if (ob.IdTarea == this.tareaMain.IdTarea)
                        {

                            // Validar conflicto de horarios con el responsable
                            DateTime fechaInicio = this.cal_Inicio.SelectionStart;
                            DateTime fechaFinEstimada = this.cal_finEstimada.SelectionStart;
                            int idResponsable = Convert.ToInt32(cb_idResponsable.SelectedValue);


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
                                MessageBox.Show("El recurso no tiene suficiente cantidad disponibilidad.", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            // Validar existencia de tarea pendiente con mayor prioridad
                            Tareas tempTarea = new Tareas
                            {
                                FechaInicio = fechaInicio,
                                Prioridad = this.cb_Prioridad.SelectedItem.ToString()
                            };
                            string estado = this.cb_Estado.SelectedItem.ToString();
                            if (_conexion.ExisteTareaPendienteConMayorPrioridad(tempTarea) && estado == "Completado")
                            {
                                MessageBox.Show("Deben completarse las tareas con mayor prioridad.", "",
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
                            _conexion.ModificarTarea(temp);
                            this.Close();

                            // Mostrar el formulario de tareas
                            using (FrmTareas frm = new FrmTareas())
                            {
                                frm.ShowDialog();
                            }
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
                        Tareas tempTarea = new Tareas
                        {
                            FechaInicio = fechaInicio,
                            Prioridad = this.cb_Prioridad.SelectedItem.ToString()
                        };
                        string estado = this.cb_Estado.SelectedItem.ToString();

                        if (_conexion.ExisteTareaPendienteConMayorPrioridad(tempTarea) && estado == "Completado")
                        {
                            MessageBox.Show("Deben completarse las tareas con mayor prioridad.", "",
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

                        this.Close();

                        // Mostrar el formulario de tareas
                        using (FrmTareas frm = new FrmTareas())
                        {
                            frm.ShowDialog();
                        }
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
    }
}
