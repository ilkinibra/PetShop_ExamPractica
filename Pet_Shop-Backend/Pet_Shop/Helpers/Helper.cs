using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Pet_Shop.Helpers
{
    public class Helper
    {

        public static void DeleteImage(IWebHostEnvironment webhost, string folder, string filename)
        {
            string path = webhost.WebRootPath;
            string resultPath = Path.Combine(path, folder, filename);
            if (System.IO.File.Exists(resultPath))
            {
                System.IO.File.Delete(resultPath);
            }
        }

        internal static void DeleteImage(object webhost, string v, string ımageUrl)
        {
            throw new NotImplementedException();
        }
    }
}
