using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class codcomercioModel
    {
        [Display(Name = "CodRetailer")]
        [MaxLength(19)]
        public string CodRetailer { get; set; }

        [Display(Name = "Dependencia")]
        [MaxLength(4)]
        public int Dependencia { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(50)]
        public string Descripcion { get; set; }

        [Display(Name = "Nit")]
        [MaxLength(4)]
        public int? Nit { get; set; }
    }
}
