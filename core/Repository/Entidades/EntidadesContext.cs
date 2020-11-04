using core.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.Repository.Entidades
{
    public class EntidadesContext
    {
        public bool save(List<EntidadesModel> lstEntidades)
        {
            using (var db = new dbContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                if (db.EntidadesModel.Any())
                    db.RemoveRange(db.EntidadesModel);
                db.AddRange(lstEntidades);

                return db.SaveChanges() > 0;
            }
        }

        public List<EntidadesModel> getAll()
        {
            using (var db = new CacheContext())
            {
                return db.entidades.ToList();
            }
        }
    }
}
