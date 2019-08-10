using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBR.Models
{
    public class Cita
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public DateTime FechaInicio { set; get; }
        public DateTime FechaFinal { set; get; }

        public Cliente Cliente { set; get; }

        public virtual ICollection<Propiedad> Propiedades { get; set; }

    }
}
