using core.Entities.ConvertData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace core.UseCase.ConvertData
{
    public class ConvertFileTextToSapModel
    {

        public List<sapModel> build(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < 3; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);
            
            
            foreach (string line in lines)
            {
                var parts = SplitInParts(line,123).ToList();
                var cols = line.Split(':');

                DataRow dr = tbl.NewRow();
                for (int i = 0; i < parts.Count(); i++)
                {
                    dr[i] = parts[i];
                }
                tbl.Rows.Add(dr);
            }

            return null;
        }

        private IEnumerable<String> SplitInParts(String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
}
