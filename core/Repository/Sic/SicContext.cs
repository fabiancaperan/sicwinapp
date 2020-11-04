using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Repository.Sic
{
    public class SicContext
    {
        public bool save(List<SapModel> lstSic)
        {
            using (var db = new CacheContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                if (db.sap.Any())
                    db.RemoveRange(db.sap);
                db.AddRange(lstSic);
                
                return db.SaveChanges() > 0;
            }
        }

        public List<SapModel> getAll()
        {
            using (var db = new CacheContext())
            {
                return db.sap.ToList();
            }
        }
    }
}
