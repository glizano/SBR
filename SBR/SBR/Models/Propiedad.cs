using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name ="Nombre Propietario")]
        public string NombrePropietario { set; get; }
        [Display(Name = "Contacto Propietario")]
        public string ContactoPropietario { set; get; }
        public string Tipo { set; get; }
        public string Categoria { set; get; }
        public string Provincia { set; get; }

        [NotMapped]
        public virtual List<string> imagenes { set; get; } = new List<string>();

        public virtual ICollection<Caracteristica> Caracteristicas { get; set; }
    }
}
