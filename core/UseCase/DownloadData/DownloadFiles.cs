using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Repository;
using core.Repository.Sic;
using core.Repository.Types;
using core.UseCase.Carrefour;
using core.UseCase.Comercios;
using core.UseCase.Exito;
using core.UseCase.Olimpica;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<EntidadesModel> lstEntidades = _db.EntidadesModel.ToList();
            List<SapModel> lstSap = new SicContext().getAll();
            foreach (var commerceType in commerceTypes)
            {
                
                Dictionary<string, List<CommerceModel>> filelst = new Dictionary<string, List<CommerceModel>>();
                switch (commerceType)
                {
                    case CommerceType.Comercios:
                        filelst = new GenerateComerciosFile().build(lstSap, lstEntidades);                        
                        break;
                    case CommerceType.Olimpica:
                        filelst = new GenerateOlimpicaFile().build(lstSap, lstEntidades);
                        break;
                    case CommerceType.Exito:
                        filelst = new GenerateExitoFile().build(lstSap, lstEntidades);
                        break;
                    case CommerceType.Cencosud:
                        filelst = new GenerateCarrefourFile().build(lstSap, lstEntidades);
                        break;
                }
                ComerciosFile(rute, lstSap, commerceType, filelst);
            }

            return true;
        }
        private bool ComerciosFile(string rute, List<SapModel> lstSap, CommerceType commerceType, Dictionary<string, List<CommerceModel>> filelst) 
        {
           
            foreach (KeyValuePair<string, List<CommerceModel>> item in filelst) 
            {
                string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
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
    }
}
