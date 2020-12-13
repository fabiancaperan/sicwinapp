using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace core.UseCase.Exito
{
    public class GenerateConcilationFile
    {
        //private readonly FormatFileByType _format;
        //public GenerateConcilationFile()
        //{
        //    //_format = new FormatFileByType();
        //}
        private readonly FormatFileByType _format;
        public GenerateConcilationFile()
        {
            _format = new FormatFileByType();
        }
        private const string Nit = "8909006089";
        private readonly List<string> _lstNoCodTrans = new List<string>() { "17", "31", "32", "33", "36", "37", "49", "58", "89" };
        private readonly List<string> _lstTx = new List<string>() { "10", "35", "59", "66", "68" };
        private const string Space = " ";
        private const string N = "N";
        private const string A = "A";

        public List<CommerceModel> Build(List<SapModel> lstSap,List<ConveniosModel> lstConv, StringBuilder dat)
        {
            double total = 0;
            var lstEmisor = lstConv.Select(s => s.emisor.Trim()).ToList();
            var lst = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => s.Nit.Trim() == Nit &&
                                    !_lstNoCodTrans.Contains(s.Cod_Trans.Substring(0, 2)) &&
                                    lstEmisor.Contains(s.Id_Fran_Hija + s.Filler_Fran_Hija)
                                    )
                       .OrderBy(o => o.Cod_RTL).ThenBy(o => o.FechaTran).ThenBy(o=>o.HoraTran)
                       //.ThenBy(o => o.e.bolsillo)
                              //.GroupBy(g => new { Rtl = g.s.Cod_RTL.Trim(), Nit = g.s.Nit.Trim() })
                              .Select(l =>

                                  {

                                      {
                                          var signo = (l.Tipo_Mensaje.Trim() == "0210" &&
                                          l.Cod_Trans.Substring(0, 2) == "14") ||
                                          (l.Tipo_Mensaje.Trim() == "0420" &&
                                          _lstTx.Contains(l.Cod_Trans.Substring(0, 2)))
                                          ? -1 : 1;
                                          var tx = signo * Convert.ToDouble(l.Valor) / 100;

                                          total += tx; 

                                          return new StringBuilder()
                                   .Append("0|")
                                   .Append(l.Cod_RTL.Trim())//COD_COMER
                                   .Append("|0|")
                                   .Append(Convert.ToInt64(l.Num_Secuen.Trim()))//CONSECUTIVO
                                   .Append("|")
                                   .Append(l.FechaCompra)//fecha contrato
                                   .Append("|")
                                   .Append(l.FechaTran)
                                   .Append("|")
                                   .Append(l.HoraTran)
                                   .Append("|")
                                   .Append(l.Bin_Tarjeta)//BIN
                                   .Append("|")
                                   .Append(l.Cod_Trans.Substring(3, 2))//BOLSILLO
                                   .Append("|")
                                   .Append(l.Id_Fran_Hija + l.Filler_Fran_Hija)//CONVENIO
                                   .Append("|")
                                   .Append(tx)
                                   .Append("|0|")
                                   .Append(l.Num_Autoriza);
                                      }

                                  }).ToList();
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 2 + Nit,
                Line = "",
                CodRtl = new StringBuilder().Append("TPRIVADAS").Append(dat).Append(".txt").ToString(),
                Lst = lst,
                FinalLine = new StringBuilder().Append(lst.Count()).Append("|").Append(total).Append("|").Append(dat).ToString()
            };
            var lstres = new List<CommerceModel> {rs};
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
