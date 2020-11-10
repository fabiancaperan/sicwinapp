using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utils.format
{
    public class BuildText
    {
        private  List<string> characteres = new List<string>(){"*", "&", "$", "%", "!", "¡", "?", "¿", "|", "+", "~", "^", "{", "}", "[", "]", "¨", "°", "¬", "@", ";", ",", ":", ".", "Ñ", "ñ", "´", "`", "<", ">", "=", "(", ")", "/", "\\",  "#", "'", "\""};
        public string build(string nombre)
        {
            string cadena = nombre;

            if (nombre.Trim() == String.Empty)
                cadena = "(Sin nombre)";
            
                cadena = nombre;
            cadena = cadena.Replace(" ","_");
            characteres.ForEach(s => 
            {
                cadena = cadena.Replace(s, String.Empty);
            });
            return cadena;
        }

    }
}
