using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;


namespace DAL
{
    public class Conexion
    {
        private string StringConexion;
        private SqlConnection _conexion;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public Conexion(string stringConexion)
        {
            StringConexion = stringConexion;
        }

        //---------------------------------------------------------------------
        //CRUD PROYECTOS
        //---------------------------------------------------------------------
        #region Proyectos
        public DataTable CargarProyectos()
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Proyectos]";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarProyecto(Proyectos proyecto)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Add_Proyecto]";

                _command.Parameters.AddWithValue("@NombreProyecto", proyecto.NombreProyecto);

                _command.Parameters.AddWithValue("@FechaDeInicio", proyecto.FechaDeInicio);

                _command.Parameters.AddWithValue("@FechaDeFinEstimada", proyecto.FechaDeFinEstimada);

                _command.Parameters.AddWithValue("@Estado", proyecto.Estado);

                _command.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);

                _command.Parameters.AddWithValue("@Presupuesto", proyecto.Presupuesto);
                _command.Parameters.AddWithValue("@IdResponsable", proyecto.IdResponsable);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin agregar proyecto

        public void ModificarProyecto(Proyectos proyecto)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Proyecto]";

                _command.Parameters.AddWithValue("@IdProyecto", proyecto.IdProyecto);
                _command.Parameters.AddWithValue("@NombreProyecto", proyecto.NombreProyecto);

                _command.Parameters.AddWithValue("@FechaDeInicio", proyecto.FechaDeInicio);

                _command.Parameters.AddWithValue("@FechaDeFinEstimada", proyecto.FechaDeFinEstimada);

                _command.Parameters.AddWithValue("@Estado", proyecto.Estado);

                _command.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);

                _command.Parameters.AddWithValue("@Presupuesto", proyecto.Presupuesto);
                _command.Parameters.AddWithValue("@IdResponsable", proyecto.IdResponsable);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Modificar proyecto

        public void EliminarProyecto(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Proyecto]";
                _command.Parameters.AddWithValue("@IdProyecto", id);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Eliminar Proyecto

        public Proyectos BuscarProyectoPorId(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Proyecto_Id]";
                _command.Parameters.AddWithValue("@IdProyecto", id);
                _reader = _command.ExecuteReader();
                Proyectos temp = null;
                if (_reader.Read())
                {
                    temp = new Proyectos();
                    temp.IdProyecto = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreProyecto = _reader.GetValue(1).ToString();
                    temp.FechaDeInicio = DateTime.Parse(_reader.GetValue(2).ToString());
                    temp.FechaDeFinEstimada = DateTime.Parse(_reader.GetValue(3).ToString());
                    temp.Estado = _reader.GetValue(4).ToString();
                    temp.Descripcion = _reader.GetValue(5).ToString();
                    temp.Presupuesto = decimal.Parse(_reader.GetValue(6).ToString());
                    temp.IdResponsable = int.Parse(_reader.GetValue(7).ToString());
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

        public DataTable BuscarProyectoPorNombre(string nombre)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;

                // Si el nombre está vacío, carga todos los empleados
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _command.CommandText = "[Sp_Proyectos]";
                }
                else
                {
                    _command.CommandText = "[Sp_Buscar_Proyecto_Nombre]";
                    _command.Parameters.AddWithValue("@NombreProyecto", nombre);
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);

                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();

                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool TodasTareasFinalizadas(int idProyecto)
        {
            try
            {
                using (_conexion = new SqlConnection(StringConexion))
                {
                    _conexion.Open();
                    using (_command = new SqlCommand("[Sp_Verificar_Tareas_Finalizadas]", _conexion))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@IdProyecto", idProyecto);

                        int tareasPendientes = (int)_command.ExecuteScalar(); // Retorna el número de tareas no finalizadas

                        return tareasPendientes == 0; // Si no hay tareas pendientes, devuelve true
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar tareas finalizadas: {ex.Message}");
            }
        }


        public void CancelarTareasAsociadas(int idProyecto)
        {
            try
            {
                using (_conexion = new SqlConnection(StringConexion))
                {
                    _conexion.Open();
                    using (_command = new SqlCommand("[Sp_Cancelar_Tareas_Proyecto]", _conexion))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@IdProyecto", idProyecto);
                        _command.ExecuteNonQuery(); // Ejecuta la actualización en la base de datos
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cancelar tareas del proyecto: {ex.Message}");
            }
        }


        public void LiberarRecursos(int idProyecto)
        {
            try
            {
                using (_conexion = new SqlConnection(StringConexion))
                {
                    _conexion.Open();
                    using (_command = new SqlCommand("[Sp_Liberar_Recursos_Proyecto]", _conexion))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@IdProyecto", idProyecto);
                        _command.ExecuteNonQuery(); // Actualiza los recursos 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al liberar recursos del proyecto: {ex.Message}");
            }
        }

        public decimal CalcularCostoTotalRecursos(int idProyecto)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand("[Sp_Calcular_Costo_Recursos_Proyecto]", _conexion);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddWithValue("@IdProyecto", idProyecto);

                SqlParameter outputParam = new SqlParameter("@CostoTotal", SqlDbType.Decimal);
                outputParam.Direction = ParameterDirection.Output;
                _command.Parameters.Add(outputParam);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

                return (outputParam.Value != DBNull.Value) ? Convert.ToDecimal(outputParam.Value) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el costo total de los recursos: " + ex.Message);
            }
        }


        public bool VerificarDisponibilidadEmpleado(int idEmpleado, DateTime fechaInicio, DateTime fechaFin, int idProyecto )
        {
            try
            {
                using (_conexion = new SqlConnection(StringConexion))
                {
                    _conexion.Open();
                    using (_command = new SqlCommand("[Sp_Verificar_Asignacion_Empleado]", _conexion))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        _command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        _command.Parameters.AddWithValue("@FechaFin", fechaFin);
                        _command.Parameters.AddWithValue("@IdProyecto", idProyecto);

                        int count = Convert.ToInt32(_command.ExecuteScalar());
                        return count > 0; // Retorna true si hay solapamiento
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar solapamiento de empleado en proyectos: " + ex.Message);
            }
        }



        #endregion
        //---------------------------------------------------------------------
        //CRUD Empleados
        //---------------------------------------------------------------------
        #region Empleados
        public void GuardarEmpleado(Empleados obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Add_Empleados]";
                _command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                _command.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                _command.Parameters.AddWithValue("@Correo", obj.Correo);
                _command.Parameters.AddWithValue("@Telefono", obj.Telefono);
                _command.Parameters.AddWithValue("@Foto", obj.Foto);
                _command.Parameters.AddWithValue("@Rol", obj.Rol);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin agregar empleado
        public void ModificarEmpleado(Empleados obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Empleado]";

                _command.Parameters.AddWithValue("@IdEmpleado", obj.IdEmpleado);
                _command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                _command.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                _command.Parameters.AddWithValue("@Foto", obj.Foto);
                _command.Parameters.AddWithValue("@Rol", obj.Rol);

                _command.Parameters.AddWithValue("@Correo", obj.Correo);
                _command.Parameters.AddWithValue("@Telefono", obj.Telefono);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Modificar Empleado

        public bool EliminarEmpleado(int id)
        {
            try
            {
                using (_conexion = new SqlConnection(StringConexion))
                {
                    _conexion.Open();
                    using (_command = new SqlCommand("[Sp_Del_Empleado]", _conexion))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@IdEmpleado", id);

                        int filasAfectadas = _command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("No se encontró ningún empleado con ese ID.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Código 547: Restricción de clave foránea
                {
                    throw new Exception("No se puede eliminar el empleado porque tiene proyectos asignados. Debe reasignar o eliminar esos proyectos primero.");
                }
                else
                {
                    throw new Exception($"Error de base de datos: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
        //Fin Eliminar Empleado

        public Empleados BuscarEmpleadoPorId(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Empleado_Id]";
                _command.Parameters.AddWithValue("@IdEmpleado", id);
                _reader = _command.ExecuteReader();
                Empleados temp = null;
                if (_reader.Read())
                {
                    temp = new Empleados();
                    temp.IdEmpleado = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreCompleto = _reader.GetValue(1).ToString();
                    temp.FechaNacimiento = DateTime.Parse(_reader.GetValue(2).ToString());
                    temp.Correo = _reader.GetValue(3).ToString();
                    temp.Telefono = _reader.GetValue(4).ToString();
                    temp.Foto = _reader.GetValue(5).ToString();
                    temp.Rol = _reader.GetValue(6).ToString();
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin de Buscar Empleado por nombre

        public Empleados BuscarEmpleadoPorEmail(string correo)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Empleado_Email]";
                _command.Parameters.AddWithValue("@Email", correo);
                _reader = _command.ExecuteReader();
                Empleados temp = null;
                if (_reader.Read())
                {
                    temp = new Empleados();
                    temp.IdEmpleado =int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreCompleto=_reader.GetValue(1).ToString();
                    temp.FechaNacimiento = DateTime.Parse(_reader.GetValue(2).ToString());
                    temp.Correo = _reader.GetValue(3).ToString();
                    temp.Telefono = _reader.GetValue(4).ToString();
                    temp.Rol = _reader.GetValue(6).ToString();
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Empleados BuscarEmpleadoPorTelefono(string telefono)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Empleado_Telefono]";
                _command.Parameters.AddWithValue("@telefono", telefono);
                _reader = _command.ExecuteReader();
                Empleados temp = null;
                if (_reader.Read())
                {
                    temp = new Empleados();
                    temp.IdEmpleado = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreCompleto = _reader.GetValue(1).ToString();
                    temp.FechaNacimiento = DateTime.Parse(_reader.GetValue(2).ToString());
                    temp.Correo = _reader.GetValue(3).ToString();
                    temp.Telefono = _reader.GetValue(4).ToString();
                    temp.Rol = _reader.GetValue(6).ToString();
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BuscarEmpleadoNombre(string nombre)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;

                // Si el nombre está vacío, carga todos los empleados
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _command.CommandText = "[Sp_Empleados]";
                }
                else
                {
                    _command.CommandText = "[Sp_Buscar_Empleado_Nombre]";
                    _command.Parameters.AddWithValue("@NombreCompleto", nombre);
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);

                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();

                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable CargarEmpleados()
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Empleado]";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //---------------------------------------------------------------------
        //CRUD Recursos
        //---------------------------------------------------------------------
        #region Recursos
        public DataTable CargarRecursos()
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Recursos]";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarRecurso(Recursos obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Add_Recurso]";
                _command.Parameters.AddWithValue("@NombreRecurso", obj.NombreRecurso);
                _command.Parameters.AddWithValue("@CantidadDisponible", obj.CantidadDisponible);
                _command.Parameters.AddWithValue("@CostoUnidad", obj.CostoUnidad);
                _command.Parameters.AddWithValue("@Tipo", obj.Tipo);
                _command.Parameters.AddWithValue("@FechaAdquisicion", obj.FechaAdquisicion);
                _command.Parameters.AddWithValue("@Estado", obj.Estado);
                _command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin agregar Recurso
        public void ModificarRecurso(Recursos obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Recurso]";
                _command.Parameters.AddWithValue("@IdRecurso", obj.IdRecurso);
                _command.Parameters.AddWithValue("@NombreRecurso", obj.NombreRecurso);
                _command.Parameters.AddWithValue("@CantidadDisponible", obj.CantidadDisponible);
                _command.Parameters.AddWithValue("@CostoUnidad", obj.CostoUnidad);
                _command.Parameters.AddWithValue("@Tipo", obj.Tipo);
                _command.Parameters.AddWithValue("@FechaAdquisicion", obj.FechaAdquisicion);
                _command.Parameters.AddWithValue("@Estado", obj.Estado);
                _command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Modificar Recurso

        public void EliminarRecurso(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Recurso]";
                _command.Parameters.AddWithValue("@IdRecurso", id);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Eliminar Recurso

        public Recursos BuscarRecursoPorId(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Recurso_Id]";
                _command.Parameters.AddWithValue("@IdRecurso", id);
                _reader = _command.ExecuteReader();
                Recursos temp = null;
                if (_reader.Read())
                {
                    temp = new Recursos();
                    temp.IdRecurso = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreRecurso = _reader.GetValue(1).ToString();
                    temp.CantidadDisponible = int.Parse(_reader.GetValue(2).ToString());
                    temp.CostoUnidad = Decimal.Parse(_reader.GetValue(3).ToString());
                    temp.Tipo = _reader.GetValue(4).ToString();
                    temp.FechaAdquisicion = DateTime.Parse(_reader.GetValue(5).ToString());
                    temp.Estado = _reader.GetValue(6).ToString();
                    temp.Descripcion = _reader.GetValue(7).ToString();
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin de Buscar Recurso por id

        #endregion
        //---------------------------------------------------------------------
        //CRUD Tareas
        //---------------------------------------------------------------------
        #region Tareas
       
        public DataTable CargarTareas()
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Tareas]";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable datos = new DataTable();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarTarea(Tareas obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Add_Tarea]";
                _command.Parameters.AddWithValue("@FechaIncio", obj.FechaInicio);
                _command.Parameters.AddWithValue("@FechaFinEstimada", obj.FechaFinEstimada);
                _command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                _command.Parameters.AddWithValue("@Estado", obj.Estado);
                _command.Parameters.AddWithValue("@Prioridad", obj.Prioridad);
                _command.Parameters.AddWithValue("@Requiere", obj.Requiere);
                _command.Parameters.AddWithValue("@IdProyecto", obj.IdProyecto);
                _command.Parameters.AddWithValue("@IdResponsable", obj.IdResponsable);
                _command.Parameters.AddWithValue("@IdRecurso", obj.IdRecurso);
                _command.Parameters.AddWithValue("@Cantidad", obj.Cantidad);
                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin agregar Tarea
        public void ModificarTarea(Tareas obj)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Tarea]";
                _command.Parameters.AddWithValue("@IdTarea", obj.IdTarea);
                _command.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                _command.Parameters.AddWithValue("@FechaFinEstimada", obj.FechaFinEstimada);
                _command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                _command.Parameters.AddWithValue("@Estado", obj.Estado);
                _command.Parameters.AddWithValue("@Prioridad", obj.Prioridad);
                _command.Parameters.AddWithValue("@Requiere", obj.Requiere);
                _command.Parameters.AddWithValue("@IdProyecto", obj.IdProyecto);
                _command.Parameters.AddWithValue("@IdResponsable", obj.IdResponsable);
                _command.Parameters.AddWithValue("@IdRecurso", obj.IdRecurso);
                _command.Parameters.AddWithValue("@Cantidad", obj.Cantidad);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Modificar Tarea

        public void EliminarTarea(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Tarea]";
                _command.Parameters.AddWithValue("@IdTarea", id);

                _command.ExecuteNonQuery();
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin Eliminar Tarea

        public Tareas BuscarTareaPorId(int id)
        {
            try
            {
                _conexion = new SqlConnection(StringConexion);
                _conexion.Open();
                _command = new SqlCommand();
                _command.Connection = _conexion;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Tarea_Id]";
                _command.Parameters.AddWithValue("@IdTarea", id);
                _reader = _command.ExecuteReader();
                Tareas temp = null;
                if (_reader.Read())
                {
                    temp = new Tareas();
                    temp.IdTarea = int.Parse(_reader.GetValue(0).ToString());
                    temp.FechaInicio = DateTime.Parse(_reader.GetValue(1).ToString());
                    temp.FechaFinEstimada = DateTime.Parse(_reader.GetValue(2).ToString());
                    temp.Descripcion = _reader.GetValue(3).ToString();
                    temp.Estado = _reader.GetValue(4).ToString();
                    temp.Prioridad = _reader.GetValue(5).ToString();
                    temp.Requiere = _reader.GetValue(6).ToString();
                    temp.IdProyecto = int.Parse(_reader.GetValue(7).ToString());
                    temp.IdResponsable = int.Parse(_reader.GetValue(8).ToString());
                    temp.IdRecurso = int.Parse(_reader.GetValue(9).ToString());
                    temp.Cantidad = int.Parse(_reader.GetValue(10).ToString());
                }
                _conexion.Close();
                _conexion.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin de Buscar Tarea por id
        #endregion
    }

}
