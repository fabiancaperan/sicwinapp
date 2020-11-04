using core.Entities.ConvertData;
using core.Repository;
using core.Repository.Sic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core.UseCase.ConvertData
{
    public class ConvertFileTextToSapModel
    {
        const int _lengthLine = 369;

        public List<SapModel> build(string filePath)
        {
            List<SapModel> sap = new List<SapModel>();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            Parallel.For(0, lines.Length, i => 
            {
                sap.Add(buildSap(lines[i], i + 1));
            });
            //for (int i = 0; i < lines.Length; i++)
            //{
            //        sap.Add(buildSap(lines[i],i+1));
            //}
            return sap;
        }

        private SapModel buildSap(string line,int id)
        {
            var dif = 0;
            if (line.Length != _lengthLine)
            {
                dif = _lengthLine - line.Length;

            }
            SapModel sapModel = new SapModel();
            sapModel.Id = id;
            sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (PropertyInfo prop in sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.Name == "Id")
                    continue;
                
                var length = dif != 0 && prop.Name == "NombreCadena" ? getMaxLength(prop) - dif : getMaxLength(prop);
                prop.SetValue(sapModel, line.Substring(i, length), null);
                ;
                i += length;
            }
            return sapModel;
        }

        private int getMaxLength(PropertyInfo propInfo)
        {
            MaxLengthAttribute attrMaxLength = (MaxLengthAttribute)propInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
            return attrMaxLength != null ? attrMaxLength.Length : 0;
        }
    }
}
