using APEAM.DataAccess;
using APEAM.Entities;
using APEAM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Logic
{
    public class CustomerManager : BaseManager
    {
        private readonly ApplicationDbContext db;

        public CustomerManager(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public ReturnArgs DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public ReturnArgs DisableByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get<Customer>(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll<Customer>(int? id, bool hideDisabled = false, string search = "")
        {
            throw new NotImplementedException();
        }

        public ReturnArgs Save<Customer>(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
