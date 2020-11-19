using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class ConveniosModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nemo")]
        [MaxLength(7)]
        public string Nemo { get; set; }

        [Display(Name = "diaini")]
        [MaxLength(2)]
        public string diaini { get; set; }

        [Display(Name = "Numdías")]
        public string Numdías { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(20)]
        public string Descripcion { get; set; }

        [Display(Name = "emisor")]
        [MaxLength(4)]
        public string emisor { get; set; }

        [Display(Name = "Nit")]
        [MaxLength(12)]
        public string Nit { get; set; }

        [Display(Name = "tiponegocio")]
        [MaxLength(4)]
        public string tiponegocio { get; set; }

        [Display(Name = "tipotrans")]
        [MaxLength(4)]
        public string tipotrans { get; set; }

        [Display(Name = "Servidor")]
        [MaxLength(30)]
        public string Servidor { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Clave")]
        [MaxLength(30)]
        public string Clave { get; set; }

        [Display(Name = "CodConcepto")]
        [MaxLength(8)]
        public string CodConcepto { get; set; }

        [Display(Name = "bolsillo")]
        public string bolsillo { get; set; }
    }
}
