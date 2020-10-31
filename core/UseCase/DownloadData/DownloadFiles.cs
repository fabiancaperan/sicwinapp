using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Repository;
using core.Repository.Sic;
using core.UseCase.Carrefour;
using core.UseCase.Comercios;
using core.UseCase.Olimpica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace core.UseCase.DownloadData
{
    public class DownloadFiles
    {
        private readonly dbContext _db;
        public DownloadFiles()
        {
            _db = new dbContext();
        }
        public bool build(string rute, List<Repository.Types.CommerceType> commerceTypes)
        {
            validateInitDatabase();

            foreach (var commerceType in commerceTypes)
            {
                string pathString = Path.Combine(rute, commerceType.ToString());
                Directory.CreateDirectory(pathString);
                pathString = Path.Combine(pathString, commerceType.ToString());
                List<SapModel> lstSap = new SicContext().getAll();

                List<string> filelst = getDataFile(commerceType, lstSap);
                if (filelst != null)
                {
                    FileStream stream = new FileStream(pathString, FileMode.OpenOrCreate);
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        filelst.ForEach(s => writer.WriteLine(s));
                    }

                }

            }

            return true;
        }

        private void validateInitDatabase()
        {
            if (!_db.entidades.Any())
            {
                new InitDatabase.InitDb().initDatabase();
            }
        }

        private List<string> getDataFile(Repository.Types.CommerceType item, List<SapModel> lstSap)
        {
            List<string> filelst;
            List<EntidadesModel> lstEntidades = _db.entidades.ToList();
            switch (item)
            {
                case Repository.Types.CommerceType.Cencosud:
                    filelst = new GenerateCarrefourFile().buildstring(lstSap);
                    break;
                case Repository.Types.CommerceType.Olimpica:
                    filelst = new GenerateOlimpicaFile().build(lstSap, lstEntidades);
                    break;
                case Repository.Types.CommerceType.Comercios:
                    
                    filelst = new GenerateComerciosFile().build(lstSap, lstEntidades);
                    break;
                default:
                    filelst = null;
                    break;
            }

            return filelst;
        }
    }
}
