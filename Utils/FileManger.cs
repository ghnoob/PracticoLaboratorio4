using System.IO;
using Microsoft.AspNetCore.Http;

namespace PracticoLaboratorio4.Utils
{
    public static class FileManager
    {
        public static string Create(IFormFile file, string destinationFolderPath)
        {
            string outputFileName = Path.GetRandomFileName() + (Path.GetExtension(file.FileName)),
                outputPath = Path.Combine(destinationFolderPath, outputFileName);

            using (FileStream fs = new(outputPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return outputFileName;
        }

        public static void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
