using core.Entities.ConvertData;
using core.Repository.Sic;
using System.Collections.Generic;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public bool Build(string path) {
            if (ValidateFormat(path)) {
                List<SapModel> sapModels=new ConvertFileTextToSapModel().Build(path);
                new SicContext().Save(sapModels);
            }
            
            return true;
        }

        private bool ValidateFormat(string path) {
            return true;
        }
    }
}
