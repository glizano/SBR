using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBR.Models
{
    public class Propiedad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Titulo { set; get; }
        public double Precio { set; get; }
        public string Moneda { set; get; } 
        public string Descripcion { set; get; }
        public string NombrePropietario { set; get; }
        public string ContactoPropietario { set; get; }

        public virtual ICollection<Caracteristica> Caracteristicas { get; set; }
    }
}
