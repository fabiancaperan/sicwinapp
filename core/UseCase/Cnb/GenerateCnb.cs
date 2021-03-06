﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;

namespace core.UseCase.Cnb
{
    public class GenerateCnb
    {
        private readonly FormatFileByType _format;
        public GenerateCnb()
        {
            _format = new FormatFileByType();
        }
        private const string A = "A";
        private const string N = "N";
        private const string _01 = "0001";
        private const string _02 = "0002";
        private const string _2 = "2";
        private const string Space = " ";


        public List<CommerceModel> Build(List<SapModel> lstSap, List<EntidadesModel> entidades, StringBuilder dat)
        {
            var fiids = entidades.Where(s => s.fiid != null).OrderBy(s => s.fiid).ToList();
            var lstCommercemodel = new List<CommerceModel>();
            fiids.ForEach(s =>
            {
                var lstFilter = lstSap.Where(l => Regex.IsMatch(l.Cod_RTL, LikeToRegular(3 + Right(s.fiid, 3) + "%")))
                .OrderBy(o => o.Cod_RTL).ToList();
                if (lstFilter.Any())
                {

                    lstFilter.ForEach(f =>
                    {

                    });

                    var res = new CommerceModel()
                    {
                        Rtl = "",
                        Nit = 1 + s.nit.Trim(),
                        Line = new StringBuilder().Append("02").Append(dat)
                                                            .Append(_format.Formato(s.nit.Trim(), 13, N)).Append(_format.Formato(RemoveSpecialCharactersChangeBySpace(s.nombre.Trim()), 30, A))
                                                            .Append("RMC").Append(new String(' ', 244)).ToString(),
                        CodRtl = new StringBuilder().Append("3").Append(Right(s.fiid, 3)).Append("000001")
                                                                .Append("-").Append(RemoveSpecialCharacters(s.nombre.Trim()))
                                                                .Append("-").Append(dat).Append("-").Append(s.nit.Trim()).ToString(),
                        FinalLine = new StringBuilder().Append("03").Append(_format.Formato(lstFilter.Count.ToString(), 8, N)).Append(_format.Formato(Space, 290, A)).ToString(),
                        Lst = lstFilter.Select(l =>
                                  new StringBuilder()
                                 .Append("01")
                                 .Append(_format.Formato(l.Id_Terminal.Substring(0, 16), 16, A))
                                 .Append(_format.Formato(l.COD_DANE.Substring(0, 8), 8, A))
                                 .Append(_format.Formato(l.FechaTran.Substring(0, 8), 8, A))
                                 .Append(_format.Formato(l.HoraTran.Substring(0, 6), 6, A))
                                 .Append(_format.Formato(l.Fiid_Emisor.Substring(0, 4), 4, A))
                                 .Append(_format.Formato(l.Abrev_Emisor.Substring(0, 3), 3, A))//FID_EMISOR NOMBRE EMISOR
                                 .Append(_format.Formato(l.Num_Tarjeta.Substring(0, 4), 4, N))
                                 .Append(_format.Formato(l.Tipo_Mensaje.Substring(0, 4), 4, N))
                                 .Append(_format.Formato(l.Cod_Trans.Substring(0, 6), 6, N))
                                 .Append(_format.Formato(l.Num_Secuen.Substring(0, 12), 12, N))
                                 .Append(_format.Formato(l.Valor.Substring(0, 12), 12, N))
                                 .Append(_format.Formato(l.Comision.Substring(0, 8), 8, N))
                                 .Append(_format.Formato(l.Retencion.Substring(0, 8), 8, N))
                                 .Append(_format.Formato(l.Propina.Substring(0, 8), 8, N))
                                 .Append(_format.Formato(l.Num_Autoriza.Substring(0, 6), 6, A))
                                 .Append(_format.Formato(l.Nombre_Establ.Substring(0, 19), 19, A))
                                 .Append(_format.Formato((l.Responder + l.Cod_Resp).Substring(0, 4), 4, N))
                                 //.Append(l.Adquirida_Por)//RED
                                 //.Append(l.Adquirida_Para)//RED
                                 .Append(_format.Formato((l.Adquirida_Por + l.Adquirida_Para).Substring(0, 2), 2, A))
                                 .Append(_format.Formato(l.Fiid_Sponsor.Substring(0, 4), 4, A))
                                 .Append(_format.Formato(l.Iva.Substring(0, 8), 8, N))
                                 //.Append(l.Id_Fran_Hija)//franquicia
                                 //.Append(l.Filler_Fran_Hija)//franquicia
                                 .Append(_format.Formato((l.Id_Fran_Hija + l.Filler_Fran_Hija).Substring(0, 3), 3, A))
                                 .Append(_format.Formato(l.Valor_Liq_Reteica.Substring(0, 8), 8, A))
                                 .Append(_format.Formato(l.Cod_RTL.Substring(0, 10), 10, A))
                                 .Append(_format.Formato(l.Base_Devol_Iva.Substring(0, 12), 12, N))
                                 .Append(_format.Formato(entidades.FirstOrDefault(e => e.fiid.Equals(l.Fiid_Emisor))?.nombre.Substring(0, 25), 25, A))
                                 .Append(_format.Formato(entidades.FirstOrDefault(e => e.fiid.Equals(l.Fiid_Sponsor))?.nombre.Substring(0, 25), 25, A))//SPONSOR
                                 .Append(_format.Formato(l.RefUniversal.Substring(0, 23), 23, A))
                                 .Append((l.Adquirida_Por + l.Adquirida_Por).Substring(0, 1) == _2 ? _02 : _01)
                                 .Append(_format.Formato(l.ConvBonos.Substring(0, 4), 4, N))
                                 .Append(_format.Formato(l.TextoAdicional.Substring(0, 25), 25, A))
                                 .Append(_format.Formato(l.Convtrack.Substring(0, 5), 5, N))//MICOMPRA
                                 .Append(_format.Formato(Space, 4, A))//space
                                                                      //.ToString()
                               ).ToList()
                    };
                    lstCommercemodel.Add(res);
                }
            });

            return lstCommercemodel;
        }
        
        private string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }


        private string LikeToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("_", ".").Replace("%", ".*") + "$";
        }
        private string RemoveSpecialCharacters(string input)
        {

            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = r.Replace(input, String.Empty);
            input = input.Replace(" ", "_");
            return input;
        }
        private string RemoveSpecialCharactersChangeBySpace(string input)
        {

            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = r.Replace(input, String.Empty);
            return input;
        }

    }
}
