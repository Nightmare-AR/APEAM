using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Helpers
{
    public class ReturnArgs
    {
        /// <summary>
        /// Obtiene true si se ejecuto con exito y false si no
        /// </summary>
        public bool Success { get; set; } 

        /// <summary>
        /// Obtiene el mensaje de error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Asigna u obtiene un resultado
        /// </summary>
        public virtual object Result { get; set; }
    }
}
