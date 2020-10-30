using core.Entities.ConvertData;
using core.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.UseCase.Olimpica
{
    public class GenerateOlimpicaFile
    {
        private const string _nit = "8901074873";

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
                               .OrderBy(j => j.s.Cod_RTL)
                               .Select(j =>
                                   new StringBuilder()
                                .Append(j.s.Nit.Trim())
                                .Append(j.s.NombreCadena.Trim())
                                .Append(j.s.Id_Terminal)
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
                                .Append(j.f.nombre).ToString()
                               ).ToList();

            return lst;
        }
    }
}
