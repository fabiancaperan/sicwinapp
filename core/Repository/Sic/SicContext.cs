using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Repository.Sic
{

    public class SicContext
    {
        public bool Save(List<SapModel> lstSic , DateTime dat)
        {
            using var db = new CacheContext();
            // Create
            Console.WriteLine("Inserting a new blog");
            if (db.Sap.Any())
            {
                db.RemoveRange(db.Sap);
                db.RemoveRange(db.DateComp);
            }

            var dateComp = new DateCompModel();
            dateComp.Dat = dat;
            db.DateComp.Add(dateComp);
            db.Sap.AddRange(lstSic);
                
            return db.SaveChanges() > 0;
        }

        public bool Save( DateTime dat)
        {
            using var db = new CacheContext();
            // Create
            Console.WriteLine("Inserting a new blog");
            if (db.Sap.Any())
            {
                //db.RemoveRange(db.Sap);
                db.RemoveRange(db.DateComp);
            }

            var dateComp = new DateCompModel();
            dateComp.Dat = dat;
            db.DateComp.Add(dateComp);
            

            return db.SaveChanges() > 0;
        }

        public List<SapModel> GetAll()
        {
            using var db = new CacheContext();
            return db.Sap.ToList();
        }
        public DateTime? GetDate()
        {
            using var db = new CacheContext();
            return db.DateComp.FirstOrDefault()?.Dat;
        }
    }
}
