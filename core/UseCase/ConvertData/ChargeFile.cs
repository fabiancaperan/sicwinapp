using core.Entities.ConvertData;
using core.Repository.Sic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.UseCase.ConvertData
{
    public class ChargeFile
    {
        public string Build(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            if (ValidateFormat(lines))
            {
                FileChargeModel fileChargeModel = new ConvertFileTextToSapModel().Build(lines);
                if (!string.IsNullOrEmpty(fileChargeModel.Message))
                {
                    return fileChargeModel.Message;
                }

                new SicContext().Save(fileChargeModel.List);
                return "TRUE";
            }

            return "Archivo no válido";

        }

        private bool ValidateFormat(string[] lines)
        {
            if (lines.Length > 0)
                return true;
            return false;
            //Parallel.ForEach(lines, (s, state) =>
            //{
            //    if (s.Length > 371 && s.Length < 360)
            //        state.Break();

            //});
            //return true;
        }
    }
}
