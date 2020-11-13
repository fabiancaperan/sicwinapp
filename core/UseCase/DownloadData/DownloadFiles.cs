using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Repository;
using core.Repository.Sic;
using core.Repository.Types;
using core.UseCase.Carrefour;
using core.UseCase.Cnb;
using core.UseCase.CnbSpecial;
using core.UseCase.Comercios;
using core.UseCase.Exito;
using core.UseCase.Falabella;
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
            List<falabellaModel> lstFalabella =  _db.falabellaModel.ToList();
            List<conveniosModel> lstConv = _db.conveniosModel.ToList();
            List<cnbsModel> lstCnb = _db.cnbsModel.ToList();
            List<SapModel> lstSap = new SicContext().getAll();
            foreach (var commerceType in commerceTypes)
            {

                Dictionary<string, List<CommerceModel>> filelst = new Dictionary<string, List<CommerceModel>>();
                List<CommerceModel> res = new List<CommerceModel>();
                switch (commerceType)
                {
                    case CommerceType.Comercios:
                        res = new GenerateComerciosFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.Falabella:
                        res = new GenerateFalabellaFile().build(lstSap, lstFalabella);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.Olimpica:
                        res = new GenerateOlimpicaFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.Exito:
                        res = new GenerateExitoFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.ExitoConciliacion:
                        res = new GenerateConcilationFile().build(lstSap, lstConv);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.Cnb:
                        res = new GenerateCnb().build(lstSap, lstEntidades, lstCnb);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.CnbSpecial:
                        res = new GenerateCnbSpecial().build(lstSap, lstEntidades, lstCnb);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, lstSap, commerceType, res);
                        break;
                    case CommerceType.Cencosud:
                        res = new GenerateCarrefourFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, lstSap, commerceType, res);
                        break;
                }
                //ComerciosFiles(rute, lstSap, commerceType, res);
                //ComerciosFile(rute, lstSap, commerceType, filelst);
            }

            return true;
        }
        private bool ComerciosFile(string rute, List<SapModel> lstSap, CommerceType commerceType, Dictionary<string, List<CommerceModel>> filelst)
        {

            foreach (KeyValuePair<string, List<CommerceModel>> item in filelst)
            {
                //string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                string path = Path.Combine(rute, item.Value.FirstOrDefault().Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.Value.FirstOrDefault().Cod_RTL);
                using (
                    FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        item.Value.ForEach(s => writer.WriteLine(s.Line));
                    }
                }
            }

            return true;
        }

        private bool ComerciosFiles(string rute, List<SapModel> lstSap, CommerceType commerceType, List<CommerceModel> filelst)
        {

            foreach (CommerceModel item in filelst)
            {
                //string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.Cod_RTL);
                using (
                    FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        //title
                        if(item.Line.Trim()!=string.Empty)
                        writer.WriteLine(item.Line);
                        //data
                        item.lst.ForEach(s => writer.WriteLine(s));
                    }
                }
            }

            return true;
        }

        private bool ComerciosFileRtc(string rute, List<SapModel> lstSap, CommerceType commerceType, List<CommerceModel> filelst)
        {

            foreach (CommerceModel item in filelst)
            {
                //string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.Cod_RTL);
                using (
                    FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        //title
                        if (item.Line.Trim() != string.Empty)
                            writer.WriteLine(item.Line);
                        //data
                        item.lst.ForEach(s => writer.WriteLine(s));
                    }
                }
            }

            return true;
        }
    }
}
