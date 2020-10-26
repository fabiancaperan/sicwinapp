using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Entities.ConvertData
{
    public class sapModel
    {
        [Display(Name = "Tipo Registro")]
        [MaxLength(2)]
        public int TipoRegistro { get; set; }

        [Display(Name = "Fecha Compra")]
        [MaxLength(8)]
        public DateTime FechaCompra { get; set; }

        [Display(Name = "Fecha Compra")]
        [MaxLength(13)]
        public int Nit { get; set; }

        [Display(Name = "Nombre Cadena")]
        [MaxLength(30)]
        public string NombreCadena { get; set; }

        [Display(Name = "Cod_RTL")]
        [MaxLength(19)]
        public int Cod_RTL { get; set; }

        [Display(Name = "Id_Terminal")]
        [MaxLength(16)]
        public string Id_Terminal { get; set; }

        //Ciudad
        [Display(Name = "COD_DANE")]
        [MaxLength(8)]
        public int COD_DANE { get; set; }

        [Display(Name = "FechaTran")]
        [MaxLength(8)]
        public DateTime FechaTran { get; set; }

        [Display(Name = "HoraTran")]
        [MaxLength(6)]
        public int HoraTran { get; set; }

        [Display(Name = "Fiid_Emisor")]
        [MaxLength(4)]
        public int Fiid_Emisor { get; set; }

        [Display(Name = "Abrev_Emisor")]
        [MaxLength(3)]
        public string Abrev_Emisor { get; set; }

        [Display(Name = "Num_Tarjeta")]
        [MaxLength(4)]
        public int Num_Tarjeta { get; set; }

        [Display(Name = "Tipo_Mensaje")]
        [MaxLength(4)]
        public char Tipo_Mensaje { get; set; }

        [Display(Name = "Cod_Trans")]
        [MaxLength(6)]
        public int Cod_Trans { get; set; }

        [Display(Name = "Num_Secuen")]
        [MaxLength(12)]
        public int Num_Secuen { get; set; }

        [Display(Name = "Valor")]
        [MaxLength(12)]
        public int Valor { get; set; }

        [Display(Name = "Comision")]
        [MaxLength(8)]
        public int Comision { get; set; }

        [Display(Name = "Retencion")]
        [MaxLength(8)]
        public int Retencion { get; set; }

        [Display(Name = "Propina")]
        [MaxLength(8)]
        public int Propina { get; set; }

        [Display(Name = "Num_Autoriza")]
        [MaxLength(6)]
        public int Num_Autoriza { get; set; }

        [Display(Name = "Nombre_Establ")]
        [MaxLength(19)]
        public char Nombre_Establ { get; set; }

        [Display(Name = "Responder")]
        [MaxLength(1)]
        public int Responder { get; set; }

        [Display(Name = "Cod_Resp")]
        [MaxLength(3)]
        public int Cod_Resp { get; set; }

        [Display(Name = "Adquirida_Por")]
        [MaxLength(1)]
        public int Adquirida_Por { get; set; }

        [Display(Name = "Adquirida_Para")]
        [MaxLength(1)]
        public int Adquirida_Para { get; set; }

        [Display(Name = "Fiid_Sponsor")]
        [MaxLength(4)]
        public int Fiid_Sponsor { get; set; }

        [Display(Name = "Iva")]
        [MaxLength(8)]
        public int Iva {get; set; }

        [Display(Name = "Id_Fran_Hija")]
        [MaxLength(1)]
        public int Id_Fran_Hija { get; set; }

        [Display(Name = "Filler_Fran_Hija")]
        [MaxLength(2)]
        public int Filler_Fran_Hija { get; set; }

        [Display(Name = "Valor_Liq_Reteica")]
        [MaxLength(10)]
        public decimal Valor_Liq_Reteica { get; set; }

        [Display(Name = "Base_Devol_Iva")]
        [MaxLength(12)]
        public int Base_Devol_Iva { get; set; }

        [Display(Name = "RefUniversal")]
        [MaxLength(23)]
        public int RefUniversal { get; set; }

        [Display(Name = "ConvBonos")]
        [MaxLength(4)]
        public int ConvBonos { get; set; }

        [Display(Name = "TextoAdicional")]
        [MaxLength(25)]
        public string TextoAdicional { get; set; }

        [Display(Name = "Convtrack")]
        [MaxLength(5)]
        public int Convtrack { get; set; }

        [Display(Name = "Imp_Consumo")]
        [MaxLength(12)]
        public int Imp_Consumo { get; set; }

        [Display(Name = "Modo_Ingreso")]
        [MaxLength(3)]
        public int Modo_Ingreso { get; set; }

        [Display(Name = "Bin_Tarjeta")]
        [MaxLength(9)]
        public int Bin_Tarjeta { get; set; }

        [Display(Name = "Filler")]
        [MaxLength(41)]
        public int Filler { get; set; }
    }
}
