using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GM_SmartTryout
{
    public interface IFilesManager
    {
        ProjectModel FileSave(string filename, string templatepath);
        ObservableCollection<ProjectModel> ProjectList();
        bool DeleteProject(string filename);
        string ZipProject(string filename);
        string GetFileUri(string zippath);
    }
}
