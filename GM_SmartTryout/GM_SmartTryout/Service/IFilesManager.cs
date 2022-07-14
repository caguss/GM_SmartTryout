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
        string GetFilePath(string filename);
        void ViewExcel(string excelpath);
        void SaveExcel(string excelpath, String contentType, MemoryStream stream);
        void ZipFile(string zippath, string projname);
    }
}
