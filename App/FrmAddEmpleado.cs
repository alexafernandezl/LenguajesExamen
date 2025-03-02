using BLL;
using DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FrmAddEmpleado : Form
    {
        private Dictionary<string, string> codigosTelefonicos;
        private readonly Conexion _conexion;
        public Empleados empleadoMain;

        private FrmEmpleados frmEmpleados;
        public FrmAddEmpleado(int id, FrmEmpleados frmEmpleados)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            empleadoMain = _conexion.BuscarEmpleadoPorId(id);
            this.frmEmpleados = frmEmpleados;

            if (empleadoMain != null) { CargarDatos(); }
            CargarCodigosTelefonicos();
                
        }
        private void CargarDatos()
        {
            txt_Correo.Text = this.empleadoMain.Correo;
            txt_NombreCompleto.Text = this.empleadoMain.NombreCompleto;
            cb_rol.SelectedItem = this.empleadoMain.Rol;
            monthCalendar1.SetDate(this.empleadoMain.FechaNacimiento);
            pictureBox2.Image = Cargar_Imagen(this.empleadoMain.Foto);
            if (!string.IsNullOrEmpty(this.empleadoMain.Telefono))
            {
                int indiceEspacio = this.empleadoMain.Telefono.IndexOf(' ');

                if (indiceEspacio != -1)
                {
                    // Si hay un espacio, separar el código y el número
                    txt_Codigo_Pais.Text = this.empleadoMain.Telefono.Substring(0, indiceEspacio);
                    txt_telefono.Text = this.empleadoMain.Telefono.Substring(indiceEspacio + 1);
                }
                else
                {
                    // Si no hay espacio, mostrar un mensaje o manejar el caso
                    MessageBox.Show("El formato del teléfono no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void CargarCodigosTelefonicos()
        {
            codigosTelefonicos = new Dictionary<string, string>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://restcountries.com/v3.1/all";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JArray paises = JArray.Parse(responseBody);

                        foreach (var pais in paises)
                        {
                            string nombrePais = pais["name"]?["common"]?.ToString();
                            string codigoRaiz = pais["idd"]?["root"]?.ToString();
                            JArray sufijos = (JArray)pais["idd"]?["suffixes"];

                            if (!string.IsNullOrEmpty(nombrePais) && !string.IsNullOrEmpty(codigoRaiz) && sufijos != null && sufijos.Count > 0)
                            {
                                string codigoCompleto = codigoRaiz + sufijos[0];
                                codigosTelefonicos.Add(nombrePais, codigoCompleto);
                                cb_Paises.Items.Add(nombrePais);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los datos de la API.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}");
                }
            }
        }
        private void cb_Paises_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Paises.SelectedItem != null)
            {
                string paisSeleccionado = cb_Paises.SelectedItem.ToString();
                if (codigosTelefonicos.TryGetValue(paisSeleccionado, out string codigoTelefonico))
                {
                    txt_Codigo_Pais.Text = codigoTelefonico;
                }
            }
        }

        private void FrmAddEmpleado_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxDate = DateTime.Today;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (!EsCorreoValido(txt_Correo.Text))
                    {
                        MessageBox.Show("Correo electrónico no válido. Por favor, revisa el formato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (monthCalendar1.SelectionStart > DateTime.Today)
                    {
                        MessageBox.Show("La fecha de nacimiento no puede ser futura.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string telefonoCompleto = this.txt_Codigo_Pais.Text +" "+ this.txt_telefono.Text;

                    if (empleadoMain != null)
                    {
                        // Modo edición
                        Empleados empleadoEmail = _conexion.BuscarEmpleadoPorEmail(this.txt_Correo.Text);
                        Empleados empleadoTelefono = _conexion.BuscarEmpleadoPorTelefono(telefonoCompleto);

                        if (empleadoEmail != null && empleadoEmail.IdEmpleado != empleadoMain.IdEmpleado)
                        {
                            MessageBox.Show("El correo ya está en uso por otro empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (empleadoTelefono != null && empleadoTelefono.IdEmpleado != empleadoMain.IdEmpleado)
                        {
                            MessageBox.Show("El teléfono ya está en uso por otro empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Actualizar empleado
                        Empleados temp = new Empleados()
                        {
                            IdEmpleado = empleadoMain.IdEmpleado,
                            NombreCompleto = this.txt_NombreCompleto.Text,
                            Rol = this.cb_rol.SelectedItem.ToString(),
                            FechaNacimiento = this.monthCalendar1.SelectionStart,
                            Correo = this.txt_Correo.Text,
                            Telefono = telefonoCompleto,
                            Foto = GuardarImagen(pictureBox2.Image)
                        };

                        _conexion.ModificarEmpleado(temp);
                        MessageBox.Show("Empleado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmEmpleados.CargarDatos();

                        this.Close();

                    }
                    else
                    {
                        // Modo agregar nuevo empleado
                        Empleados empleadoEmail = _conexion.BuscarEmpleadoPorEmail(this.txt_Correo.Text);
                        Empleados empleadoTelefono = _conexion.BuscarEmpleadoPorTelefono(telefonoCompleto);

                        if (empleadoEmail != null || empleadoTelefono != null)
                        {
                            MessageBox.Show("Ya existe un empleado con ese correo o teléfono.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Guardar nuevo empleado
                        Empleados temp = new Empleados()
                        {
                            Correo = this.txt_Correo.Text,
                            NombreCompleto = this.txt_NombreCompleto.Text,
                            Telefono = telefonoCompleto,
                            Rol = this.cb_rol.SelectedItem.ToString(),
                            FechaNacimiento = this.monthCalendar1.SelectionStart,
                            Foto = GuardarImagen(pictureBox2.Image)
                        };

                        _conexion.GuardarEmpleado(temp);
                        MessageBox.Show("Empleado guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmEmpleados.CargarDatos();

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar los espacios vacios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(this.txt_Correo.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_NombreCompleto.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_telefono.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_Codigo_Pais.Text))
            {
                return false;
            }
            return true;
        }

        private void txt_Codigo_Pais_TextChanged(object sender, EventArgs e)
        {

        }


        private string GuardarImagen(Image imagen)
        {
            if (imagen == null)
            {
                return null;
            }
            try
            {
                string carpetaFotos = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotos");
                if (!System.IO.Directory.Exists(carpetaFotos)){
                    System.IO.Directory.CreateDirectory(carpetaFotos);
                }
                string ruta = System.IO.Path.Combine(carpetaFotos, $"{Guid.NewGuid()}.png");
                imagen.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);
                return ruta;
            } catch (Exception ex) { throw new Exception("Error al guardar la imagen: " + ex.Message, ex); }
        }

        private void btn_Cargar_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imágenes|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Seleccionar imagen";
                if (openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    try
                    {
                        Image imagenSeleccionada = Image.FromFile(openFileDialog.FileName);
                        Image imagenRedimensionada = Redimensionar_Y_Cortar_Circular(imagenSeleccionada, 86, 86);
                        pictureBox2.Image = imagenRedimensionada;        
                    }catch(Exception ex) { throw new Exception(ex.Message, ex); }
                }
            };
        }
        private Image Redimensionar_Y_Cortar_Circular(Image imagen, int x, int y) {
            Bitmap bitmapRedimensionado = new Bitmap(x, y);
            using (Graphics g = Graphics.FromImage(bitmapRedimensionado))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, x, y);
                    g.SetClip(path);

                    float ratioImagen = (float)imagen.Width / imagen.Height;
                    float ratioDestino = (float)y / x;

                    RectangleF destino;
                    if (ratioDestino < ratioImagen) {
                        float escala = (float)x / imagen.Height;
                        float nuevoX = imagen.Width * escala;
                        float offsetX = (nuevoX - x) / 2;
                        destino = new RectangleF(-offsetX, 0, nuevoX, y);
                    }
                    else
                    {
                        float escala = (float)x / imagen.Width;
                        float nuevoY = imagen.Height * escala;
                        float offsetX = (nuevoY - y) / 2;
                        destino = new RectangleF( 0, - offsetX, nuevoY, y);
                    }

                    g.DrawImage(imagen, destino);
                }
            }
            return bitmapRedimensionado;
        }

        private Image Cargar_Imagen(string rutaImagen)
        {
            if (!string.IsNullOrEmpty(rutaImagen) && System.IO.File.Exists(rutaImagen))
            {
                return Image.FromFile(rutaImagen);
            } 
            else {
                return global::App.Properties.Resources.icons8_male_user_50;
            }
        }

        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_Correo_TextChanged(object sender, EventArgs e)
        {
            if (EsCorreoValido(txt_Correo.Text))
            {
                txt_Correo.ForeColor = Color.Black; // Texto normal si es válido
            }
            else
            {
                txt_Correo.ForeColor = Color.Red; // Texto rojo si es inválido
            }
        }
        private bool EsCorreoValido(string correo)
        {
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patronCorreo);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}
