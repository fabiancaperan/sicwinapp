using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace core.UseCase.Exito
{
    public class GenerateRedebanmvtosFile
    {
        private readonly FormatFileByType _format;
        private readonly StringBuilder _dat;
        public GenerateRedebanmvtosFile()
        {
            var date = DateTime.Now;
            _dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            _format = new FormatFileByType();
        }
        private const string Nit = "8909006089";
        private const string N = "N";
        private readonly List<string> _lstNoCodTrans = new List<string>() { "17", "45", "49", "58", "59" };

        public List<CommerceModel> Build(List<SapModel> lstSap, List<BinesespModel> lstBinesesp, List<RedprivadasModel> lstRedPrivadas)
        {

            var lstByNit = lstSap
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Where(s => s.Nit.Trim() == Nit &&
                            Convert.ToInt32(s.Cod_Resp.Substring(0, 3)) > 0 &&
                            Convert.ToInt32(s.Cod_Resp.Substring(0, 3)) < 9 &&
                            !_lstNoCodTrans.Contains(s.Cod_Trans.Substring(0, 2))).OrderBy(s=> s.Cod_RTL).ToList();
            var lst = new List<StringBuilder>();
            var lstRedPriv = lstRedPrivadas.Select(s => s.red).ToList();
            
            var lstFiidTarStep2 = lstBinesesp.Where(s => s.Fiid != "0808").Select(s => s.Fiid).ToList();
            lstFiidTarStep2.Add("0821");
            lstFiidTarStep2.Add("0808");
            lstFiidTarStep2.Add("0903");

            var lstFiidTarStep3 = lstBinesesp.Where(s => s.Fiid != "0808").Select(s => s.Fiid).ToList();

            foreach (var g in lstByNit.GroupBy(g=>g.Cod_RTL))
            {
                lst.Add(BuildStep1(g.ToList(), g.Key));
                lst.Add(BuildStep2(g.ToList(), lstFiidTarStep2, lstRedPriv, g.Key));
                lst.Add(BuildStep3(g.ToList(), lstFiidTarStep3, lstRedPriv, g.Key));

            }
           
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 1 + Nit,
                Line = "",
                CodRtl = new StringBuilder().Append("Redebanmvtos").Append(_dat).Append(".txt").ToString(),
                Lst = lst
            };
            var lstres = new List<CommerceModel> { rs };
            return lstres;
        }


        public StringBuilder BuildStep1(List<SapModel> lstSap, string codRtl)
        {
            double total = 0;
            var filter = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => (s.Adquirida_Por + s.Adquirida_Para).Substring(1, 1) == "B").ToList();
            filter.ForEach((s) =>

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


                                      
                                  }

                              });
            var num = filter.Count();
            var ret = new StringBuilder()
                .Append(codRtl)//COD_COMER
                .Append("\t")
                .Append(_dat)
                .Append("\t")
                .Append(_format.Formato(num.ToString(), 12, N))
                .Append("\t")
                .Append(_format.Formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                .Append("\t")
                .Append("01")
                .Append("\t")
                .Append("04");
            return ret;
        }

        private StringBuilder BuildStep2(List<SapModel> lstSap, List<string> lstFiidTarStep2, List<string> lstRedPriv, string codRtl)
        {
            
            double total = 0;
            //INCONSISTENCIA DE FILTRO CON (s.Id_Fran_Hija + s.Filler_Fran_Hija) == Y != "000"
            var filter =lstSap
                    .AsParallel()
                    .WithDegreeOfParallelism(4)
                    .Where(s =>
                        (!lstFiidTarStep2.Contains(s
                                .Fiid_Emisor) /*|| (s.Fiid_Emisor == "0808" && (s.Id_Fran_Hija + s.Filler_Fran_Hija) == "000")*/
                        ) &&
                        (s.Id_Fran_Hija + s.Filler_Fran_Hija) != "000" &&
                        !lstRedPriv.Contains((s.Adquirida_Por + s.Adquirida_Para))).ToList();

                    filter.ForEach((s) =>

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
                        }

                    });

                    var num = filter.Count();
                    StringBuilder ret = new StringBuilder()
                        .Append(codRtl)//COD_COMER
                        .Append("\t")
                        .Append(_dat)
                        .Append("\t")
                        .Append(_format.Formato(num.ToString(), 12, N))
                        .Append("\t")
                        .Append(_format.Formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                        .Append("\t")
                        .Append("01")
                        .Append("\t")
                        .Append("01");
            return ret;
        }

        private StringBuilder BuildStep3(List<SapModel> lstSap, List<string> lstFiidTarStep3, List<string> lstRedPriv, string codRtl)
        {
            
            double total = 0;
            //INCONSISTENCIA DE FILTRO CON (s.Id_Fran_Hija + s.Filler_Fran_Hija) == Y != "000"
            var filter = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => (lstFiidTarStep3.Contains(s.Fiid_Emisor) || (
                                                (s.Id_Fran_Hija + s.Filler_Fran_Hija) != "000" &&
                                                            (s.Fiid_Emisor == "0808") || lstRedPriv.Contains((s.Adquirida_Por + s.Adquirida_Para)))
                                                )
                                        ).ToList();
            filter.ForEach((s) =>

                              {
                                  {
                                      var val = Convert.ToInt64(s.Valor);

                                      if (s.Tipo_Mensaje.Substring(1, 3) == "210")
                                      {
                                          if (s.Cod_Trans.Substring(0, 2) == "14" || s.Cod_Trans.Substring(0, 2) == "45")
                                          {
                                              total -= val;
                                          }
                                          else if (s.Cod_Trans.Substring(0, 2) == "10" || s.Cod_Trans.Substring(0, 2) == "53" || s.Cod_Trans.Substring(0, 2) == "15")
                                          {
                                              total += val;
                                          }
                                      }
                                      else if (s.Tipo_Mensaje.Substring(1, 3) == "420")
                                      {
                                          if (s.Cod_Trans.Substring(0, 2) == "14" || s.Cod_Trans.Substring(0, 2) == "45")
                                          {
                                              total += val;
                                          }
                                          else if (s.Cod_Trans.Substring(0, 2) == "10" || s.Cod_Trans.Substring(0, 2) == "53" || s.Cod_Trans.Substring(0, 2) == "15")
                                          {
                                              total -= val;
                                          }
                                      }
                                  }

                              });
            var num = filter.Count();
            var res = new StringBuilder()
                .Append(codRtl)//COD_COMER
                .Append("\t")
                .Append(_dat)
                .Append("\t")
                .Append(_format.Formato(num.ToString(), 12, N))
                .Append("\t")
                .Append(_format.Formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                .Append("\t")
                .Append("01")
                .Append("\t")
                .Append("02");
            return res;
        }
    }
}
