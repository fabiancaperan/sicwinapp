using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class RedprivadasModel
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
