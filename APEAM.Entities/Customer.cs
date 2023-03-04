using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public class Customer: EntityBase
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; }

        [Display(Name = "RFC")]
        public string RFC { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo '{0}' es requerido."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Address { get; set; }

        [Display(Name = "Código postal")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string State { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Country { get; set; }
        
        [Display(Name = "Número de telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Lista de ventas")]
        public virtual IEnumerable<Sale> Sales { get; set; }

    }
}
