using System;
using System.Collections.Generic;
using System.Text;

namespace core.UseCase.DownloadData
{
    public class DownloadFiles
    {
        public bool build(string rute, List<string> subfolder)
        {
            foreach (var item in subfolder)
            {
                string pathString = System.IO.Path.Combine(rute, item);
                System.IO.Directory.CreateDirectory(pathString);
                pathString = System.IO.Path.Combine(pathString, item);
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {

                }
            }

            return true;
        }

    }
}
