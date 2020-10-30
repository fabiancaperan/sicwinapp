using core.Entities.ConvertData;
using core.Repository.Sic;
using core.UseCase.Carrefour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace core.UseCase.DownloadData
{
    public class DownloadFiles
    {
        public bool build(string rute, List<Repository.Types.CommerceType> subfolder)
        {
            foreach (var item in subfolder)
            {
                string pathString = Path.Combine(rute, item.ToString());
                Directory.CreateDirectory(pathString);
                pathString = Path.Combine(pathString, item.ToString());
                List<SapModel> lstSap = new SicContext().getAll();
                List<String> filelst = new GenerateCarrefourFile().buildstring(lstSap);

                // Create a FileStream with mode CreateNew  
                FileStream stream = new FileStream(pathString, FileMode.OpenOrCreate);
                // Create a StreamWriter from FileStream  
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    filelst.ForEach(s =>writer.WriteLine(s));
                }
                
            }
            
            return true;
        }

    }
}
