using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class establecimientos_sic4Model
    {
        [Display(Name = "nombre")]
        [MaxLength(30)]
        public string nombre { get; set; }

        [Display(Name = "nit")]
        [MaxLength(15)]
        public int nit { get; set; }

        [Display(Name = "ciudad")]
        [MaxLength(20)]
        public string ciudad { get; set; }

        [Display(Name = "email")]
        [MaxLength(255)]
        public string email { get; set; }

        [Display(Name = "formato")]
        [MaxLength(20)]
        public string formato { get; set; }
    }
}
