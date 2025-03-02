using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Tareas
    {
        [Required]
        public int IdTarea { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFinEstimada { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Prioridad { get; set; }
        [Required]
        public string Requiere{ get; set; }
        [Required]
        public int IdProyecto { get; set; }
        [Required]
        public int IdResponsable { get; set; }
        public int IdRecurso { get; set; }
        public int Cantidad { get; set; }







    }
}
