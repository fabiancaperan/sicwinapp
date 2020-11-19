using System;

namespace core.Utils.format
{
    public class FormatFileByType
    {
        private const long Unonegativo = -1;
        private const string Vacio = "";
        private const string A = "A";
        private const string N = "N";
        private const string Space = " ";
        private const string Raya = "_";
        private const string Zero = "0";
        private long _valor;

        public string Formato(string campo, int tamaño, string tipo)
        {
            try
            {
                string formateado = Vacio;
                if (tipo == A)
                {
                    return formatA(tamaño, campo);
                }
                else if (tipo == N)
                {
                    return FormatN(tamaño, campo);
                }
                return formateado;

            }

            catch (Exception)
            {
                return Vacio;
            }
        }

        private string formatA( int tamaño, string campo)
        {
            var format = campo.Trim();
            while(format.Length < tamaño)
                format += Space;
            return format;
        }
        private string FormatN(int tamaño,string campo) 
        {
            string format = Vacio;
            if (!long.TryParse(campo.Trim(), out _valor))
                return Vacio;
            if (_valor < 0)
            {
                format = Raya;
                campo = (_valor * Unonegativo).ToString();
            }
            while(format.Length < (tamaño - campo.Trim().Length))
                format += Zero;
            format += campo.Trim();
            return format;
        }
    }
}
