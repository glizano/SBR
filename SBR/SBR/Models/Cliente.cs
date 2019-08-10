using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBR.Models
{
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Contacto { set; get; }

        public virtual ICollection<Caracteristica> Intereses { get; set; }
    }
}
