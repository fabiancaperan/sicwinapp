using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class EntidadModel
    {
        [Display(Name = "fiids")]
        [MaxLength(4)]
        public string fiids { get; set; }

        [Display(Name = "nombres")]
        [MaxLength(25)]
        public string nombres { get; set; }
    }
}
