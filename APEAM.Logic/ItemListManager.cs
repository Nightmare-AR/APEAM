using APEAM.DataAccess;
using APEAM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Logic
{
    public class ItemListManager
    {
        private readonly ApplicationDbContext db;

        public ItemListManager(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }
        public ReturnArgs Delete(int id)
        {
            ReturnArgs args = new ReturnArgs();            

            try
            {
                this.db.ItemLists.Remove(this.db.ItemLists.Find(id));
                args.Success = true;
            }
            catch (Exception ex)
            {
                args.Success = false;
                args.Message = ex.Message;
            }

            return args;
        }
    }
}
