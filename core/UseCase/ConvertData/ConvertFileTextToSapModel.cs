﻿using System;
using core.Entities.ConvertData;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using core.Repository;

namespace core.UseCase.ConvertData
{
    public class ConvertFileTextToSapModel
    {
        const int LengthLine = 369;
        //private const string noLength = "El archivo tiene líneas que no estan entre 360 y 371 caracteres";
        private const string NoLength = "El archivo tiene líneas que son vacías o no tienen el formato válido";
        private const string NodatesValid = "El archivo tiene campos de fecha con formato no válido";
        //private readonly List<string> _lstDates = new List<string>(){ "FechaCompra", "FechaTran" }; 

        public FileChargeModel Build(string[] lines, DateTime dateOut)
        {
            using var db = new CacheContext();
            if (db.Sap.Any())
            {
                db.RemoveRange(db.Sap);
                db.RemoveRange(db.DateComp);
            }
            var dateComp = new DateCompModel();
            dateComp.Dat = dateOut;
            db.DateComp.Add(dateComp);
            var ret = new FileChargeModel();
            
            foreach (var t in lines)
            {
                var le = t.Length;
                if (le > 373 || le < 360)
                {
                    ret.Message = NoLength;
                    break;
                }

                var sa = BuildSap(t);
                if (sa == null)
                {
                    ret.Message = NodatesValid;
                    break;
                }

                db.Sap.Add(sa);
            }

            db.SaveChanges();
            db.Dispose();
            return ret;
        }

        private SapModel BuildSap(string line)
        {
            var dif = 0;
            if (line.Length != LengthLine)
            {
                dif = LengthLine - line.Length;

            }

            SapModel sapModel = new SapModel();
            var propertyInfos = sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (PropertyInfo prop in propertyInfos)
            {
                
                var length = dif != 0 && prop.Name == "NombreCadena" ? GetMaxLength(prop) - dif : GetMaxLength(prop);
                prop.SetValue(sapModel, line.Substring(i, length), null);
                i += length;
            }
            return sapModel;
        }

        private int GetMaxLength(PropertyInfo propInfo)
        {
            MaxLengthAttribute attrMaxLength = (MaxLengthAttribute)propInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
            return attrMaxLength?.Length ?? 0;
        }
    }
}
