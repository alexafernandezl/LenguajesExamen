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
    public partial class FrmAddRecursos : Form
    {
        private Recursos recursosMain; // Variable para acceder a los recursos de la aplicación principal
        private readonly Conexion _conexion; // Conexión a la base de datos (solo se asigna al inicio).

        private FrmRecursos frmRecursos; // Formulario de recursos

        public FrmAddRecursos(int id, FrmRecursos frmRecursos)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);  // Crea una conexión a la base de datos usando la cadena de conexión de la configuración.
            recursosMain = _conexion.BuscarRecursoPorId(id); // Busca un recurso en la base de datos por su ID y lo asigna a 'recursosMain'.
            this.frmRecursos = frmRecursos; // Asigna el formulario de recursos a la variable 'frmRecursos'.

            // Si el recurso se encuentra, carga los datos en el formulario.
            if (recursosMain != null)
                CargarDatos();

        }//Fin de FrmAddRecursos

        // El método 'CargarDatos' se encarga de llenar los controles del formulario con la información
        // del recurso que está almacenada en el objeto 'recursosMain'. Esto permite que el usuario vea o
        // edite los datos del recurso, mostrando valores como nombre, cantidad disponible, costo, tipo,
        // estado, fecha de adquisición y descripción en los campos correspondientes.
        private void CargarDatos()
        {
            txt_nombreRecurso.Text = this.recursosMain.NombreRecurso;
            txt_cantidadDisponible.Text = this.recursosMain.CantidadDisponible + "";
            txt_costoUnidad.Text = this.recursosMain.CostoUnidad + "";
            cb_tipoRecurso.SelectedItem = this.recursosMain.Tipo;
            cal_fechaAdquisicion.SetDate(this.recursosMain.FechaAdquisicion);
            cb_EstadoRecurso.SelectedItem = this.recursosMain.Estado;
            txt_Descripcion.Text = this.recursosMain.Descripcion;

        }//Fin de CargarDatos

        // Valida los campos del formulario y muestra mensajes de error si es necesario.
        private bool ValidarCampos()
        {
            List<string> errores = new List<string>(); // Lista para almacenar mensajes de error

            // Verificar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(txt_nombreRecurso.Text)||
                string.IsNullOrWhiteSpace(txt_cantidadDisponible.Text)||
                string.IsNullOrWhiteSpace(txt_costoUnidad.Text)||
                cb_tipoRecurso.SelectedItem == null||
                cb_EstadoRecurso.SelectedItem == null)
            {
                errores.Add("Por favor, complete todos los campos del formulario.");
            }

            // Validar que la cantidad disponible sea un número entero
            if (!int.TryParse(txt_cantidadDisponible.Text, out _))
            {
                errores.Add("La cantidad disponible debe ser un número entero.");
            }

            // Validar que el costo por unidad sea un número decimal válido
            if (!decimal.TryParse(txt_costoUnidad.Text, out _))
            {
                errores.Add("El costo por unidad debe ser un número válido.");
            }

            // Si hay errores, mostrarlos en un solo mensaje y retornar false
            if (errores.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errores), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;

        }//Fin ValidarCampos
        // Crea un objeto Recurso con los datos del formulario
        private Recursos CrearRecurso()
        {
            Recursos temp = new Recursos(); // Crear un nuevo objeto Recurso
            temp.NombreRecurso = this.txt_nombreRecurso.Text; // Asignar el nombre del recurso según el TextBox


            int cantidad; // Declaración de una variable entera para almacenar la cantidad disponible.

            // Intenta convertir el texto del cuadro de texto a un entero.
            // Si falla, muestra un mensaje de error y retorna null.
            if (!int.TryParse(txt_cantidadDisponible.Text, out cantidad))
            {
                MessageBox.Show("La cantidad disponible no es un número válido.");
                return null;  // Retorna null si la validación falla
            }

            // Asigna la cantidad válida al objeto 'temp'.
            temp.CantidadDisponible = cantidad;

            // Declaración de una variable decimal para almacenar el costo por unidad.
            decimal costoUnidad;

            // Intenta convertir el texto del cuadro de texto a un decimal.
            // Si falla, muestra un mensaje de error y retorna null.
            if (!decimal.TryParse(txt_costoUnidad.Text, out costoUnidad))
            {
                MessageBox.Show("El costo de la unidad no es un número válido.");
                return null;  // Retorna null si la validación falla
            }

            // Asigna el costo válido al objeto 'temp'.
            temp.CostoUnidad = costoUnidad;

            // Asigna el estado seleccionado en el ComboBox al objeto 'temp'.
            temp.Estado = this.cb_EstadoRecurso.SelectedItem.ToString();

            // Asigna el tipo seleccionado en el ComboBox al objeto 'temp'
            temp.Tipo = cb_tipoRecurso.SelectedItem.ToString();

            // Asigna la fecha seleccionada en el calendario al objeto 'temp'.
            temp.FechaAdquisicion = this.cal_fechaAdquisicion.SelectionStart;

            // Asigna el texto de la descripción al objeto 'temp'.
            temp.Descripcion = this.txt_Descripcion.Text;

            // Devuelve el objeto 'temp' con todos los datos asignados.
            return temp;

        }//Fin CrearRecurso


        //Funcionalidad del boton guardar
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de campos
                if (ValidarCampos())
                {
                    // Crear objeto Recurso
                    Recursos temp = CrearRecurso();
                    if (temp == null)
                    {
                        // Si la creación del recurso falló debido a entradas incorrectas, no continuamos
                        return;
                    }

                    if (recursosMain != null)
                    {
                        // Si el recurso principal no es nulo, intentar modificar el recurso existente
                        temp.IdRecurso = recursosMain.IdRecurso; // ASIGNAR EL ID ORIGINAL

                        // Busca un recurso en la base de datos usando el ID del recurso actual
                        Recursos ob = _conexion.BuscarRecursoPorId(recursosMain.IdRecurso);

                        // Verifica si el recurso encontrado tiene el mismo ID que el recurso actual
                        if (ob.IdRecurso == this.recursosMain.IdRecurso)
                        {
                            // Si el recurso existe, lo modifica en la base de datos.
                            _conexion.ModificarRecurso(temp);
                            // Muestra un mensaje de éxito al usuario.
                            MessageBox.Show("Recurso modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Si no se encuentra el recurso, mostrar mensaje de error
                            MessageBox.Show("No existe este recurso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        // Si recursosMain es nulo, guardar un nuevo recurso
                        _conexion.GuardarRecurso(temp);

                        MessageBox.Show("Recurso guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Cerrar el formulario actual y abrir el formulario de recursos
                    this.Close();


                    // Crea una nueva instancia del formulario de recursos.


                    // Muestra el formulario de recursos como un cuadro de diálogo modal.
                    // Esto significa que no se podrá interactuar con otras ventanas de la aplicación 
                    // hasta que se cierre esta ventana.
                    frmRecursos.CargarDatos();
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si algo falla
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//Fin btn_Guardar_Click


        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario actual

        }//Fin btn_Cancelar_Click

        private void cal_fechaAdquisicion_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Verifica si la fecha seleccionada es mayor a la fecha actual
            if (e.Start > DateTime.Today)
            {
                // Muestra un mensaje de advertencia si la fecha es futura.
                MessageBox.Show("No se pueden seleccionar fechas futuras.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Restaura la fecha del calendario a la fecha actual
                cal_fechaAdquisicion.SetDate(DateTime.Today);
            }

        }//Fin cal_fechaAdquisicion_DateSelected


        private void txt_cantidadDisponible_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y teclas de control (como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancela el evento
                MessageBox.Show("Por favor, ingrese solo números enteros.", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }//Fin txt_cantidadDisponible_KeyPress

        private void txt_costoUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, el punto decimal y teclas de control
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Por favor, ingrese un número válido.", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Evitar múltiples puntos decimales
            if (e.KeyChar == '.' && txt_costoUnidad.Text.Contains("."))
            {
                e.Handled = true;
            }

        }//Fin txt_costoUnidad_KeyPress


    }//Fin de clase

}//Fin de namespace