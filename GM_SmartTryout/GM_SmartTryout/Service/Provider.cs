using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public class Provider
    {
        static IFilesManager ifm = DependencyService.Get<IFilesManager>();

    
        public static ProjectModel CreateProject(string projName, string templatePath)
        {
            try
            {
                return ifm.FileSave(projName, templatePath);
            }
            catch (Exception ex)
            {
                return new ProjectModel()
                {
                    ProjectName = "Error",
                    GenerateDate = ex.Message
                };
            }
        }
        public static ObservableCollection<ProjectModel> ProjectList()
        {
            try
            {
                return ifm.ProjectList();

            }
            catch (Exception ex)
            {
                return new ObservableCollection<ProjectModel>()
                {
                    new ProjectModel()
                    {
                         ProjectName = "Error",
                         GenerateDate = ex.Message
                    } 
                };
            }
          
        }

        public static bool DeleteProject(string filename)
        {
            try
            {
                ifm.DeleteProject(filename);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string ZipProject(string foldername)
        {
            try
            {
                return ifm.ZipProject(foldername);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetFileUri (string zippath)
        {
            try
            {
                return ifm.GetFileUri(zippath);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
