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
                CargarDatos();
        }
        private void CargarDatos()
        {
            txt_recurso.Text = this.tareaMain.IdRecurso+"";
            txt_idResponsable.Text=this.tareaMain.IdResponsable + "";
            txt_idProyecto.Text = this.tareaMain.IdProyecto + "";
            txt_Descripcion.Text = this.tareaMain.Descripcion;
            cb_Estado.SelectedItem = this.tareaMain.Estado;
            cal_Inicio .SetDate(this.tareaMain.FechaInicio);
            cal_finEstimada.SetDate(this.tareaMain.FechaFinEstimada);
            cb_Prioridad.SelectedItem = this.tareaMain.Prioridad;
            cb_requiere.SelectedItem = this.tareaMain.Requiere;
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(this.txt_recurso.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_idResponsable.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_idProyecto.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_Descripcion.Text))
            {
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
                            

                            Tareas temp = new Tareas();

                            temp.FechaInicio = (this.cal_Inicio.SelectionStart);
                            temp.FechaFinEstimada = (this.cal_finEstimada.SelectionStart);
                            temp.Descripcion = this.txt_Descripcion.Text;
                            temp.Estado = this.cb_Estado.SelectedItem.ToString();
                            temp.Prioridad = this.cb_Prioridad.SelectedItem.ToString();
                            temp.Requiere = this.cb_requiere.SelectedItem.ToString();
                            temp.IdProyecto = int.Parse(txt_idProyecto.Text);
                            temp.IdResponsable = int.Parse(this.txt_idResponsable.Text);
                            temp.IdRecurso = int.Parse(this.txt_recurso.Text);
                            temp.Cantidad = int.Parse(this.txt_cantidad.Text);
                            temp.IdTarea = ob.IdTarea;
                            _conexion.ModificarTarea(temp);
                            this.Close();
                            try
                            {
                                using (FrmTareas frm = new FrmTareas())
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
                            MessageBox.Show("No existe este recurso", "",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                        }
                    }
                }
                if (ValidarCampos())
                {
                    Tareas temp = new Tareas();
                    temp.FechaInicio = (this.cal_Inicio.SelectionStart);
                    temp.FechaFinEstimada = (this.cal_finEstimada.SelectionStart);
                    temp.Descripcion = this.txt_Descripcion.Text;
                    temp.Estado = this.cb_Estado.SelectedItem.ToString();
                    temp.Prioridad = this.cb_Prioridad.SelectedItem.ToString();
                    temp.Requiere = this.cb_requiere.SelectedItem.ToString();
                    temp.IdProyecto = int.Parse(txt_idProyecto.Text);
                    temp.IdResponsable = int.Parse(this.txt_idResponsable.Text);
                    temp.IdRecurso = int.Parse(this.txt_recurso.Text);
                    temp.Cantidad = int.Parse(this.txt_cantidad.Text);
                    _conexion.GuardarTarea(temp);
                    this.Close();
                    try
                    {
                        using (FrmTareas frm = new FrmTareas())
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
