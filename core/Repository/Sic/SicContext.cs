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
            using (var db = new ServiceContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                if (db.sicModels.Any())
                    db.RemoveRange(db.sicModels);
                db.AddRange(lstSic);
                
                return db.SaveChanges() > 0;
            }
        }

        public List<SapModel> getAll()
        {
            using (var db = new ServiceContext())
            {
                return db.sicModels.ToList();
            }
        }
    }
}
