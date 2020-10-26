using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class conveniosModel
    {
        [Display(Name = "Nemo")]
        [MaxLength(7)]
        public string Nemo { get; set; }

        [Display(Name = "diaini")]
        [MaxLength(2)]
        public string diaini { get; set; }

        [Display(Name = "Numdías")]
        public int? Numdías { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(20)]
        public string Descripcion { get; set; }

        [Display(Name = "emisor")]
        [MaxLength(4)]
        public int? emisor { get; set; }

        [Display(Name = "Nit")]
        [MaxLength(12)]
        public int Nit { get; set; }

        [Display(Name = "tiponegocio")]
        [MaxLength(4)]
        public int? tiponegocio { get; set; }

        [Display(Name = "tipotrans")]
        [MaxLength(4)]
        public short? tipotrans { get; set; }

        [Display(Name = "Servidor")]
        [MaxLength(30)]
        public string Servidor { get; set; }

        [Display(Name = "Usuario")]
        public int? Usuario { get; set; }

        [Display(Name = "Clave")]
        [MaxLength(30)]
        public string Clave { get; set; }

        [Display(Name = "CodConcepto")]
        [MaxLength(8)]
        public string CodConcepto { get; set; }

        [Display(Name = "bolsillo")]
        public int bolsillo { get; set; }
    }
}
