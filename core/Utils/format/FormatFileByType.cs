using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utils.format
{
    public class FormatFileByType
    {
        private readonly long _unonegativo = -1;
        private readonly string _vacio = "";
        private readonly string A = "A";
        private readonly string N = "N";
        private readonly string _space = " ";
        private readonly string _raya = "_";
        private readonly string _zero = "0";
        private long _valor = 0;

        public string formato(string campo, int tamaño, string tipo)
        {
            try
            {
                string formateado = _vacio;
                if (tipo == A)
                {
                    return formatA(tamaño, campo);
                }
                else if (tipo == N)
                {
                    return formatN(tamaño, campo);
                }
                return formateado;

            }
            catch (Exception ex)
            {
                return _vacio;
            }
        }

        private string formatA( int tamaño, string campo)
        {
            var format = campo.Trim();
            while(format.Length < tamaño)
                format += _space;
            return format;
        }
        private string formatN(int tamaño,string campo) 
        {
            string format = _vacio;
            if (!long.TryParse(campo.Trim(), out _valor))
                return _vacio;
            if (_valor < 0)
            {
                format = _raya;
                campo = (_valor * _unonegativo).ToString();
            }
            while(format.Length < (tamaño - campo.Trim().Length))
                format += _zero;
            format += campo.Trim();
            return format;
        }
    }
}
