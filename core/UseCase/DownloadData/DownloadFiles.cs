using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Repository;
using core.Repository.Sic;
using core.Repository.Types;
using core.UseCase.Carrefour;
using core.UseCase.Comercios;
using core.UseCase.Olimpica;
using System;
using System.Collections.Generic;
using System.Data;
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
        public bool build(string rute, List<CommerceType> commerceTypes)
        {
            validateInitDatabase();

            foreach (var commerceType in commerceTypes)
            {
                
                List<SapModel> lstSap = new SicContext().getAll();

                if (commerceType == CommerceType.Comercios)
                {
                        return ComerciosFile(rute, lstSap);
                }
                else
                {
                    List<string> filelst = getDataFile(commerceType, lstSap);

                    if (filelst != null)
                    {
                        return GenericFile(rute, commerceType, filelst);
                    }
                }
                

            }

            return true;
        }
        private bool ComerciosFile(string rute, List<SapModel> lstSap) 
        {
            List<EntidadesModel> lstEntidades = _db.entidades.ToList();
            var filelst = new GenerateComerciosFile().build(lstSap, lstEntidades);
            //FileStream stream;
            foreach (KeyValuePair<string, List<CommerceModel>> item in filelst) 
            {
                string path = Path.Combine(rute, CommerceType.Comercios.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.Value.FirstOrDefault().Cod_RTL);
                using (
                    FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        item.Value.ForEach(s => writer.WriteLine(s.line));
                    }
                }
            }
                
            return true;
        }

        private bool GenericFile(string rute, CommerceType commerceType, List<string> filelst) 
        {
            string path = Path.Combine(rute, commerceType.ToString());
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "Olimpica");

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                filelst.ForEach(s => writer.WriteLine(s));
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

        private List<string> getDataFile(CommerceType item, List<SapModel> lstSap)
        {
            List<string> filelst;
            List<EntidadesModel> lstEntidades = _db.entidades.ToList();
            switch (item)
            {
                case CommerceType.Cencosud:
                    filelst = new GenerateCarrefourFile().buildstring(lstSap);
                    break;
                case CommerceType.Olimpica:
                    filelst = new GenerateOlimpicaFile().build(lstSap, lstEntidades);
                    break;
                //case CommerceType.Comercios:
                    
                //    filelst = new GenerateComerciosFile().build(lstSap, lstEntidades);
                //    break;
                default:
                    filelst = null;
                    break;
            }

            return filelst;
        }
    }
}
