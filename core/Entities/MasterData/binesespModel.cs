using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace core.Entities.MasterData
{
    public class binesespModel
    {
        [Display(Name = "Fiid")]
        [MaxLength(4)]
        public int Fiid { get; set; }

        [Display(Name = "NombreTar")]
        [MaxLength(50)]
        public string NombreTar { get; set; }
    }
}
