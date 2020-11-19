﻿using core.Entities.ComerciosData;
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
        public bool Build(string rute, List<CommerceType> commerceTypes)
        {
            var lstEntidades = _db.EntidadesModel.ToList();
            var lstFalabella = _db.FalabellaModel.ToList();
            var lstConv = _db.ConveniosModel.ToList();
            var lstCnb = _db.CnbsModel.ToList();
            var lstSap = new SicContext().GetAll();
            var lstBinesesp = _db.BinesesModel.ToList();
            var lstRedPrivadas = _db.RedprivadasModel.ToList();
            foreach (var commerceType in commerceTypes)
            {

                List<CommerceModel> res;
                switch (commerceType)
                {
                    case CommerceType.Comercios:
                        res = new GenerateComerciosFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Falabella:
                        res = new GenerateFalabellaFile().build(lstSap, lstFalabella);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Olimpica:
                        res = new GenerateOlimpicaFile().Build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.Exito:
                        res = new GenerateExitoFile().build(lstSap, lstEntidades);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.ExitoTarjetasPrivadas:
                        res = new GenerateConcilationFile().build(lstSap, lstConv);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.ExitoRedebanmvtos:
                        res = new GenerateRedebanmvtosFile().Build(lstSap, lstBinesesp, lstRedPrivadas);
                        if (res != null && res.Any())
                            ComerciosFileRtc(rute, res);
                        break;
                    case CommerceType.Cnb:
                        res = new GenerateCnb().Build(lstSap, lstEntidades, lstCnb);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.CnbSpecial:
                        res = new GenerateCnbSpecial().Build(lstSap, lstEntidades, lstCnb);
                        if (res != null && res.Any())
                            ComerciosFiles(rute, res);
                        break;
                    case CommerceType.Cencosud:
                        res = new GenerateCarrefourFile().Build(lstSap, lstEntidades);
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
                //string path = Path.Combine(rute, commerceType.ToString() + "\\" + item.Value.FirstOrDefault().Nit.Trim());
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.CodRtl);
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                //FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    //title
                    if (item.Line.Trim() != string.Empty)
                        writer.WriteLine(item.Line);
                    //data
                    item.Lst.ForEach(s => { writer.WriteLine(s); });
                }
            }
        }

        private void ComerciosFileRtc(string rute,
            List<CommerceModel> filelst)
        {

            foreach (CommerceModel item in filelst)
            {
                string path = Path.Combine(rute, item.Nit.Trim());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, item.CodRtl);
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    //title
                    if (item.Line.Trim() != string.Empty)
                        writer.WriteLine(item.Line);
                    //data
                    item.Lst.ForEach(s => { writer.WriteLine(s); });
                }
            }
        }
    }
}
