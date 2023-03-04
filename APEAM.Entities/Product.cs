using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public class Product: EntityBase
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
