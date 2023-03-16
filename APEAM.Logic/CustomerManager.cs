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
    public class CustomerManager:IBaseManager<Customer>
    {
        private readonly ApplicationDbContext db;

        public CustomerManager(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public ReturnArgs DeleteByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var customer = this.Get(id);
            if (customer != null)
            {
                this.db.Customers.Remove(customer);

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
                args.Message = "El cliente no se encuentra en el sistema.";
            }

            return args;
        }

        public ReturnArgs DisableByID(int id)
        {
            ReturnArgs args = new ReturnArgs();
            var customer = this.Get(id);
            if (customer != null)
            {
                try
                {
                    customer.IsDisabled = true;
                    this.db.Entry(customer).State = EntityState.Modified;
                    
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

        public Customer Get(int id)
        {
            var customer = this.db.Customers.Find(id);
            return customer;
        }

        public List<Customer> GetAll(int? id, string search = "", string sort = "")
        {
            var customers = this.db.Customers.AsQueryable();

            customers = customers.Where(c => !c.IsDisabled);

            if (!string.IsNullOrEmpty(search))
                customers = customers.Where(GetFullTextExpression(search));

            switch (sort)
            {
                case "name_asc":
                    customers = customers.OrderBy(o => o.Name.TrimStart());
                    break;
                case "name_desc":
                    customers = customers.OrderByDescending(o => o.Name.TrimStart());
                    break;              
                default:
                    customers = customers.OrderBy(o => o.Name.TrimStart());
                    break;
            }

            return customers.ToList();
        }

        public ReturnArgs Save(Customer customer)
        {
            var args = new ReturnArgs();
            if (customer.ID <= 0)
                this.db.Customers.Add(customer);
            else            
                this.db.Entry(customer).State = EntityState.Modified;            

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

        private Expression<Func<Customer, bool>> GetFullTextExpression(string text)
        {
            return c => c.Name.Contains(text) ||                      
                        c.City.Contains(text) ||
                        c.State.Contains(text) ||
                        c.Country.Contains(text) ||
                        c.ZipCode.Contains(text) ||
                        c.RFC.Contains(text) ||
                        c.Email.Contains(text) ||
                        c.Phone.Contains(text);
        }

    }
}
