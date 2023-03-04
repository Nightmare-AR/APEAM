using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public class Sale: EntityBase
    {

        public Sale()
        {
            ItemLists = new List<ItemList>();
        }

        [Display(Name = "Folio")]
        public string Folio { get; set; }

        [Display(Name ="Número de articulos")]
        public int NumberArticles { 
            get { 
                return ItemLists.Count();
            } 
        }

        [Display(Name = "Precio neto")]
        public double NetPrice { 
            get {
                var ivaPrice = BrutePrice * (IVA / 100);
                return BrutePrice + ivaPrice;
            } 
        }

        [Display(Name = "Precio bruto")]
        public double BrutePrice { 
            get {
                var price = ItemLists.Sum(it => it.TotalPrice);

                return price;
            }
        }

        [Display(Name = "IVA")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public float IVA { get; set; }

        [Display(Name = "Fecha de venta")]
        public DateTime SaleDate { get; set; }
 
        [Display(Name = "ID del Cliente")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Lista de productos")]
        public virtual List<ItemList> ItemLists { get; set; } 
    }
}
