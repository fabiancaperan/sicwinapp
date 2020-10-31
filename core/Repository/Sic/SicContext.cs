using core.Entities.ConvertData;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
