using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class falabellaModel
    {
        [Display(Name = "codigounico")]
        [MaxLength(10)]
        public int codigounico { get; set; }

        [Display(Name = "localfalabella")]
        [MaxLength(4)]
        public int? localfalabella { get; set; }

        [Display(Name = "nombrelocal")]
        [MaxLength(30)]
        public string nombrelocal { get; set; }
    }
}
