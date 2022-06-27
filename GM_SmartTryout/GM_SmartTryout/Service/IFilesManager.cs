using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GM_SmartTryout
{
    public interface IFilesManager
    {
        ProjectModel FileCreate(string filename, string templatepath);
        ObservableCollection<ProjectModel> ProjectList();
        bool DeleteProject(string filename);
        string ZipProject(string filename);
        string GetFileUri(string zippath);
        Task LoadExcelData(string excelpath, string contentType, MemoryStream stream);
        void SaveFPR(string excelpath, String contentType, MemoryStream stream);
    }
}
