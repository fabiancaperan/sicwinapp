using core.Entities.ConvertData;
using core.Repository.Sic;
using System.Collections.Generic;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public bool build(string path) {
            var lst =new SicContext().GetAll();
            if (validateFormat(path)) {
                List<SapModel> sapModels=new ConvertFileTextToSapModel().Build(path);
                new SicContext().Save(sapModels);
            }
            
            return true;
        }

        private bool validateFormat(string path) {
            return true;
        }
    }
}
