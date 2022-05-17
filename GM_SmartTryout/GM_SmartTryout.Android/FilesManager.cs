using Android.Content;
using GM_SmartTryout.Droid;
using Java.Util.Zip;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilesManager))]
namespace GM_SmartTryout.Droid
{
    public class FilesManager : IFilesManager
    {
        public static Context mContext;
        public ProjectModel FileSave(string filename, string templatepath)
        {
            try
            {
                var projlistpath = Path.Combine(FileSystem.CacheDirectory, "Projects");
                if (!File.Exists(projlistpath))
                {
                    Directory.CreateDirectory(projlistpath);
                }
                var projpath = Path.Combine(projlistpath, filename + DateTime.Now.ToString("_yyMMdd"));
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
                        ProjectName = dirName.Split('_')[0],
                        GenerateDate = DateTime.ParseExact(dirName.Split('_')[1], "yyMMdd", null).ToString("yy-MM-dd"),
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
    }
}