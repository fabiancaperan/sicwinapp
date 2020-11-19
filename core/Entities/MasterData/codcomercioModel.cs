using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class CodcomercioModel
    {
        [Display(Name = "CodRetailer")]
        [MaxLength(19)]
        public string CodRetailer { get; set; }

        [Display(Name = "Dependencia")]
        [MaxLength(4)]
        public int Dependencia { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(50)]
        public string Descripcion { get; set; }

        [Display(Name = "Nit")]
        [MaxLength(4)]
        public int? Nit { get; set; }
    }
}
