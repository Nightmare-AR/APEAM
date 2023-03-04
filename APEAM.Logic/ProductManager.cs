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
    public class ProductManager
    {
        private readonly ApplicationDbContext db;

        public ProductManager(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public ReturnArgs DeleteByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var product = this.Get(id);
            if (product != null)
            {
                this.db.Products.Remove(product);

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
                args.Message = "El producto no se encuentra en el sistema.";
            }

            return args;
        }

        public ReturnArgs DisableByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var product = this.Get(id);
            if (product != null)
            {
                try
                {
                    product.IsDisabled = true;
                    this.db.Entry(product).State = EntityState.Modified;

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

        public Product Get(int id)
        {
            var product = this.db.Products.Find(id);
            return product;
        }

        public List<Product> GetAll(int? id, string search = "", string sort = "")
        {
            var products = this.db.Products.AsQueryable();

            products = products.Where(p => !p.IsDisabled);

            if (!string.IsNullOrEmpty(search))
                products = products.Where(GetFullTextExpression(search));

            switch (sort)
            {
                case "name_asc":
                    products = products.OrderBy(o => o.Name.TrimStart());
                    break;
                case "name_desc":
                    products = products.OrderByDescending(o => o.Name.TrimStart());
                    break;
                default:
                    products = products.OrderBy(o => o.Name.TrimStart());
                    break;
            }

            return products.ToList();
        }

        public ReturnArgs Save(Product product)
        {
            var args = new ReturnArgs();
            if (product.ID <= 0)
                this.db.Products.Add(product);
            else
                this.db.Entry(product).State = EntityState.Modified;

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

        private Expression<Func<Product, bool>> GetFullTextExpression(string text)
        {
            return c => c.Name.Contains(text); ;
        }
    }
}
