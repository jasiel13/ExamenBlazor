using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenBlazor.Shared.Entidades
{
    public class Meta
    {
        public int MetaId { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }
        public DateTime? FechadeCreacion { get; set; }        
        public virtual List<Tarea> ListadeTareas { get; set; }
    }
    public class Tarea
    {
        public int TareaId { get; set; }

        [MaxLength(80)]
        public string Nombre { get; set; }
        public bool Es_Completada { get; set; }
        public bool Es_Importante { get; set; }
        public DateTime? FechadeCreacion { get; set; }    

        //propiedades de navegacion       
        public int MetaId { get; set; }
        public virtual Meta Meta { get; set; }
    }
}
