using System;
using System.Collections.Generic;
using System.Text;

namespace core.Entities.ComerciosData
{
    public class CommerceModel
    {
        public string Cod_RTL { get; set; }
        public string Nit { get; set; }
        public string NombreCadena { get; set; }
        public string Line { get; set; }
        public string Rtl { get; set; }

        public List<StringBuilder> lst { get; set; }
    }


}
