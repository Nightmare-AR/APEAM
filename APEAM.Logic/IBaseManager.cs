using APEAM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Logic
{
    internal interface IBaseManager<T>
    {
        /// <summary>
        /// Delete object of <see cref="T"/> by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReturnArgs DeleteByID(int id);
        ReturnArgs DisableByID(int id);
        T Get(int id);
        List<T> GetAll(int? id, string search = "", string sort = "");
        ReturnArgs Save(T obj);
    }
}
