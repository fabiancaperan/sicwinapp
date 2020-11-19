using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class EstablecimientosSic4Model
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

        [Display(Name = "Formato")]
        [MaxLength(20)]
        public string formato { get; set; }
    }
}
