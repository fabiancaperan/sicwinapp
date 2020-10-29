using core.Entities.ConvertData;
using core.Repository.Sic;
using core.UseCase.Carrefour;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public Boolean build(string path) {
            var lst =new SicContext().getAll();
            if (validateFormat(path)) {
                List<SapModel> sapModels=new ConvertFileTextToSapModel().build(path);
            }
            lst = new SicContext().getAll();
            new GenerateCarrefourFile().buildstring(lst);
            return true;
        }

        private Boolean validateFormat(string path) {
            //validar extension .txt
            return true;
        }
    }
}
