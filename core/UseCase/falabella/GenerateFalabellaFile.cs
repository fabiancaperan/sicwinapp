using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace core.UseCase.Falabella
{
    public class GenerateFalabellaFile
    {
        private readonly FormatFileByType _format;

        public GenerateFalabellaFile()
        {
            _format = new FormatFileByType();
        }
        private const string _nit = "9000174478";
        private const string _A = "A";
        private const string _N = "N";
        private const string _01 = "0001";
        private const string _02 = "0002";
        private const string _2 = "2";
        private const string _space = " ";
        private const string _dinner = "03";
        private const string _amex = "07";
        

        public List<CommerceModel> build(List<SapModel> lstSap, List<falabellaModel> falabella)
        {
            long total = 0;
            var lst = lstSap
                       .Join(falabella,
                              post => post.Cod_RTL,
                              meta => meta.CODIGO_UNICO,
                              (s, e) => new { s, e })
                        .Where(j => j.s.Nit.Trim() == _nit &&
                                    j.s.Num_Autoriza != string.Empty)
                        .GroupBy(g => new { Rtl = g.s.Cod_RTL.Trim(), Nit = g.s.Nit.Trim() })
                        .Select((j, i) => 
                        {
                            
                            return new CommerceModel
                            {
                                Rtl = j.Key.Rtl,
                                Nit = 1 + j.Key.Nit,
                                Line = new StringBuilder().Append(j.FirstOrDefault().s.TipoRegistro).Append(j.FirstOrDefault().s.FechaCompra)
                                                     .Append(_format.formato(j.FirstOrDefault().s.Nit.Trim(), 13, _A)).Append(_format.formato(RemoveSpecialCharacters(j.FirstOrDefault().s.NombreCadena.Trim()), 30, _A))
                                                     .Append("RMC").Append(new String(' ', 244)).ToString(),
                                Cod_RTL = new StringBuilder().Append(j.FirstOrDefault().s.Cod_RTL.Trim()).Append("-").Append(RemoveSpecialCharacters(j.FirstOrDefault().s.NombreCadena.Trim()))
                                                         .Append("-").Append(j.FirstOrDefault().s.FechaCompra).Append("-").Append(j.FirstOrDefault().s.Nit.Trim()).ToString(),
                                lst = j.Select(l =>
                                {
                                    var cod = l.s.Adquirida_Por + l.s.Adquirida_Para == _dinner ? "09|" :
                                            l.s.Adquirida_Por + l.s.Adquirida_Para == _amex ? "11|" : "|";
                                    var fecComp = l.s.FechaCompra.Substring(0, 8);
                                    var codTrans = l.s.Cod_Trans.Substring(0, 2);
                                    var valor = Convert.ToInt64(l.s.Valor.Substring(0, 10));
                                    total += codTrans == "14" ?-(valor): valor;
                                    return new StringBuilder()
                                .Append("650RE")
                                .Append(l.e.LOCAL_FALABELLA.Substring(1, 2))
                                .Append(cod)
                                .Append(i + 1)
                                .Append("|")
                                .Append("fecha_festivo")
                                .Append("|")
                                .Append(total)
                                .Append(".00|RED")
                                .Append(fecComp)
                                .Append(l.e.LOCAL_FALABELLA)
                                .Append(cod)
                                .Append("COP|830070527-1|")
                                .Append(fecComp)
                                .Append(l.e.LOCAL_FALABELLA)
                                .Append(cod)
                                .Append(total)
                                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                                .Append(fecComp)
                                .Append("00000000|FACOLOCREDAR");
                                }
                         ).ToList()
                            };
                        }).ToList();
            return lst;
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
