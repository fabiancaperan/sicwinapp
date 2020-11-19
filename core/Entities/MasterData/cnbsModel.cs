﻿using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class CnbsModel
    {
        [Key]
        [Display(Name = "CODIGO_UNICO")]
        [MaxLength(10)]
        public string CODIGO_UNICO { get; set; }

        [Display(Name = "NOMBRE")]
        [MaxLength(50)]
        public string NOMBRE { get; set; }
    }
}
