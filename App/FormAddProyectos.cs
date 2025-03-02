using BLL;
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
    public partial class FormAddProyectos: Form
    {
        private readonly Conexion _conexion;
        public Proyectos proyectoMain;
        public FormAddProyectos(int id)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            proyectoMain = _conexion.BuscarProyectoPorId(id);
            if (proyectoMain != null)
                CargarDatos();
        }
        
        private void CargarDatos()
         {
            txt_idResponsable.Text = this.proyectoMain.IdResponsable+"";
            txt_NombreProyecto.Text = this.proyectoMain.NombreProyecto;
            txt_Presupuesto.Text = this.proyectoMain.Presupuesto+"";
            cb_Estado.SelectedItem = this.proyectoMain.Estado;
            cal_Inicio.SetDate(this.proyectoMain.FechaDeInicio);
            cal_finEstimada.SetDate(this.proyectoMain.FechaDeFinEstimada);
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(this.txt_idResponsable.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_NombreProyecto.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_Presupuesto.Text))
            {
                return false;
            }
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (proyectoMain != null)
                {
                    if (ValidarCampos())
                    {
                        Proyectos proyecto = _conexion.BuscarProyectoPorNombre(this.txt_NombreProyecto.Text);

                        if (proyecto.IdProyecto == this.proyectoMain.IdProyecto)
                        {

                            Proyectos temp = new Proyectos();
                            temp.NombreProyecto = this.txt_NombreProyecto.Text;
                            temp.FechaDeFinEstimada = this.cal_finEstimada.SelectionStart;
                            temp.FechaDeInicio = this.cal_Inicio.SelectionStart;
                            temp.Estado = "activo";
                            temp.Descripcion = this.txt_Descripcion.Text;
                            temp.IdResponsable =int.Parse( this.txt_idResponsable.Text);
                            temp.IdProyecto = this.proyectoMain.IdProyecto;
                            _conexion.ModificarProyecto(temp);
                            this.Close();
                            try
                            {
                                using (FrmProyectos frm = new FrmProyectos())
                                {
                                    frm.ShowDialog();
                                }
                                ;
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            MessageBox.Show("El usuario no tiene este correo", "",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);

                        }
                    }
                }
                if (ValidarCampos())
                {
                        Proyectos temp = new Proyectos();
                        temp.NombreProyecto = this.txt_NombreProyecto.Text;
                        temp.FechaDeFinEstimada = this.cal_finEstimada.SelectionStart;
                        temp.FechaDeInicio = this.cal_Inicio.SelectionStart;
                        temp.Estado = "activo";
                        temp.Descripcion = this.txt_Descripcion.Text;
                        temp.IdResponsable = int.Parse(this.txt_idResponsable.Text);
                        _conexion.GuardarProyecto(temp);
                        this.Close();
                        try
                        {
                            using (FrmProyectos frm = new FrmProyectos())
                            {
                                frm.ShowDialog();
                            }
                            ;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                }
                else
                {
                    MessageBox.Show("Hola");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
