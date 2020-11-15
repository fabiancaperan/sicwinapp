using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace core.UseCase.Exito
{
    public class GenerateConcilationFile
    {
        private readonly FormatFileByType _format;
        public GenerateConcilationFile()
        {
            _format = new FormatFileByType();
        }
        private const string _nit = "8909006089";
        private const string _A = "A";
        private const string _N = "N";
        private const string _01 = "0001";
        private const string _02 = "0002";
        private const string _2 = "2";
        private const string _space = " ";
        private List<string> _lstNoCodTrans = new List<string>() { "17", "31", "32", "33", "36", "37", "49", "58", "89" };
        private List<string> _lstTx = new List<string>() { "10", "35", "59", "66", "68" };

        public List<CommerceModel> build(List<SapModel> lstSap,List<conveniosModel> lstConv)
        {
            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            double total = 0;
            var lst = lstSap
                       .Join(lstConv,
                              post => (post.Id_Fran_Hija + post.Filler_Fran_Hija),
                              meta => meta.emisor.Trim(),
                              (s, e) => new { s, e })
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(j => j.s.Nit.Trim() == _nit &&
                                    !_lstNoCodTrans.Contains(j.s.Cod_Trans.Substring(0, 2)))
                              //.GroupBy(g => new { Rtl = g.s.Cod_RTL.Trim(), Nit = g.s.Nit.Trim() })
                              .Select(l =>

                                  {

                                      {
                                          var signo = (l.s.Tipo_Mensaje.Trim() == "0210" &&
                                          l.s.Cod_Trans.Substring(0, 2) == "14") ||
                                          (l.s.Tipo_Mensaje.Trim() == "0420" &&
                                          _lstTx.Contains(l.s.Cod_Trans.Substring(0, 2)))
                                          ? -1 : 1;
                                          var tx = signo * Convert.ToDouble(l.s.Valor) / 100;
                                          total += tx;

                                          return new StringBuilder()
                                   .Append("0|")
                                   .Append(l.s.Cod_RTL.Trim())//COD_COMER
                                   .Append("|0|")
                                   .Append(Convert.ToInt64(l.s.Num_Secuen.Trim()))//CONSECUTIVO
                                   .Append("|")
                                   .Append(l.s.FechaCompra)//fecha contrato
                                   .Append("|")
                                   .Append(l.s.FechaTran)
                                   .Append("|")
                                   .Append(l.s.HoraTran)
                                   .Append("|")
                                   .Append(l.s.Bin_Tarjeta)//BIN
                                   .Append("|")
                                   .Append(l.s.Cod_Trans.Substring(3, 2))//BOLSILLO
                                   .Append("|")
                                   .Append(l.s.Id_Fran_Hija + l.s.Filler_Fran_Hija)//CONVENIO
                                   .Append("|")
                                   .Append(tx)
                                   .Append("|0|")
                                   .Append(l.s.Num_Autoriza);
                                      }

                                  }).ToList();
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 2 + _nit,
                Line = "",
                Cod_RTL = new StringBuilder().Append("TPRIVADAS").Append(dat).Append(".txt").ToString(),
                lst = lst
            };
            var lstres = new List<CommerceModel>();
            lstres.Add(rs);
            return lstres;
        }
        public string RemoveSpecialCharacters(string input)
        {

            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = r.Replace(input, String.Empty);
            input = input.Replace(" ", "_");
            return input;
        }

    }
}
