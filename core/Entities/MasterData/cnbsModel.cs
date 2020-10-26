using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class cnbsModel
    {
        [Display(Name = "CodigoUnico")]
        [MaxLength(10)]
        public int CodigoUnico { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
