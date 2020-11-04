using core.Entities.ComerciosData;
using core.Entities.ConvertData;
using core.Entities.MasterData;
using core.Utils.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace core.UseCase.Carrefour
{
    public class GenerateCarrefourFile
    {
        private readonly FormatFileByType _format;
        public GenerateCarrefourFile()
        {
            _format = new FormatFileByType();
        }
        private const string _nit = "9001551071";
        private const string _con = "150";
        private const string _blank = "      ";
        private const string _tipo = "00";
        private const string _reteivdescu = "00000000";
        private const string _reteica = "00";
        private List<string> _franquicias = new List<string> { "009","013","007"};
        private const string _codTrans = "10";
        private const short _codResMin = 000;
        private const short _codResMax = 009;
        private const string _N = "N";


        public Dictionary<string, List<CommerceModel>> build(List<SapModel> lstSap, List<EntidadesModel> entidades)
        {
            var lst = lstSap.Where(s => s.Nit.Trim() == _nit &&
                                        _franquicias.Contains((s.Id_Fran_Hija + s.Filler_Fran_Hija)) &&
                                        s.Cod_Trans.Substring(0, 2) == _codTrans &&
                                        Convert.ToInt16(s.Cod_Resp.Substring(0, 3)) >= _codResMin &&
                                        Convert.ToInt16(s.Cod_Resp.Substring(0, 3)) <= _codResMax
                                   )
                               .Select(j => new CommerceModel
                               {
                                   Cod_RTL = new StringBuilder().Append(j.Cod_RTL.Trim()).Append("-").Append(RemoveSpecialCharacters(j.NombreCadena.Trim())).Append(j.FechaCompra).ToString(),
                                   Nit = j.Nit.Trim(),
                                   line = new StringBuilder()

                                .Append("01")
                                .Append(j.Cod_RTL.Substring(0, 10))
                                .Append(j.FechaTran.Substring(2, 2))
                                .Append(j.FechaTran.Substring(4, 2))
                                .Append(j.FechaTran.Substring(6, 2))
                                .Append(_blank)
                                .Append(j.Id_Terminal.Substring(3, 5))
                                .Append(_con)
                                .Append(j.Num_Secuen.Substring(8, 4))
                                .Append(_format.formato(j.Num_Tarjeta, 19, _N))
                                .Append(j.Num_Autoriza)
                                .Append(_tipo)
                                .Append(j.Valor)
                                .Append(_format.formato(j.Iva.Substring(0, 10), 13, _N))
                                .Append(_reteivdescu)
                                .Append(_reteivdescu)
                                .Append(j.Retencion)
                                .Append(_reteica)
                                .Append(_blank)
                                .Append(j.FechaCompra.Substring(2, 2))
                                .Append(j.FechaCompra.Substring(4, 2))
                                .Append(j.FechaCompra.Substring(6, 2))
                                .Append(j.Id_Fran_Hija + j.Filler_Fran_Hija)
                                .ToString()
                               }
                               ).ToList();
            Dictionary<string, List<CommerceModel>> dict = new Dictionary<string, List<CommerceModel>>();
            dict.Add(_nit, lst);
            return dict;
        }

        public string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }




        //public List<Object> build(List<SapModel> lstSap) 
        //{
        //    var lst = lstSap
        //        //.Where(s =>
        //    //s.Nit == _nit &&
        //    //_franquicias.Contains((s.Id_Fran_Hija + s.Filler_Fran_Hija)) &&
        //    //s.Cod_Trans.Substring(0, 2) == _codTrans  &&
        //    //Convert.ToInt16(s.Cod_Resp.Substring(1, 3)) >= _codResMin &&
        //    //Convert.ToInt16(s.Cod_Resp.Substring(1, 3)) <= _codResMax 
        //    //)
        //    .Select(s => new  
        //    {
        //        Cod_RTL= s.Cod_RTL.Substring(0,10),
        //        an1 = s.FechaTran.Substring(2, 2),
        //        m1 = s.FechaTran.Substring(4, 2),
        //        d1 = s.FechaTran.Substring(6, 2),
        //        blanc = _blank,
        //        ter = s.Id_Terminal.Substring(3, 5),
        //        con = _con,
        //        seq = s.Num_Secuen.Substring(8, 4),
        //        NUM_TARJETA = s.Num_Tarjeta,
        //        NUM_AUTORIZA = s.Num_Autoriza,
        //        tipo = _tipo,
        //        VALOR = s.Valor,
        //        VALOR_IVA = s.Iva,
        //        reteiv = _reteivdescu,
        //        descu = _reteivdescu,
        //        RETENCION = s.Retencion,
        //        RETEICA = _reteica,
        //        blanc2 =_blank,
        //        an2 = s.FechaCompra.Substring(2, 2),
        //        m2 = s.FechaCompra.Substring(4, 2),
        //        d2 = s.FechaCompra.Substring(6, 2),
        //        FRANQUICIA = s.Id_Fran_Hija + s.Filler_Fran_Hija

        //    }).ToList();
        //    return null;
        //}

        //public List<String> buildstring(List<SapModel> lstSap)
        //{
        //    var lst = lstSap
        //    .Where(s =>
        //    s.Nit.Trim() == _nit &&
        //    _franquicias.Contains((s.Id_Fran_Hija + s.Filler_Fran_Hija)) &&
        //    s.Cod_Trans.Substring(0, 2) == _codTrans &&
        //    Convert.ToInt16(s.Cod_Resp.Substring(0, 3)) >= _codResMin &&
        //    Convert.ToInt16(s.Cod_Resp.Substring(0, 3)) <= _codResMax
        //    )

        //    .Select(s => 
            
        //        string.Concat(s.Cod_RTL.Substring(0, 10),
        //        s.FechaTran.Substring(2, 2),
        //        s.FechaTran.Substring(4, 2),
        //        s.FechaTran.Substring(6, 2),
        //        _blank,
        //        s.Id_Terminal.Substring(3, 5),
        //        _con,
        //        s.Num_Secuen.Substring(8, 4),
        //        s.Num_Tarjeta,
        //        s.Num_Autoriza,
        //        _tipo,
        //        s.Valor,
        //        s.Iva,
        //        _reteivdescu,
        //        _reteivdescu,
        //        s.Retencion,
        //        _reteica,
        //        _blank,
        //        s.FechaCompra.Substring(2, 2),
        //        s.FechaCompra.Substring(4, 2),
        //        s.FechaCompra.Substring(6, 2),
        //        s.Id_Fran_Hija, s.Filler_Fran_Hija)

        //    ).ToList();
        //    return lst;
        //}
    }
}
