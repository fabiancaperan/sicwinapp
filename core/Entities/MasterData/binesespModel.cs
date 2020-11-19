using System.ComponentModel.DataAnnotations;


namespace core.Entities.MasterData
{
    public class BinesespModel
    {
        [Display(Name = "Fiid")]
        [MaxLength(4)]
        [Key]
        public string Fiid { get; set; }

        [Display(Name = "NombreTar")]
        [MaxLength(50)]
        public string NombreTar { get; set; }
    }
}
