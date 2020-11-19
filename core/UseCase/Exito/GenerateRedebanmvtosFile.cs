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
        public GenerateRedebanmvtosFile()
        {
            _format = new FormatFileByType();
        }
        private const string Nit = "8909006089";
        private const string N = "N";
        private readonly List<string> _lstNoCodTrans = new List<string>() { "17", "45", "49", "58", "59" };

        public List<CommerceModel> Build(List<SapModel> lstSap, List<BinesespModel> lstBinesesp, List<RedprivadasModel> lstRedPrivadas)
        {
            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
#pragma warning disable CS0219 // The variable 'total' is assigned but its value is never used
            double total = 0;
#pragma warning restore CS0219 // The variable 'total' is assigned but its value is never used
            var lstByNit = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => s.Nit.Trim() == Nit &&
                                    Convert.ToInt32(s.Cod_Resp.Substring(0, 3)) > 0 &&
                                    Convert.ToInt32(s.Cod_Resp.Substring(0, 3)) < 9 &&
                                    !_lstNoCodTrans.Contains(s.Cod_Trans.Substring(0, 2))).ToList();
            var lst = new List<StringBuilder>();
            lst.AddRange(BuildStepOne(lstByNit));
            lst.AddRange(BuildStepTwo(lstByNit, lstBinesesp, lstRedPrivadas));
            lst.AddRange(BuildStepThree(lstByNit, lstBinesesp, lstRedPrivadas));
            var rs = new CommerceModel()
            {
                Rtl = "",
                Nit = 1 + Nit,
                Line = "",
                CodRtl = new StringBuilder().Append("Redebanmvtos").Append(dat).Append(".txt").ToString(),
                Lst = lst
            };
            var lstres = new List<CommerceModel> {rs};
            return lstres;
        }
        public List<StringBuilder> BuildStepOne(List<SapModel> lstSap)
        {
            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            double total = 0;
            var lst = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s =>(s.Adquirida_Por + s.Adquirida_Para).Substring(1, 1) == "B" )
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
                                   .Append(_format.formato(i.ToString(), 12, N))
                                   .Append("\t")
                                   .Append(_format.formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                                   .Append("\t")
                                   .Append("01")
                                   .Append("\t")
                                   .Append("04");
                                      }

                                  }).ToList();
            return lst;
        }

        private List<StringBuilder> BuildStepTwo(List<SapModel> lstSap, List<BinesespModel> lstBinesesp, List<RedprivadasModel> lstRedPrivadas)
        {
            var lstFiidTar = lstBinesesp.Where(s => s.Fiid != "0808").Select(s => s.Fiid).ToList();
            lstFiidTar.Add("0821");
            lstFiidTar.Add("0808");
            lstFiidTar.Add("0903");

            var lstRedPriv = lstRedPrivadas.Select(s => s.red).ToList();

            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            double total = 0;
            //INCONSISTENCIA DE FILTRO CON (s.Id_Fran_Hija + s.Filler_Fran_Hija) == Y != "000"
            var lst = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => (!lstFiidTar.Contains(s.Fiid_Emisor) /*|| (s.Fiid_Emisor == "0808" && (s.Id_Fran_Hija + s.Filler_Fran_Hija) == "000")*/) &&
                                        (s.Id_Fran_Hija + s.Filler_Fran_Hija) != "000" &&
                                     !lstRedPriv.Contains((s.Adquirida_Por + s.Adquirida_Para)))
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
                               .Append(_format.formato(i.ToString(), 12, N))
                               .Append("\t")
                               .Append(_format.formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                               .Append("\t")
                               .Append("01")
                               .Append("\t")
                               .Append("01");
                                  }

                              }).ToList();
            return lst;
        }

        private List<StringBuilder> BuildStepThree(List<SapModel> lstSap, List<BinesespModel> lstBinesesp, List<RedprivadasModel> lstRedPrivadas)
        {
            var lstFiidTar = lstBinesesp.Where(s => s.Fiid != "0808").Select(s => s.Fiid).ToList();

            var lstRedPriv = lstRedPrivadas.Select(s => s.red).ToList();

            var date = DateTime.Now;
            var dat = new StringBuilder().Append(date.Year).Append(date.Month).Append(date.Day);
            double total = 0;
            //INCONSISTENCIA DE FILTRO CON (s.Id_Fran_Hija + s.Filler_Fran_Hija) == Y != "000"
            var lst = lstSap
                        .AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(s => (lstFiidTar.Contains(s.Fiid_Emisor) || (
                                                (s.Id_Fran_Hija + s.Filler_Fran_Hija) != "000" && 
                                                            (s.Fiid_Emisor == "0808") || lstRedPriv.Contains((s.Adquirida_Por + s.Adquirida_Para))) 
                                                )
                                        )
                              .Select((s, i) =>

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


                                      return new StringBuilder()
                               .Append(s.Cod_RTL.Trim())//COD_COMER
                               .Append("\t")
                               .Append(dat)
                               .Append("\t")
                               .Append(_format.formato(i.ToString(), 12, N))
                               .Append("\t")
                               .Append(_format.formato(total.ToString(CultureInfo.InvariantCulture), 12, N))
                               .Append("\t")
                               .Append("01")
                               .Append("\t")
                               .Append("02");
                                  }

                              }).ToList();
            return lst;
        }
    }
}
