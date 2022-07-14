using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace GM_SmartTryout
{
    public static class ExportUtilties
    {
        public static async Task<bool> CompressAndExportFolder(string folderPath, string projname)
        {
            // Get a temporary cache directory
            var exportZipTempDirectory = Path.Combine(FileSystem.CacheDirectory, "Export");
            // Get a timestamped filename
            var exportZipFilename = $"{projname}_whenZip_{DateTime.Now.ToString("yyyyMMdd")}.zip";

            if (!Directory.Exists(exportZipTempDirectory))
                Directory.CreateDirectory(exportZipTempDirectory);

            var exportZipFilePath = Path.Combine(exportZipTempDirectory, exportZipFilename);

            if (File.Exists(exportZipFilePath))
            {
                File.Delete(exportZipFilePath);
            }

            ZipFile.CreateFromDirectory(folderPath, exportZipFilePath, CompressionLevel.Fastest, true);
      
            return true;
        }

        public static async Task<bool> ShareFile(string folderPath, string projname)
        {
            // Get a temporary cache directory
            var exportZipTempDirectory = Path.Combine(FileSystem.CacheDirectory, "Export");
            // Get a timestamped filename
            var exportZipFilename = $"{projname}_whenZip_{DateTime.Now.ToString("yyyyMMdd")}.zip";


            var exportZipFilePath = Path.Combine(exportZipTempDirectory, exportZipFilename);

            // Give the user the option to share this using whatever medium they like
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = projname,
                File = new ShareFile(exportZipFilePath),
            });

            return true;
        }


    }
}
