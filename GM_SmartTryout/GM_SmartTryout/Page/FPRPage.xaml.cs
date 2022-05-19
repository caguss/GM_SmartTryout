using FormsControls.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public partial class FPRPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };
        private ProjectModel selectedProject;
        public ObservableCollection<FPRModel> FPRModels { get; set; }

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
        public FPRPage()
        {
            InitializeComponent();
        }

        public FPRPage(ProjectModel param)
        {
            InitializeComponent();
            selectedProject = param;
            FPRModels = Provider.ContentList(selectedProject);
            Contentlist.ItemsSource = FPRModels;
        }

      
    }
}
