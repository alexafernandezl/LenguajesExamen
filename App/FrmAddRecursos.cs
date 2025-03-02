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
        private Recursos recursosMain;
        private readonly Conexion _conexion;
        public FrmAddRecursos(int id)
        {
            InitializeComponent();
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
            recursosMain = _conexion.BuscarRecursoPorId(id);
            if (recursosMain != null)
                CargarDatos();
        }
        private void CargarDatos()
        {
            txt_nombreRecurso.Text = this.recursosMain.NombreRecurso;
            txt_cantidadDisponible.Text = this.recursosMain.CantidadDisponible + "";
            txt_costoUnidad.Text = this.recursosMain.CostoUnidad + "";
            cb_tipoRecurso.SelectedItem = this.recursosMain.Tipo;
            cal_fechaAdquisicion.SetDate(this.recursosMain.FechaAdquisicion);
            cb_EstadoRecurso.SelectedItem = this.recursosMain.Estado;
            txt_Descripcion.Text = this.recursosMain.Descripcion;
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(this.txt_nombreRecurso.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_cantidadDisponible.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_costoUnidad.Text))
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
                if (recursosMain != null)
                {
                    if (ValidarCampos())
                    {
                        Recursos ob = _conexion.BuscarRecursoPorId(recursosMain.IdRecurso);

                        if (ob.IdRecurso == this.recursosMain.IdRecurso)
                        {

                            Recursos temp = new Recursos();
                            temp.NombreRecurso = this.txt_nombreRecurso.Text;
                            temp.CantidadDisponible = int.Parse(txt_cantidadDisponible.Text);
                            temp.CostoUnidad = Decimal.Parse(txt_costoUnidad.Text);
                            temp.Estado = "activo";
                            temp.Tipo = cb_tipoRecurso.SelectedItem.ToString();
                            temp.FechaAdquisicion = (this.cal_fechaAdquisicion.SelectionStart);
                            temp.Estado = this.cb_EstadoRecurso.SelectedItem.ToString();
                            temp.Descripcion = this.txt_Descripcion.Text;
                            temp.IdRecurso = ob.IdRecurso;
                            _conexion.ModificarRecurso(temp);
                            this.Close();
                            try
                            {
                                using (FrmRecursos frm = new FrmRecursos())
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
                    Recursos temp = new Recursos();
                    temp.NombreRecurso = this.txt_nombreRecurso.Text;
                    temp.CantidadDisponible = int.Parse(txt_cantidadDisponible.Text);
                    temp.CostoUnidad = int.Parse(txt_costoUnidad.Text);
                    temp.Estado = "activo";
                    temp.Tipo = cb_tipoRecurso.SelectedItem.ToString();
                    temp.FechaAdquisicion = (this.cal_fechaAdquisicion.SelectionStart);
                    temp.Estado = this.cb_EstadoRecurso.SelectedItem.ToString();
                    temp.Descripcion = this.txt_Descripcion.Text;
                    _conexion.GuardarRecurso(temp);
                    this.Close();
                    try
                    {
                        using (FrmRecursos frm = new FrmRecursos())
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

        private void incioLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
