using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.UseCase.Comercios
{
    public class GenerateComerciosFile
    {
        private readonly FormatFileByType _format;
        public GenerateComerciosFile()
        {
            _format = new FormatFileByType();
        }
        private const string _nit = "8901074873";
        private const string _A = "A";
        private const string _N = "N";
        private const string _01 = "0001";
        private const string _02 = "0002";
        private const string _2 = "2";


        public List<String> build(List<SapModel> lstSap, List<EntidadesModel> entidades)
        {


            var lst = lstSap.Where(s => s.Nit.Trim() == _nit)
                       .Join(entidades,
                              post => post.Fiid_Emisor,
                              meta => meta.fiid,
                              (s, e) => new { s, e })
                        .Join(entidades,
                              se => se.s.Fiid_Sponsor,
                              f => f.fiid,
                              (se, f) => new { se.s, se.e, f })
                       .Where(j => j.s.Cod_RTL.Contains(j.e.fiid.PadRight(3)))
                              .OrderBy(j => j.s.Cod_RTL)
                              .Select(j => new
                              {
                                  j.s.Cod_RTL,
                                  j.s.Nit,
                                  j.s.NombreCadena,
                                  d = new StringBuilder()
                               .Append(_format.formato(j.s.Id_Terminal.Substring(0, 16), 16, _A))
                               .Append(_format.formato(j.s.COD_DANE.Substring(0, 8), 8, _A))
                               .Append(_format.formato(j.s.FechaTran.Substring(0, 8), 8, _A))
                               .Append(_format.formato(j.s.HoraTran.Substring(0, 6), 6, _A))
                               .Append(_format.formato(j.s.Fiid_Emisor.Substring(0, 4), 4, _A))
                               .Append(_format.formato(j.s.Abrev_Emisor.Substring(0, 3), 3, _A))//FID_EMISOR
                               .Append(_format.formato(j.s.Num_Tarjeta.Substring(0, 4), 4, _N))
                               .Append(_format.formato(j.s.Tipo_Mensaje.Substring(0, 4), 4, _N))
                               .Append(_format.formato(j.s.Cod_Trans.Substring(0, 6), 6, _N))
                               .Append(_format.formato(j.s.Num_Secuen.Substring(0, 12), 12, _N))
                               .Append(_format.formato(j.s.Valor.Substring(0, 12), 12, _N))
                               .Append(_format.formato(j.s.Comision.Substring(0, 8), 8, _N))
                               .Append(_format.formato(j.s.Retencion.Substring(0, 8), 8, _N))
                               .Append(_format.formato(j.s.Propina.Substring(0, 8), 8, _N))
                               .Append(_format.formato(j.s.Num_Autoriza.Substring(0, 6), 6, _A))
                               .Append(_format.formato(j.s.Nombre_Establ.Substring(0, 19), 19, _A))
                               .Append(_format.formato(j.s.Cod_Resp.Substring(0, 4), 4, _N))
                               //.Append(j.s.Adquirida_Por)//RED
                               //.Append(j.s.Adquirida_Para)//RED
                               .Append(_format.formato((j.s.Adquirida_Por + j.s.Adquirida_Para).Substring(0, 2), 2, _A))
                               .Append(_format.formato(j.s.Fiid_Sponsor.Substring(0, 4), 4, _A))
                               .Append(_format.formato(j.s.Iva.Substring(0, 8), 8, _N))
                               //.Append(j.s.Id_Fran_Hija)//franquicia
                               //.Append(j.s.Filler_Fran_Hija)//franquicia
                               .Append(_format.formato((j.s.Id_Fran_Hija + j.s.Filler_Fran_Hija).Substring(0, 1), 1, _A))
                               .Append(_format.formato(j.s.Valor_Liq_Reteica.Substring(0, 8), 8, _A))
                               .Append(_format.formato(j.s.Cod_RTL.Substring(1, 10), 10, _A))
                               .Append(_format.formato(j.s.Base_Devol_Iva.Substring(0, 12), 12, _N))
                               .Append(_format.formato(j.e.nombre.Substring(0, 25), 25, _A))
                               .Append(_format.formato(j.f.nombre.Substring(0, 25), 25, _A))//SPONSOR
                               .Append(_format.formato(j.s.RefUniversal.Substring(0, 23), 23, _A))
                               .Append((j.s.Adquirida_Por + j.s.Adquirida_Por).Substring(0, 1) == _2 ? _02 : _01)
                               .Append(_format.formato(j.s.ConvBonos.Substring(0, 4), 4, _N))
                               .Append(_format.formato(j.s.TextoAdicional.Substring(0, 25), 25, _A))
                               .Append(_format.formato(j.s.Convtrack.Substring(0, 5), 5, _N))//MICOMPRA
                               .ToString()
                              }
                              ).GroupBy(s => s.Cod_RTL)
                              .ToDictionary(s => s.Key, s => s.ToList());
                               
            return null;
        }

    }
}
