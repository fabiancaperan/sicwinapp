using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public Boolean build(string path) {
            if (validateFormat(path)) {
                List<SapModel> sapModels=new ConvertFileTextToSapModel().build(path);
            }
            //Validar Path formato
            // leer archivo
            //convertir archivo
            //guardar archivo

            return true;
        }

        private Boolean validateFormat(string path) {
            //validar extension .txt
            return true;
        }
    }
}
