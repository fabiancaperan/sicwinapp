﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            HashSet<string> hashSets = LinesDuplicate(lines);
            var numDuplicates = lines.Length - hashSets.Count;
            if (numDuplicates != 0)
            {
                fileCharge.Message = "Archivo con " + numDuplicates + " líneas duplicadas";
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

        private HashSet<string> LinesDuplicate(IEnumerable<string> lines)
        {
            HashSet<string> hashSets = new HashSet<string>();
            List<string> duplicates = new List<string>();
            foreach (var line in lines)
            {
                if (!hashSets.Add(line))
                {
                    duplicates.Add(line);
                }
            }
            if (duplicates.Any())
                SaveDuplicates(duplicates);
            return hashSets;
        }

        private void SaveDuplicates(List<string> duplicates)
        {
            var rute = Directory.GetCurrentDirectory();

            var path = Path.Combine(rute, "duplicates");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, "duplicates");
            if (Directory.Exists(path))
                Directory.Delete(path);
            using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            duplicates.ForEach(s =>
            {
                if (writer != null) writer.WriteLine(s);
            });
        }


        private bool ValidateFormat(IReadOnlyCollection<string> lines)
        {
            if (lines.Count > 0)
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
            if (Int32.TryParse("20" + dat.Substring(0, 2), out var year) &&
                Int32.TryParse(dat.Substring(2, 2), out var month) &&
                Int32.TryParse(dat.Substring(4, 2), out var day)
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
