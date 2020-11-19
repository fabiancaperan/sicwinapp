using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace core.UseCase.Olimpica
{
    public class GenerateOlimpicaFile
    {
        private readonly FormatFileByType _format;
        public GenerateOlimpicaFile()
        {
            _format = new FormatFileByType();
        }
        private const string Nit = "8901074873";
        private const string A = "A";
        private const string N = "N";
        private const string _01 = "0001";
        private const string _02 = "0002";
        private const string _2 = "2";
        private const string Space = " ";

        public List<CommerceModel> Build(List<SapModel> lstSap, List<EntidadesModel> entidades)
        {
            var lst = lstSap.Where(s => s.Nit.Trim() == Nit)
                        .Join(entidades,
                               post => post.Fiid_Emisor,
                               meta => meta.fiid,
                               (s, e) => new { s, e })
                        .Join(entidades,
                               se => se.s.Fiid_Sponsor,
                               f => f.fiid,
                               (se, f) => new { se.s, se.e, f })
                               .GroupBy(g => new { Rtl = g.s.Cod_RTL.Trim(), Nit = g.s.Nit.Trim() })
                              .Select(j => new CommerceModel
                              {
                                  Rtl = j.Key.Rtl,
                                  Nit = 1+j.Key.Nit,
                                  Line = new StringBuilder().Append("02").Append(j.FirstOrDefault()?.s.FechaCompra)
                                                            .Append(_format.formato(j.FirstOrDefault()?.s.Nit.Trim(), 13, A)).Append(_format.formato(RemoveSpecialCharacters(j.FirstOrDefault()?.s.NombreCadena.Trim()), 30, A))
                                                            .Append("RMC").Append(new String(' ', 244)).ToString(),
                                  CodRtl = new StringBuilder().Append(j.FirstOrDefault()?.s.Cod_RTL.Trim()).Append("-").Append(RemoveSpecialCharacters(j.FirstOrDefault()?.s.NombreCadena.Trim()))
                                                                .Append("-").Append(j.FirstOrDefault()?.s.FechaCompra).Append("-").Append(j.FirstOrDefault()?.s.Nit.Trim()).ToString(),
                                  Lst = j.Select(l =>
                                  new StringBuilder()
                                 .Append("01")
                                 .Append(_format.formato(l.s.Id_Terminal.Substring(0, 16), 16, A))
                                 .Append(_format.formato(l.s.COD_DANE.Substring(0, 8), 8, A))
                                 .Append(_format.formato(l.s.FechaTran.Substring(0, 8), 8, A))
                                 .Append(_format.formato(l.s.HoraTran.Substring(0, 6), 6, A))
                                 .Append(_format.formato(l.s.Fiid_Emisor.Substring(0, 4), 4, A))
                                 .Append(_format.formato(l.s.Abrev_Emisor.Substring(0, 3), 3, A))//FID_EMISOR
                                 .Append(_format.formato(l.s.Num_Tarjeta.Substring(0, 4), 4, N))
                                 .Append(_format.formato(l.s.Tipo_Mensaje.Substring(0, 4), 4, N))
                                 .Append(_format.formato(l.s.Cod_Trans.Substring(0, 6), 6, N))
                                 .Append(_format.formato(l.s.Num_Secuen.Substring(0, 12), 12, N))
                                 .Append(_format.formato(l.s.Valor.Substring(0, 12), 12, N))
                                 .Append(_format.formato(l.s.Comision.Substring(0, 8), 8, N))
                                 .Append(_format.formato(l.s.Retencion.Substring(0, 8), 8, N))
                                 .Append(_format.formato(l.s.Propina.Substring(0, 8), 8, N))
                                 .Append(_format.formato(l.s.Num_Autoriza.Substring(0, 6), 6, A))
                                 .Append(_format.formato(l.s.Nombre_Establ.Substring(0, 19), 19, A))
                                 .Append(_format.formato((l.s.Responder + l.s.Cod_Resp).Substring(0, 4), 4, N))
                                 //.Append(l.s.Adquirida_Por)//RED
                                 //.Append(l.s.Adquirida_Para)//RED
                                 .Append(_format.formato((l.s.Adquirida_Por + l.s.Adquirida_Para).Substring(0, 2), 2, A))
                                 .Append(_format.formato(l.s.Fiid_Sponsor.Substring(0, 4), 4, A))
                                 .Append(_format.formato(l.s.Iva.Substring(0, 8), 8, N))
                                 //.Append(l.s.Id_Fran_Hija)//franquicia
                                 //.Append(l.s.Filler_Fran_Hija)//franquicia
                                 .Append(_format.formato((l.s.Id_Fran_Hija + l.s.Filler_Fran_Hija).Substring(0, 3), 3, A))
                                 .Append(_format.formato(l.s.Valor_Liq_Reteica.Substring(0, 8), 8, A))
                                 .Append(_format.formato(l.s.Cod_RTL.Substring(0, 10), 10, A))
                                 .Append(_format.formato(l.s.Base_Devol_Iva.Substring(0, 12), 12, N))
                                 .Append(_format.formato(l.e.nombre.Substring(0, 25), 25, A))
                                 .Append(_format.formato(l.f.nombre.Substring(0, 25), 25, A))//SPONSOR
                                 .Append(_format.formato(l.s.RefUniversal.Substring(0, 23), 23, A))
                                 .Append((l.s.Adquirida_Por + l.s.Adquirida_Por).Substring(0, 1) == _2 ? _02 : _01)
                                 .Append(_format.formato(l.s.ConvBonos.Substring(0, 4), 4, N))
                                 .Append(_format.formato(l.s.TextoAdicional.Substring(0, 25), 25, A))
                                 .Append(_format.formato(l.s.Convtrack.Substring(0, 5), 5, N))//MICOMPRA
                                 .Append(_format.formato(Space, 4, A))//space
                                                                        //.ToString()
                               ).ToList()
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
