﻿using core.Entities.ConvertData;
using core.Repository.Sic;
using core.UseCase.Carrefour;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public bool build(string path) {
            var lst =new SicContext().getAll();
            if (validateFormat(path)) {
                List<SapModel> sapModels=new ConvertFileTextToSapModel().build(path);
                new SicContext().save(sapModels);
            }
            
            return true;
        }

        private bool validateFormat(string path) {
            //validar extension .txt
            return true;
        }
    }
}
