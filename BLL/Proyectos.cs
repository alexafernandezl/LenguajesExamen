using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Proyectos
    {
        [Required]
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        [Required]
        public DateTime FechaDeInicio{ get; set; }
        [Required]
        public DateTime FechaDeFinEstimada { get; set; }
        [Required]
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public decimal Presupuesto { get; set; }
        [Required]
        public int IdResponsable { get; set; }





    }
}
