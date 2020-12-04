using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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


        public List<CommerceModel> Build(List<SapModel> lstSap, List<FalabellaModel> falabella, StringBuilder date)
        {

            long total = 0;
            var dat = getDat();

            var lstFilter = lstSap
                .Join(falabella,
                    post => post.Cod_RTL.Trim(),
                    meta => _format.Formato(meta.CODIGO_UNICO.Substring(0, 10), 10, A),
                    (s, e) => new { s, e })
                .Where(j => j.s.Nit.Trim() == Nit &&
                            Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) >= 0 &&
                            Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) <= 9 &&
                            j.s.Num_Autoriza != string.Empty)
                .OrderByDescending(o => o.s.Tipo_Mensaje).Select(m => new SapModel()
                {
                    Cod_RTL = m.s.Cod_RTL.Trim(),
                    //usamos Nombre cadena como localFalabella
                    NombreCadena = m.e.LOCAL_FALABELLA,
                    Cod_Trans = m.s.Cod_Trans,
                    FechaCompra = m.s.FechaCompra,
                    Valor = m.s.Valor
                })
                .GroupBy(g => new { g.Cod_RTL, g.NombreCadena }).ToList();
            var lst = lstSap
                .Join(falabella,
                    post => post.Cod_RTL.Trim(),
                    meta => _format.Formato(meta.CODIGO_UNICO.Substring(0, 10), 10, A),
                    (s, e) => new {s, e})
                .Where(j => j.s.Nit.Trim() == Nit &&
                            Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) > 0 &&
                            Convert.ToInt32(j.s.Cod_Resp.Substring(0, 3)) < 9 &&
                            j.s.Num_Autoriza != string.Empty);
            var lstText = new List<StringBuilder>();


            foreach (var g in lstFilter)
            {
                lstText.Add(BuildStep1(g.ToList(), dat.ToString(), date.ToString(), g.Key.NombreCadena));
                lstText.Add(BuildStep2(g.ToList(), dat.ToString(), date.ToString(), g.Key.NombreCadena));
                lstText.Add(BuildStep3(g.ToList(), dat.ToString(), date.ToString(), g.Key.NombreCadena));
            }

            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 1 + Nit,
                Line = "",
                CodRtl = new StringBuilder().Append("XREDCO_01_").Append(date).Append(".txt").ToString(),
                Lst = lstText
            };
            var lstres = new List<CommerceModel> { rs };
            return lstres;
        }

        private StringBuilder BuildStep1(List<SapModel> lstSap, string dat, string dateFile, string localFalabella)
        {

            double total = 0;


            var filter = lstSap
                .Where(s => (s.Adquirida_Por + s.Adquirida_Para) == "03").ToList();

            var fechaComp = dateFile;
            filter.ForEach((s) =>

            {
                {
                    fechaComp = s.FechaCompra.Substring(0, 8);
                    var codTrans = s.Cod_Trans.Substring(0, 2);
                    var valor = Convert.ToInt64(s.Valor.Substring(0, 10));
                    total += codTrans == "14" ? -(valor) : valor;
                }

            });
            var num = filter.Count();
            var local = localFalabella.Substring(0, 4);
            var ret = new StringBuilder()
                .Append("650RE")
                .Append(local)
                .Append("09|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(local)
                .Append("09|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(local)
                .Append("09|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR"); ;
            return ret;

        }

        private StringBuilder BuildStep2(List<SapModel> lstSap, string dat, string dateFile, string localFalabella)
        {

            double total = 0;

            var filter = lstSap
                .Where(s => (s.Adquirida_Por + s.Adquirida_Para) == "07").ToList();

            var fechaComp = dateFile;
            filter.ForEach((s) =>

            {
                {
                    fechaComp = s.FechaCompra.Substring(0, 8);
                    var codTrans = s.Cod_Trans.Substring(0, 2);
                    var valor = Convert.ToInt64(s.Valor.Substring(0, 10));
                    total += codTrans == "14" ? -(valor) : valor;
                }

            });
            var local = localFalabella.Substring(0, 4);
            var num = filter.Count();
            var ret = new StringBuilder()
                .Append("650RE")
                .Append(local)
                .Append("11|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(local)
                .Append("11|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(local)
                .Append("11|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR"); ;
            return ret;

        }

        private StringBuilder BuildStep3(List<SapModel> lstSap, string dat, string dateFile, string localFalabella)
        {

            double total = 0;


            var filter = lstSap
                .Where(s => !new[]{ "03","07" }.Contains(s.Adquirida_Por + s.Adquirida_Para)).ToList();

            var fechaComp = dateFile;
            filter.ForEach((s) =>

            {
                {
                    fechaComp = s.FechaCompra.Substring(0, 8);
                    var codTrans = s.Cod_Trans.Substring(0, 2);
                    var valor = Convert.ToInt64(s.Valor.Substring(0, 10));
                    total += codTrans == "14" ? -(valor) : valor;
                }

            });
            var num = filter.Count();
            var local = localFalabella.Substring(0, 4);
            var ret = new StringBuilder()
                .Append("650RE")
                .Append(local)
                .Append("|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(local)
                .Append("13|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(local)
                .Append("13|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR"); ;
            return ret;

        }

        private StringBuilder getDat()
        {
            var today = DateTime.Today;
            var dayWeek = DateTime.Now.DayOfWeek;
            var dayAdd = dayWeek == DayOfWeek.Sunday ? 1 :
                dayWeek == DayOfWeek.Friday ? 3 :
                dayWeek == DayOfWeek.Saturday ? 2 : 0;
            if (dayAdd > 0)
                today = today.AddDays(dayAdd);
            var dat = new StringBuilder().Append(today.Year).Append(today.Month).Append(today.Day);
            return dat;
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
