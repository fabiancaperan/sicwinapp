using System.Collections.Generic;
using System.Text;

namespace core.Entities.ComerciosData
{
    public class CommerceModel
    {
        public string CodRtl { get; set; }
        public string Nit { get; set; }
        public string NombreCadena { get; set; }
        public string Line { get; set; }
        public string FinalLine { get; set; }
        public string Rtl { get; set; }

        public List<StringBuilder> Lst { get; set; }
    }


}
