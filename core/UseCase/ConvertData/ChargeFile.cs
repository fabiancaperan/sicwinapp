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

        public FileChargeModel Build(string path)
        {
            var fileCharge = new FileChargeModel();
            var ret =ValidatePath(path, out var dateOut);
            if (ret != String.Empty)
            {
                fileCharge.Message = ret;
                return fileCharge;
            }

            string[] lines = File.ReadAllLines(path);
            HashSet<string> hashSets = lines.ToHashSet();
            if (lines.Length != hashSets.Count)
            {
                fileCharge.Message = "Archivo con líneas duplicadas";
                return fileCharge;
            }

            if (ValidateFormat(lines))
            {
                fileCharge = new ConvertFileTextToSapModel().Build(lines, dateOut);
                if (!string.IsNullOrEmpty(fileCharge.Message))
                {
                    return fileCharge;
                }

                new SicContext().Save(dateOut);
                fileCharge.Message = "TRUE";
                return fileCharge;
            }
            fileCharge.Message = "Archivo no válido";
            return fileCharge;

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
