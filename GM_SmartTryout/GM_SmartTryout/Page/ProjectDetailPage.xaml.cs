using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public partial class ProjectDetailPage : ContentPage
    {
        private ProjectModel selectedProject;
        public ProjectDetailPage()
        {
            InitializeComponent();
        }

        public ProjectDetailPage(ProjectModel param)
        {
            InitializeComponent();
            selectedProject = param;
            lbl_ProjectName.Text = param.ProjectName;
        }
        private void ProjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}
