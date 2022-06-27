using Android;
using Android.Content;
using Android.Content.PM;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using GM_SmartTryout.Droid;
using Java.Util.Zip;
using Syncfusion.XlsIO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using FileProvider = AndroidX.Core.Content.FileProvider;

[assembly: Dependency(typeof(FilesManager))]
namespace GM_SmartTryout.Droid
{
    public class FilesManager : IFilesManager
    {
        public static Context mContext;
        public ProjectModel FileCreate(string filename, string templatepath)
        {
            try
            {
                var projlistpath = Path.Combine(FileSystem.CacheDirectory, "Projects");
                if (!File.Exists(projlistpath))
                {
                    Directory.CreateDirectory(projlistpath);
                }
                var projpath = Path.Combine(projlistpath, filename + DateTime.Now.ToString("__yyMMdd"));
                var dirName = new DirectoryInfo(projpath).Name;
                if (File.Exists(projpath))
                {
                    return null;
                }

                var imagepath = Path.Combine(projpath, "Image");
                Directory.CreateDirectory(imagepath);
                var imageFPRpath = Path.Combine(imagepath, "FPR");
                var imageDynamicpath = Path.Combine(imagepath, "Dynamic");
                var imageStaticpath = Path.Combine(imagepath, "Static");
                Directory.CreateDirectory(imageFPRpath);
                Directory.CreateDirectory(imageDynamicpath);
                Directory.CreateDirectory(imageStaticpath);
                var excelpath = Path.Combine(projpath, filename + ".xlsx");
                //File.Create(excelpath);
                File.Copy(templatepath, excelpath, true);
                return new ProjectModel() {
                    ProjectName = filename,
                    GenerateDate = DateTime.Now.ToString("yy-MM-dd"),
                    foldername = dirName,
                    folderPath = projpath
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ObservableCollection<ProjectModel> ProjectList()
        {
            var projlist = new ObservableCollection<ProjectModel>();
            try
            {
                var projlistpath = Path.Combine(FileSystem.CacheDirectory, "Projects");
                if (!File.Exists(projlistpath))
                {
                    Directory.CreateDirectory(projlistpath);
                }
                string[] listarray = Directory.GetDirectories(projlistpath);
                for (int i = 0; i < listarray.Length; i++)
                {
                    var dirName = new DirectoryInfo(listarray[i]).Name;
                    projlist.Add(new ProjectModel()
                    {
                        ProjectName = dirName.Split("__")[0],
                        GenerateDate = DateTime.ParseExact(dirName.Split("__")[1], "yyMMdd", null).ToString("yy-MM-dd"),
                        foldername = dirName,
                        folderPath = listarray[i]
                    });
                }
            }
            catch (Exception ex)
            {
                projlist.Add(new ProjectModel() { ProjectName = "오류 발생", GenerateDate = $"프로그램에서 생성하지 않은 파일이 존재합니다. 제거해주세요.\n{ex.Message}" });

            }

            return projlist;
        }
        public bool DeleteProject(string filename)
        {
            try
            {
                var projlistpath = Path.Combine(FileSystem.CacheDirectory, "Projects");
                string[] listarray = Directory.GetDirectories(projlistpath);
                for (int i = 0; i < listarray.Length; i++)
                {
                    var dirName = new DirectoryInfo(listarray[i]).Name;

                    if (filename == dirName)
                    {
                        Directory.Delete(listarray[i], true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string ZipProject(string foldername)
        {

            try
            {
                var zippath = Path.Combine(FileSystem.CacheDirectory, "Zips");
                if (!File.Exists(zippath))
                {
                    Directory.CreateDirectory(zippath);
                }
                var filepath = Path.Combine(FileSystem.CacheDirectory, "Projects", foldername);

                return filepath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetFileUri(string zippath)
        {
            var uri = AndroidX.Core.Content.FileProvider.GetUriForFile(mContext,mContext.PackageName+".fileprovider", new Java.IO.File(zippath));
            return uri.Path;
        }


        //Method to save document as a file in Android and view the saved document
        public async Task LoadExcelData(string excelpath, String contentType, MemoryStream stream)
        {
            string root = null;

            if (ContextCompat.CheckSelfPermission(mContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Android.App.Activity)mContext, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

        

            Java.IO.File file = new Java.IO.File(excelpath);

            //Write the stream into the file
            Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();


            ///
            /// 엑셀보기 메소드
            ////Invoke the created file for viewing
            if (file.Exists())
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file); //모호한 참조 떴었음 FileProvider 확인할 것.
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }
        }


        public void SaveFPR(string excelpath, String contentType, MemoryStream stream)
        {
            string root = null;

            if (ContextCompat.CheckSelfPermission(mContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Android.App.Activity)mContext, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
            }



            Java.IO.File file = new Java.IO.File(excelpath);

            //Write the stream into the file
            Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();

        }
    }

  
}