using System.ComponentModel.DataAnnotations;

namespace core.Entities.MasterData
{
    public class CorreosModel
    {
        [Display(Name = "Archivo")]
        [MaxLength(255)]
        public string Archivo { get; set; }

        [Display(Name = "Email")]
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
