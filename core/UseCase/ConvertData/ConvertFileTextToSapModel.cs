using core.Entities.ConvertData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace core.UseCase.ConvertData
{
    public class ConvertFileTextToSapModel
    {
        const int LengthLine = 369;
        //private const string noLength = "El archivo tiene líneas que no estan entre 360 y 371 caracteres";
        private const string NoLength = "El archivo tiene líneas que son vacías o no tienen el formato válido";
        private const string NodatesValid = "El archivo tiene campos de fecha con formato no válido";
        private readonly List<string> _lstDates = new List<string>(){ "FechaCompra", "FechaTran" }; 

        public FileChargeModel Build(string[] lines)
        {
            var ret = new FileChargeModel();
            List<SapModel> sap = new List<SapModel>();
            Parallel.For(0, lines.Length, (i,state) =>
            {
                var le = lines[i].Length;
                if (le > 373 || le < 360)
                {
                    ret.Message = NoLength;
                    state.Break();
                    return;
                }

                var sa = BuildSap(lines[i], i + 1);
                if (sa == null)
                {
                    ret.Message = NodatesValid;
                    state.Break();
                    return;
                }
                sap.Add(sa);
            });
            
            
            ret.List = sap;
            return ret;
        }

        private SapModel BuildSap(string line,int id)
        {
            var dif = 0;
            if (line.Length != LengthLine)
            {
                dif = LengthLine - line.Length;

            }

            SapModel sapModel = new SapModel {Id = id};
            var propertyInfos = sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (PropertyInfo prop in propertyInfos)
            {
                if (prop.Name == "Id")
                    continue;

                var length = dif != 0 && prop.Name == "NombreCadena" ? GetMaxLength(prop) - dif : GetMaxLength(prop);
                if (_lstDates.Contains(prop.Name))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(line.Substring(i, length), "^[0-9]*$"))
                    {

                        return null;
                    }
                }
                prop.SetValue(sapModel, line.Substring(i, length), null);
                i += length;
            }
            return sapModel;
        }

        private int GetMaxLength(PropertyInfo propInfo)
        {
            MaxLengthAttribute attrMaxLength = (MaxLengthAttribute)propInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
            return attrMaxLength?.Length ?? 0;
        }
    }
}
