using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class EntidadesModel
    {
        [Display(Name = "fiid")]
        [MaxLength(4)]
        public string fiid { get; set; }

        [Display(Name = "nombre")]
        [MaxLength(25)]
        public string nombre { get; set; }

        [Display(Name = "nit")]
        [MaxLength(25)]
        public int? nit { get; set; }
    }
}
