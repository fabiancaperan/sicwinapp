using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class codconceptoModel
    {
        [Display(Name = "CodConcepto")]
        [MaxLength(8)]
        public string CodConcepto { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(50)]
        public string Descripcion { get; set; }
    }
}
