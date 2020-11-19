using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class FestivoModel
    {
        [Key]
        [Display(Name = "FESTIVO")]
        [MaxLength(8)]
        public string FESTIVO { get; set; }

        [Display(Name = "DIAHABIL")]
        [MaxLength(8)]
        public string DIAHABIL { get; set; }
    }
}
