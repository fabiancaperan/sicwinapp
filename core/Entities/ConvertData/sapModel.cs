using System.ComponentModel.DataAnnotations;

namespace core.Entities.ConvertData
{
    public class SapModel
    {
     
        [Display(Name = "Tipo Registro")]
        [MaxLength(2)]
        public string TipoRegistro { get; set; }
        
        [Display(Name = "Fecha Compra")]
        [MaxLength(8)]
        public string FechaCompra { get; set; }
        [Key]
        [Display(Name = "Nit")]
        [MaxLength(13)]
        public string Nit { get; set; }

        [Display(Name = "Nombre Cadena")]
        [MaxLength(30)]
        public string NombreCadena { get; set; }

        [Display(Name = "Cod_RTL")]
        [MaxLength(19)]
        public string Cod_RTL { get; set; }

        [Display(Name = "Id_Terminal")]
        [MaxLength(16)]
        public string Id_Terminal { get; set; }

        //Ciudad
        [Display(Name = "COD_DANE")]
        [MaxLength(8)]
        public string COD_DANE { get; set; }

        [Display(Name = "FechaTran")]
        [MaxLength(8)]
        public string FechaTran { get; set; }

        [Display(Name = "HoraTran")]
        [MaxLength(6)]
        public string HoraTran { get; set; }

        [Display(Name = "Fiid_Emisor")]
        [MaxLength(4)]
        public string Fiid_Emisor { get; set; }

        [Display(Name = "Abrev_Emisor")]
        [MaxLength(3)]
        public string Abrev_Emisor { get; set; }

        [Display(Name = "Num_Tarjeta")]
        [MaxLength(4)]
        public string Num_Tarjeta { get; set; }
        
        [Display(Name = "Tipo_Mensaje")]
        [MaxLength(4)]
        public string Tipo_Mensaje { get; set; }
        
        [Display(Name = "Cod_Trans")]
        [MaxLength(6)]
        public string Cod_Trans { get; set; }
        [Key]
        [Display(Name = "Num_Secuen")]
        [MaxLength(12)]
        public string Num_Secuen { get; set; }

        [Display(Name = "Valor")]
        [MaxLength(12)]
        public string Valor { get; set; }

        [Display(Name = "Comision")]
        [MaxLength(8)]
        public string Comision { get; set; }

        [Display(Name = "Retencion")]
        [MaxLength(8)]
        public string Retencion { get; set; }

        [Display(Name = "Propina")]
        [MaxLength(8)]
        public string Propina { get; set; }

        [Display(Name = "Num_Autoriza")]
        [MaxLength(6)]
        public string Num_Autoriza { get; set; }

        [Display(Name = "Nombre_Establ")]
        [MaxLength(19)]
        public string Nombre_Establ { get; set; }

        [Display(Name = "Responder")]
        [MaxLength(1)]
        public string Responder { get; set; }

        [Display(Name = "Cod_Resp")]
        [MaxLength(3)]
        public string Cod_Resp { get; set; }

        [Display(Name = "Adquirida_Por")]
        [MaxLength(1)]
        public string Adquirida_Por { get; set; }

        [Display(Name = "Adquirida_Para")]
        [MaxLength(1)]
        public string Adquirida_Para { get; set; }

        [Display(Name = "Fiid_Sponsor")]
        [MaxLength(4)]
        public string Fiid_Sponsor { get; set; }

        [Display(Name = "Iva")]
        [MaxLength(8)]
        public string Iva {get; set; }

        [Display(Name = "Id_Fran_Hija")]
        [MaxLength(1)]
        public string Id_Fran_Hija { get; set; }

        [Display(Name = "Filler_Fran_Hija")]
        [MaxLength(2)]
        public string Filler_Fran_Hija { get; set; }

        [Display(Name = "Valor_Liq_Reteica")]
        [MaxLength(10)]
        public string Valor_Liq_Reteica { get; set; }

        [Display(Name = "Base_Devol_Iva")]
        [MaxLength(12)]
        public string Base_Devol_Iva { get; set; }
        
        [Display(Name = "RefUniversal")]
        [MaxLength(23)]
        public string RefUniversal { get; set; }

        [Display(Name = "ConvBonos")]
        [MaxLength(4)]
        public string ConvBonos { get; set; }

        [Display(Name = "TextoAdicional")]
        [MaxLength(25)]
        public string TextoAdicional { get; set; }

        [Display(Name = "Convtrack")]
        [MaxLength(5)]
        public string Convtrack { get; set; }

        [Display(Name = "Imp_Consumo")]
        [MaxLength(12)]
        public string Imp_Consumo { get; set; }

        [Display(Name = "Modo_Ingreso")]
        [MaxLength(3)]
        public string Modo_Ingreso { get; set; }

        [Display(Name = "Bin_Tarjeta")]
        [MaxLength(9)]
        public string Bin_Tarjeta { get; set; }

        [Display(Name = "Filler")]
        [MaxLength(41)]
        public string Filler { get; set; }
    }
}
