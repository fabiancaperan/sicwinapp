﻿using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
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
