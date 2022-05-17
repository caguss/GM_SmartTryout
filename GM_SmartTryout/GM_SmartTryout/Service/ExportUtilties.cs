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
            // Delete folder incase anything from previous exports, it will be recreated later anyway
            try
            { 
                if (Directory.Exists(exportZipTempDirectory))
                {
                    Directory.Delete(exportZipTempDirectory, true);
                }
            }
            catch (Exception ex)
            {
                // Log things and move on, don't want to fail just because of a left over lock or something
                Debug.WriteLine(ex);
            }

            // Get a timestamped filename
            var exportZipFilename = $"{projname}_whenZip_{DateTime.Now.ToString("yyyyMMdd")}.zip";

            Directory.CreateDirectory(exportZipTempDirectory);

            var exportZipFilePath = Path.Combine(exportZipTempDirectory, exportZipFilename);

            if (File.Exists(exportZipFilePath))
            {
                File.Delete(exportZipFilePath);
            }
            // Zip everything up
            var test = Provider.GetFileUri(exportZipFilePath);

            ZipFile.CreateFromDirectory(folderPath, exportZipFilePath, CompressionLevel.Fastest, true);

      
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
