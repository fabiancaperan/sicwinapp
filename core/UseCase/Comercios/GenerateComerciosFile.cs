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
                               .Append(j.s.COD_DANE)
                               .Append(j.s.FechaTran)
                               .Append(j.s.Fiid_Emisor)
                               .Append(j.s.Abrev_Emisor)//FID_EMISOR
                               .Append(j.s.Num_Tarjeta)
                               .Append(j.s.Tipo_Mensaje)
                               .Append(j.s.Cod_Trans)
                               .Append(j.s.Num_Secuen)
                               .Append(j.s.Valor)
                               .Append(j.s.Comision)
                               .Append(j.s.Retencion)
                               .Append(j.s.Propina)
                               .Append(j.s.Num_Autoriza)
                               .Append(j.s.Nombre_Establ)
                               .Append(j.s.Cod_Resp)
                               .Append(j.s.Adquirida_Por)//RED
                               .Append(j.s.Adquirida_Para)//RED
                               .Append(j.s.Fiid_Sponsor)
                               .Append(j.s.Iva)
                               .Append(j.s.Id_Fran_Hija)//franquicia
                               .Append(j.s.Filler_Fran_Hija)//franquicia
                               .Append(j.s.Valor_Liq_Reteica)
                               .Append(j.s.Cod_RTL)
                               .Append(j.s.Base_Devol_Iva)
                               .Append(j.e.nombre)
                               .Append(j.f.nombre)//SPONSOR
                               .Append(j.s.RefUniversal)
                               .Append((j.s.Adquirida_Por + j.s.Adquirida_Por).Substring(0, 1) == _2 ? _02 : _01)
                               .Append(j.s.ConvBonos)
                               .Append(j.s.TextoAdicional)
                               .Append(j.s.Convtrack)//MICOMPRA
                               .ToString()
                              }
                              ).GroupBy(s => s.Cod_RTL)
                              .ToDictionary(s => s.Key, s => s.ToList());
                               
            return null;
        }

    }
}
