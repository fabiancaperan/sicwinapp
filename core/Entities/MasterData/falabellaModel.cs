using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class FalabellaModel
    {
        [Key]
        [Display(Name = "CODIGO_UNICO")]
        [MaxLength(10)]
        public string CODIGO_UNICO { get; set; }

        [Display(Name = "LOCAL_FALABELLA")]
        [MaxLength(4)]
        public string LOCAL_FALABELLA { get; set; }

        [Display(Name = "NOMBRE_LOCAL")]
        [MaxLength(30)]
        public string NOMBRE_LOCAL { get; set; }
    }
}
