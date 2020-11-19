﻿using core.Entities.ComerciosData;
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
        private const string Nit = "9000174478";
        private const string A = "A";
        private const string Dinner = "03";
        private const string Amex = "07";


        public List<CommerceModel> Build(List<SapModel> lstSap, List<FalabellaModel> falabella)
        {
            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            long total = 0;
            var lst = lstSap
                       .Join(falabella,
                              post => post.Cod_RTL.Trim(),
                              meta => _format.Formato(meta.CODIGO_UNICO.Substring(0, 10), 10, A),
                              (s, e) => new { s, e })
                        .Where(j => j.s.Nit.Trim() == Nit &&
                                    Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) > 0 &&
                                    Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) < 9 &&
                                    j.s.Num_Autoriza != string.Empty)
                        .Select((l, i) =>
                        {

                            {
                                var codigo = l.s.Adquirida_Por + l.s.Adquirida_Para == Dinner ? "09" :
                                        l.s.Adquirida_Por + l.s.Adquirida_Para == Amex ? "11" : "13";
                                var cod = codigo == "13" ? "|" : codigo + "|";
                                var fecComp = l.s.FechaCompra.Substring(0, 8);
                                var codTrans = l.s.Cod_Trans.Substring(0, 2);
                                var valor = Convert.ToInt64(l.s.Valor.Substring(0, 10));
                                total += codTrans == "14" ? -(valor) : valor;
                                return new StringBuilder()
                            .Append("650RE")
                            .Append(l.e.LOCAL_FALABELLA.Substring(1, 3))
                            .Append(cod)
                            .Append(i + 1)
                            .Append("|")
                            .Append(dat)
                            .Append("|")
                            .Append(total)
                            .Append(".00|RED")
                            .Append(fecComp)
                            .Append(l.e.LOCAL_FALABELLA)
                            .Append(codigo)
                            .Append("|COP|830070527-1|")
                            .Append(dat)
                            .Append("|IC")
                            .Append(fecComp)
                            .Append(l.e.LOCAL_FALABELLA)
                            .Append(codigo + "|")
                            .Append(total)
                            .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                            .Append(fecComp)
                            .Append("00000000|FACOLOCREDAR");
                            }

                        }).ToList();
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 1+Nit,
                Line = "",
                CodRtl = new StringBuilder().Append("XREDCO_01_").Append(dat).Append(".txt").ToString(),
                Lst = lst
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
