using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ConvertFileTextToSapModel
    {

        public List<SapModel> build(string filePath)
        {
            List<SapModel> sap = new List<SapModel>();
            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                sap.Add(buildSap(line));
            }

            return sap;
        }

        private SapModel buildSap(string line)
        {
            SapModel sapModel = new SapModel();
            sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (PropertyInfo prop in sapModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var length = getMaxLength(prop);
                prop.SetValue(sapModel, line.Substring(i, length).Trim(), null);
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
