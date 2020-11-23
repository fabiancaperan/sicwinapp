using System;
using core.Entities.ConvertData;
using core.Repository.Sic;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        private const int LengthPath = 11;

        public string Build(string path)
        {
            var ret =ValidatePath(path, out var dateOut);
            if (ret != String.Empty)
            {
                return ret;
            }

            string[] lines = File.ReadAllLines(path);
            HashSet<string> hashSets = lines.ToHashSet();
            if (lines.Length != hashSets.Count)
            {
                return "Archivo con líneas duplicadas";
            }

            if (ValidateFormat(lines))
            {
                FileChargeModel fileChargeModel = new ConvertFileTextToSapModel().Build(lines, dateOut);
                if (!string.IsNullOrEmpty(fileChargeModel.Message))
                {
                    return fileChargeModel.Message;
                }

                //new SicContext().Save(fileChargeModel.List, dateOut);
                return "TRUE";
            }

            return "Archivo no válido";

        }

        private bool ValidateFormat(string[] lines)
        {
            if (lines.Length > 0)
                return true;
            return false;
        }
        private string ValidatePath(string path, out DateTime dateOut)
        {
            dateOut= new DateTime();
            var filename = Path.GetFileName(path);
            if (filename.Length != LengthPath)
            {
                var formatNoValid = "Nombre de archivo no válido";
                return formatNoValid;
            }

            if (!ValidateDate(filename,out dateOut))
            {
                var message = "Fecha de archivo no válida";
                return message;
            }

            return String.Empty;
        }

        private bool ValidateDate(string filename, out DateTime dateOut)
        {
            var dat =filename.Substring(4, 6);
            var dateString = $"{dat.Substring(4, 2)}/{dat.Substring(2, 2)} /{dat.Substring(0,2)}";

            return (DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateOut));

        }
    }
}
