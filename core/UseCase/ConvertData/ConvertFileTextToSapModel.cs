﻿using System;
using System.Collections.Generic;
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
            InitDb(db);
            var dateComp = new DateCompModel();
            dateComp.Dat = dateOut;
            db.DateComp.Add(dateComp);
            var ret = new FileChargeModel { List = new List<SapModel>() };

            //var lst = new List<List<SapModel>>();
            //var numbatch = 50000;
            int i = 0;
            foreach (var t in lines)
            {
                var le = t.Length;
                if (le > 373 || le < 360)
                {
                    ret.Message = NoLength;
                    break;
                }

                i++;
                var sa = BuildSap(t, i);
                if (sa == null)
                {
                    ret.Message = NodatesValid;
                    break;
                }
                ret.List.Add(sa);
                //if ((i % numbatch) == 0)
                //{
                //    lst.Add(ret.List);
                //    ret.List = new List<SapModel>();
                //}
                //ret.List.Add(sa);
                //db.Sap.Add(sa);
               
                
            }
            //var bulkConfig = new BulkConfig()
            //{
            //    SetOutputIdentity = true,
            //    PreserveInsertOrder = true
            //};
            //db.BulkInsert(ret.List, bulkConfig);
            //lst.ForEach(s =>
            //{
            //    db.BulkInsert(s, bulkConfig);
            //    //db.Sap.AddRange(s);
            //    //db.SaveChanges();
            //});
            //db.Sap.AddRange(ret.List);


            //db.Dispose();
            return ret;
        }

        private void InitDb(CacheContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();


            if (db.Sap.Any())
            {

                db.RemoveRange(db.Sap);
                db.RemoveRange(db.DateComp);
            }

        }

        private SapModel BuildSap(string line, int id)
        {
            var dif = 0;
            if (line.Length != LengthLine)
            {
                dif = LengthLine - line.Length;

            }

            //SapModel sapModel = new SapModel();
            SapModel sapModel = new SapModel { Id = id };
            var propertyInfos = sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (PropertyInfo prop in propertyInfos)
            {
                if (prop.Name == "Id")
                    continue;
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
