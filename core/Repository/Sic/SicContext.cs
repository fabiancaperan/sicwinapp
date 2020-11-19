using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Repository.Sic
{

    public class SicContext
    {
        public bool Save(List<SapModel> lstSic)
        {
            using var db = new CacheContext();
            // Create
            Console.WriteLine("Inserting a new blog");
            if (db.Sap.Any())
                db.RemoveRange(db.Sap);
            db.AddRange(lstSic);
                
            return db.SaveChanges() > 0;
        }

        public List<SapModel> GetAll()
        {
            using var db = new CacheContext();
            return db.Sap.ToList();
        }
    }
}
