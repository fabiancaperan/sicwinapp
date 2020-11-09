using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class redprivadasModel
    {
        [Key]
        [Display(Name = "red")]
        [MaxLength(10)]
        public string red { get; set; }

        [Display(Name = "nombre")]
        [MaxLength(50)]
        public string nombre { get; set; }
    }
}
