using System;
using System.Collections.Generic;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public bool build(string path) {
            if (validateFormat(path))
            {
                // leer archivo
                //convertir archivo
                //guardar archivo
            }
     
          
            return true;
        }

        private bool validateFormat(string path) {
            //validar extension .txt
            return true;
        }
    }
}
