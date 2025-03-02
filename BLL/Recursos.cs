using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Recursos
    {
        [Required]
        public int IdRecurso { get; set; }
        [Required]
        public string NombreRecurso { get; set; }
        [Required]
        public int CantidadDisponible { get; set; }
        [Required]
        public decimal CostoUnidad { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public DateTime FechaAdquisicion { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Descripcion { get; set; }








    }
}
