﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;

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


        public List<CommerceModel> Build(List<SapModel> lstSap, List<FalabellaModel> falabella, StringBuilder date)
        {
            var dat = GetDat();

            var lstFilter = falabella
                .Join(lstSap,
                    meta => meta.CODIGO_UNICO,
                    post => post.Cod_RTL.Trim(),
                    (e, s) => new { e, s })
                .Where(j => j.s.Nit.Trim() == Nit &&
                            j.e.CODIGO_UNICO == j.s.Cod_RTL.Trim() &&
                            int.TryParse(j.s.Cod_Resp, out int res) &&
                            // _format.Formato(j.e.CODIGO_UNICO.Substring(0, 10), 10, A)== j.s.Cod_RTL.Trim()&&
                            res >= 0 &&
                            res <= 9 &&
                            j.s.Num_Autoriza.Trim() != string.Empty)
                .Select(m => new SapModel()
                {
                    Cod_RTL = m.s.Cod_RTL.Trim(),
                    //usamos Nombre cadena como localFalabella
                    NombreCadena = m.e.LOCAL_FALABELLA,
                    Cod_Trans = m.s.Cod_Trans,
                    FechaCompra = m.s.FechaCompra,
                    Adquirida_Para = m.s.Adquirida_Para,
                    Adquirida_Por = m.s.Adquirida_Por,
                    Valor = m.s.Valor
                })
                .GroupBy(g => new { g.Cod_RTL, g.NombreCadena }).ToList();

            var lstText = new List<StringBuilder>();

            int i = 1;
            foreach (var g in lstFilter.OrderBy(s => s.Key.Cod_RTL))
            {
                var lstOrder = g
                    //.OrderByDescending(o => o.Tipo_Mensaje)
                    .ToList();
                var local = g.Key.NombreCadena.Substring(0, 4);
                lstText.Add(BuildStep1(lstOrder, dat.ToString(), date.ToString(), local, i));
                i++;
                lstText.Add(BuildStep2(lstOrder, dat.ToString(), date.ToString(), local, i));
                i++;
                lstText.Add(BuildStep3(lstOrder, dat.ToString(), date.ToString(), local, i));
                i++;
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

        private StringBuilder BuildStep1(List<SapModel> lstSap, string dat, string dateFile, string localFalabella, int num)
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
                    if (long.TryParse(s.Valor.Substring(0, 10), out var valor))
                    {
                        total += codTrans == "14" ? -(valor) : valor;
                    }
                }

            });
            //var num = filter.Count();

            var ret = new StringBuilder()
                .Append("650RE")
                .Append(localFalabella.Substring(1, 3))
                .Append("09|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("09|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("09|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR");
            return ret;

        }

        private StringBuilder BuildStep2(List<SapModel> lstSap, string dat, string dateFile, string localFalabella, int num)
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
                    if (long.TryParse(s.Valor.Substring(0, 10), out var valor))
                    {
                        total += codTrans == "14" ? -(valor) : valor;
                    }
                }

            });

            //var num = filter.Count();
            var ret = new StringBuilder()
                .Append("650RE")
                .Append(localFalabella.Substring(1, 3))
                .Append("11|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("11|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("11|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR");
            return ret;

        }

        private StringBuilder BuildStep3(List<SapModel> lstSap, string dat, string dateFile, string localFalabella, int num)
        {

            double total = 0;


            var filter = lstSap
                .Where(s => !new[] { "03", "07" }.Contains(s.Adquirida_Por + s.Adquirida_Para)).ToList();

            var fechaComp = dateFile;
            filter.ForEach((s) =>

            {
                {
                    fechaComp = s.FechaCompra.Substring(0, 8);
                    var codTrans = s.Cod_Trans.Substring(0, 2);
                    if (long.TryParse(s.Valor.Substring(0, 10), out var valor))
                    {
                        total += codTrans == "14" ? -(valor) : valor;
                    }
                }

            });
            //var num = filter.Count();

            var ret = new StringBuilder()
                .Append("650RE")
                .Append(localFalabella.Substring(1, 3))
                .Append("|")
                .Append(num)
                .Append("|")
                .Append(dat)
                .Append("|")
                .Append(total)
                .Append(".00|RED")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("13|COP|830070527-1|")
                .Append(dat)
                .Append("|IC")
                .Append(fechaComp)
                .Append(localFalabella)
                .Append("13|")
                .Append(total)
                .Append(".00|||19|CO|||||||||||FACOLOCREDAR")
                .Append(fechaComp)
                .Append("00000000|FACOLOCREDAR");
            return ret;

        }

        private StringBuilder GetDat()
        {
            var today = DateTime.Today;
            var dayWeek = DateTime.Now.DayOfWeek;
            var dayAdd = dayWeek == DayOfWeek.Sunday ? 1 :
                dayWeek == DayOfWeek.Friday ? 3 :
                dayWeek == DayOfWeek.Saturday ? 2 : 0;
            if (dayAdd > 0)
                today = today.AddDays(dayAdd);
            var dat = new StringBuilder()
                .Append(today.Year)
                .Append(_format.Formato(today.Month.ToString(), 2, "N"))
                .Append(_format.Formato(today.Day.ToString(), 2, "N"));
            return dat;
        }

    }
}
