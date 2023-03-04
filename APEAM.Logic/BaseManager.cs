using APEAM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Logic
{
    internal interface BaseManager
    {

        /// <summary>
        /// Insert or Update Object
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        ReturnArgs Save<T>(T entity);

        /// <summary>
        /// Get object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(int id);

        /// <summary>
        /// Get list of object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<T> GetAll<T>(int? id, bool hideDisabled = false, string search = "");


        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReturnArgs DeleteByID(int id);

        /// <summary>
        /// Disable object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReturnArgs DisableByID(int id);
    }
}
