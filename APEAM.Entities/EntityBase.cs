﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APEAM.Entities
{
    public class EntityBase
    {
        public int ID { get; set; }

        [Display(Name = "Desactivado")]        
        public bool IsDisabled { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Fecha ultima actualización")]
        public DateTime? Uptime { get; set; }
    }
}
