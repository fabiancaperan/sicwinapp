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
    public class GenerateByStoreFile
    {
        private readonly FormatFileByType _format;
        public GenerateByStoreFile()
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
        private List<string> _lstNoCodTrans = new List<string>() { "17", "45", "49", "58", "59" };
        private List<string> _lstTx = new List<string>() { "10", "35", "59", "66", "68" };

        public List<CommerceModel> build(List<SapModel> lstSap, List<conveniosModel> lstConv)
        {
            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            double total = 0;
            var lst = lstSap
                        //.Join(lstConv,
                        //       post => (post.Id_Fran_Hija + post.Filler_Fran_Hija),
                        //       meta => meta.emisor.Trim(),
                        //       (s, e) => new { s, e })
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => s.Nit.Trim() == _nit &&
                                    (s.Adquirida_Por + s.Adquirida_Para).Substring(1, 1) == "B" &&
                                    Convert.ToInt32(s.Cod_Resp.Substring(1, 3)) > 0 &&
                                    Convert.ToInt32(s.Cod_Resp.Substring(1, 3)) < 9 &&
                                    !_lstNoCodTrans.Contains(s.Cod_Trans.Substring(0, 2)))
                              //.GroupBy(g => new { Rtl = g.s.Cod_RTL.Trim(), Nit = g.s.Nit.Trim() })
                              .Select((s, i) =>

                                  {
                                      {
                                          var val = Convert.ToInt64(s.Valor);

                                          if (s.Tipo_Mensaje.Substring(1, 3) == "210")
                                          {
                                              if (s.Cod_Trans.Substring(0, 2) == "14")
                                              {
                                                  total -= val;
                                              }
                                              else if (s.Cod_Trans.Substring(0, 2) == "10" || s.Cod_Trans.Substring(0, 2) == "53")
                                              {
                                                  total += val;
                                              }
                                          }
                                          else if (s.Tipo_Mensaje.Substring(1, 3) == "420")
                                          {
                                              if (s.Cod_Trans.Substring(0, 2) == "14")
                                              {
                                                  total += val;
                                              }
                                              else if (s.Cod_Trans.Substring(0, 2) == "10" || s.Cod_Trans.Substring(0, 2) == "53")
                                              {
                                                  total -= val;
                                              }
                                          }


                                          return new StringBuilder()
                                   .Append(s.Cod_RTL.Trim())//COD_COMER
                                   .Append("\t")
                                   .Append(dat)
                                   .Append("\t")
                                   .Append(_format.formato(i.ToString(), 12, _N))
                                   .Append("\t")
                                   .Append(_format.formato(total.ToString(), 12, _N))
                                   .Append("\t")
                                   .Append("01")
                                   .Append("\t")
                                   .Append("04");
                                      }

                                  }).ToList();
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 1 + _nit,
                Line = "",
                Cod_RTL = new StringBuilder().Append("Redebanmvtos").Append(dat).Append(".txt").ToString(),
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
