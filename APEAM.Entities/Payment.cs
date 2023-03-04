using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public class Payment : EntityBase
    {
        [Display(Name = "Número de la tarjeta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido."), DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Display(Name = "Mes exp")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string MonthExp { get; set; }
        [Display(Name = "Año exp")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string YearExp { get; set; }
        [Display(Name = "Año exp")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string CVV { get; set; }
        [Display(Name = "Nombre del tarjetahabiente")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string NameCard { get; set; }

        [Display(Name = "ID de la venta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int SaleId { get; set; }

        [Display(Name = "Venta")]
        public virtual Sale Sale { get; set; }
    }
}
