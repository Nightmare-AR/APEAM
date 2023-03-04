using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public  class ItemList: EntityBase
    {
        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion del producto")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Description { get; set; }

        [Display(Name = "Precio del producto")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public double Price { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Quantity { get; set; }

        [Display(Name = "Precio Total")]        
        public double TotalPrice { 
            get { 
            return Quantity * Price;
            }
        }

        [Display(Name = "ID del producto")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int ProductId { get; set; }

        [Display(Name = "ID de la venta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int SaleId { get; set; }

        [Display(Name = "Producto")]
        public virtual Product Product { get; set; }

        [Display(Name = "Venta")]
        public virtual Sale Sale { get; set; }
    }
}
