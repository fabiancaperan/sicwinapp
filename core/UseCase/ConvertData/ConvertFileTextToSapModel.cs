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

        public List<SapModel> Build(string filePath)
        {
            List<SapModel> sap = new List<SapModel>();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            Parallel.For(0, lines.Length, i => 
            {
                sap.Add(BuildSap(lines[i], i + 1));
            });
            //for (int i = 0; i < lines.Length; i++)
            //{
            //        sap.Add(buildSap(lines[i],i+1));
            //}
            return sap;
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
