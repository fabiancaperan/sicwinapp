using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using core.Entities.ConvertData;
using core.Repository.Sic;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        private const int LengthPath = 11;

        public FileChargeModel Build(string path)
        {
            var fileCharge = new FileChargeModel();
            var ret = ValidatePath(path, out var dateOut);
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
        public string ValidatePath(string path, out DateTime dateOut)
        {
            dateOut = new DateTime();
            var filename = Path.GetFileName(path);
            if (filename.Length != LengthPath)
            {
                var formatNoValid = "Nombre de archivo no válido";
                return formatNoValid;
            }

            if (!ValidateDate(filename, out dateOut))
            {
                var message = "Fecha de archivo no válida";
                return message;
            }

            return String.Empty;
        }

        private bool ValidateDate(string filename, out DateTime dateOut)
        {
            var dat = filename.Substring(4, 6);
            int month;
            int day;
            int year;
            if (Int32.TryParse("20" + dat.Substring(0, 2), out year) &&
                Int32.TryParse(dat.Substring(2, 2), out month) &&
                Int32.TryParse(dat.Substring(4, 2), out day)
                )
            {
                dateOut = new DateTime(year, month, day);
                return true;
            }
            dateOut = DateTime.Now;
            return false;
        }
    }
}
