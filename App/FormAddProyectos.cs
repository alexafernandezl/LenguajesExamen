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
        // Constructor de la clase que recibe el ID del proyecto
        public FormAddProyectos(int id, FrmProyectos frmProyectos)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            proyectoMain = _conexion.BuscarProyectoPorId(id);// Valida si el form tiene un id para saber si debe insertar o modificar
            CargarResponsables(); // Carga la lista de empleados en el ComboBox
            if (proyectoMain != null)
                CargarDatos(); // Carga los datos del proyecto si existe

            txt_Presupuesto.KeyPress += ValidarEntradaDecimal;//No se puede insertar letras 
            cal_Inicio.SetDate(DateTime.Today.AddDays(0));//La fecha inicia en el dia de hoy
            cal_finEstimada.SetDate(DateTime.Today.AddDays(365));//La fecha finaliza un annio despues   
            cal_Inicio.DateChanged += ValidarFechas;//Para validad que sea menor
            cal_finEstimada.DateChanged += ValidarFechas;//Para validad que sea mayor
            this.frmProyectos = frmProyectos;

        }

        // Método para cargar los datos del proyecto en los campos del formulario
        private void CargarDatos()
        {
            txt_NombreProyecto.Text = proyectoMain.NombreProyecto;
            txt_Presupuesto.Text = proyectoMain.Presupuesto.ToString("N2", CultureInfo.InvariantCulture);//Muestra solo 2 digitos
            cb_Estado.SelectedItem = proyectoMain.Estado;
            txt_Descripcion.Text = proyectoMain.Descripcion;
            cbResponsable.SelectedValue = proyectoMain.IdResponsable;
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
            cbResponsable.DisplayMember = "NombreCompleto";//muestra el nombre
            cbResponsable.ValueMember = "IdEmpleado";//almacena id
            cbResponsable.SelectedIndex = 0;
        }

        // Validación para asegurar que la fecha de inicio no sea posterior a la fecha de fin en tiempo real
        private void ValidarFechas(object sender, EventArgs e)
        {
            if (cal_Inicio.SelectionStart > cal_finEstimada.SelectionStart)
            {

                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin estimada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Debe seleccionar un estado para el proyecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbResponsable.SelectedIndex == 0 || cbResponsable.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Debe seleccionar un responsable para el proyecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Validación para permitir solo números y punto decimal en el campo Presupuesto
        private void ValidarEntradaDecimal(object sender, KeyPressEventArgs e)//Bloquea campos 
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        // Método para guardar o modificar un proyecto
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())//Valida si los campos no estan vacios
                    return;

                Proyectos temp = new Proyectos()//Almacena en na variable termporal los datos 
                {
                    NombreProyecto = txt_NombreProyecto.Text,
                    Presupuesto = decimal.Parse(txt_Presupuesto.Text, CultureInfo.InvariantCulture),
                    FechaDeFinEstimada = cal_finEstimada.SelectionStart,
                    FechaDeInicio = cal_Inicio.SelectionStart,
                    Estado = cb_Estado.SelectedItem.ToString(),
                    Descripcion = txt_Descripcion.Text,
                    IdResponsable = Convert.ToInt32(cbResponsable.SelectedValue),
                };

                if (proyectoMain != null)//Si el from tiene un id, se modifica
                {
                    temp.IdProyecto = proyectoMain.IdProyecto;
                    _conexion.ModificarProyecto(temp);
                    MessageBox.Show("Empleado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else//Si el form no tiene id se crea
                {
                    _conexion.GuardarProyecto(temp);
                    MessageBox.Show("Empleado creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                frmProyectos.CargarDatos();
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para cerrar el formulario sin guardar cambios
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
