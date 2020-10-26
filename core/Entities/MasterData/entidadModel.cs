using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class entidadModel
    {
        [Display(Name = "fiids")]
        [MaxLength(4)]
        public string fiids { get; set; }

        [Display(Name = "nombres")]
        [MaxLength(25)]
        public string nombres { get; set; }
    }
}
