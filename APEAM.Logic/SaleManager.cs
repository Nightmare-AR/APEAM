using APEAM.DataAccess;
using APEAM.Entities;
using APEAM.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Logic
{
    public class SaleManager
    {
        private readonly ApplicationDbContext db;

        public SaleManager(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public ReturnArgs DeleteByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var sale = this.Get(id);
            if (sale != null)
            {
                this.db.Sales.Remove(sale);

                try
                {
                    this.db.SaveChanges();
                    args.Success = true;
                }
                catch (Exception ex)
                {
                    args.Success = false;
                    args.Message = ex.Message;
                }
            }
            else
            {
                args.Success = false;
                args.Message = "La venta no se encuentra en el sistema.";
            }

            return args;
        }

        public ReturnArgs DisableByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var sale = this.Get(id);
            if (sale != null)
            {
                try
                {
                    sale.IsDisabled = true;
                    this.db.Entry(sale).State = EntityState.Modified;

                    this.db.SaveChanges();
                    args.Success = true;
                }
                catch (Exception ex)
                {
                    args.Success = false;
                    args.Message = ex.Message;
                }
            }
            else
            {
                args.Success = false;
                args.Message = "El cliente no se encuentra en el sistema.";
            }

            return args;
        }

        public Sale Get(int id)
        {
            var sale = this.db.Sales.Find(id);
            return sale;
        }

        public List<Sale> GetAll(int? id, string search = "", string sort = "")
        {
            var sales = this.db.Sales.AsQueryable();

            sales = sales.Where(p => !p.IsDisabled);

            if (!string.IsNullOrEmpty(search))
                sales = sales.Where(GetFullTextExpression(search));

            switch (sort)
            {
                case "timestamp_asc":
                    sales = sales.OrderBy(o => o.TimeStamp);
                    break;
                case "timestamp_desc":
                    sales = sales.OrderByDescending(o => o.TimeStamp);
                    break;
                default:
                    sales = sales.OrderBy(o => o.TimeStamp);
                    break;
            }

            return sales.ToList();
        }

        public ReturnArgs Save(Sale sale)
        {
            var args = new ReturnArgs();
            if (sale.ID <= 0)
                this.db.Sales.Add(sale);
            else
                this.db.Entry(sale).State = EntityState.Modified;

            try
            {
                this.db.SaveChanges();

                args.Success = true;
            }
            catch (Exception ex)
            {
                args.Success = false;
                args.Message = ex.Message;
            }

            return args;
        }

        private Expression<Func<Sale, bool>> GetFullTextExpression(string text)
        {
            return c => c.Customer.Name.Contains(text);
                        
        }
    }
}
