using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBR.Models
{
    public class Cita
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [DisplayName("Fecha de Inicio")]
        public String FechaInicio { set; get; }

        [DisplayName("Hora de Inicio")]
        public String HoraInicio { set; get; }

        [DisplayName("Fecha de Finalización")]
        public String FechaFinal { set; get; }

        [DisplayName("Hora de Finalizacion")]
        public String HoraFinal { set; get; }

        public Cliente Cliente { set; get; }

        public String cliente { set; get; }

        public virtual ICollection<Propiedad> Propiedades { get; set; }

    }
}
