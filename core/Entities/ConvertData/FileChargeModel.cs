using System.Collections.Generic;

namespace core.Entities.ConvertData
{
    public class FileChargeModel
    {
        public string Message { get; set; }
        public bool IsTrue { get; set; }
        public List<SapModel> List { get; set; }
    }
}
