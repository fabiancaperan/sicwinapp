using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class FestivoModel
    {
        [Key]
        [Display(Name = "FESTIVO")]
        [MaxLength(8)]
        public string FESTIVO { get; set; }

        [Display(Name = "DIAHABIL")]
        [MaxLength(8)]
        public string DIAHABIL { get; set; }
    }
}
