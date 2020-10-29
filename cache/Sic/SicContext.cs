using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Text;

namespace cache.Sic
{
    public class SicContext
    {
        public bool save(List<SapModel> lstSic)
        {
            using (var db = new ServiceContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                db.AddRange(lstSic);
                //db.Add(new { Url = "http://blogs.msdn.com/adonet" });
                return db.SaveChanges() > 0;
            }
        }
    }
}
