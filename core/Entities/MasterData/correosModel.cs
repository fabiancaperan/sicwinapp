using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.MasterData
{
    public class correosModel
    {
        [Display(Name = "Archivo")]
        [MaxLength(255)]
        public string Archivo { get; set; }

        [Display(Name = "Email")]
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
