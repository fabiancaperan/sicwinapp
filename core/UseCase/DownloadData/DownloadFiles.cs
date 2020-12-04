using core.Entities.ComerciosData;
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
        public bool Build(string rute, List<CommerceType> commerceTypes, List<Entities.ConvertData.SapModel> lstSap)
        {
            var lstEntidades = _db.EntidadesModel.ToList();
            var lstFalabella = _db.FalabellaModel.ToList();
            var lstConv = _db.ConveniosModel.ToList();
            var lstCnb = _db.CnbsModel.ToList();
            //var lstSap = new SicContext().GetAll();
            var date = new SicContext().GetDate();
            var lstBinesesp = _db.BinesespModel.ToList();
            var lstRedPrivadas = _db.RedprivadasModel.ToList();

            if (date == null)
                return false;
            var dat = new StringBuilder().Append(date.Value.Year).Append(date.Value.Month).Append(date.Value.Day);
            var datmvtos = new StringBuilder().Append(date.Value.Day).Append(date.Value.Month).Append(date.Value.Year);
            foreach (var commerceType in commerceTypes)
            {

                List<CommerceModel> res;
                switch (commerceType)
                {
                    case CommerceType.Comercios:
                        res = new GenerateComerciosFile().Build(lstSap, lstEntidades, dat);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Falabella:
                        res = new GenerateFalabellaFile().Build(lstSap, lstFalabella, dat);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Olimpica:
                        res = new GenerateOlimpicaFile().Build(lstSap, lstEntidades, dat);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.Exito:
                        res = new GenerateExitoFile().Build(lstSap, lstEntidades, dat);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.ExitoTarjetasPrivadas:
                        res = new GenerateConcilationFile().Build(lstSap, lstConv, dat);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.ExitoRedebanmvtos:
                        res = new GenerateRedebanmvtosFile().Build(lstSap, lstBinesesp, lstRedPrivadas, datmvtos);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.Cnb:
                        res = new GenerateCnb().Build(lstSap, lstEntidades, lstCnb, dat);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.CnbSpecial:
                        res = new GenerateCnbSpecial().Build(lstSap, lstEntidades, lstCnb, dat);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Cencosud:
                        res = new GenerateCarrefourFile().Build(lstSap, lstEntidades, dat);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                }
            }

            return true;
        }

        private void ComerciosFiles(string rute, List<CommerceModel> filelst)
        {

            foreach (CommerceModel item in filelst)
            {
                if (item.Lst == null || !item.Lst.Any())
                    return;
                //string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.CodRtl);
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
                //title
                if (item.Line != null && item.Line.Trim() != string.Empty)
                    writer.WriteLine(item.Line);
                //data
                item.Lst.ForEach(s => { writer.WriteLine(s); });
                //num regis
                if (item.FinalLine != null && item.FinalLine.Trim() != string.Empty)
                    writer.WriteLine(item.FinalLine);
            }
        }

        private void ComerciosFileRtc(string rute,
            List<CommerceModel> filelst)
        {

            foreach (CommerceModel item in filelst)
            {
                if (item.Lst == null || !item.Lst.Any())
                    return;
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.CodRtl);
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
                //title
                if (item.Line != null && item.Line.Trim() != string.Empty)
                    writer.WriteLine(item.Line);
                //data
                item.Lst.ForEach(s => { writer.WriteLine(s); });
                //num regis
                if (item.FinalLine!= null && item.FinalLine.Trim() != string.Empty)
                    writer.WriteLine(item.FinalLine);
            }
        }
    }
}
