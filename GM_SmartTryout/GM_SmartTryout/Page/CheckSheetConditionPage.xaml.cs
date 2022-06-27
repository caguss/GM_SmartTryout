using FormsControls.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public partial class CheckSheetConditionPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };
        private ProjectModel selectedProject;
        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public async void OnAnimationFinished(bool isPopAnimation)
        {
            await Task.WhenAll<bool>
                (
                    lbl_ProjectName.FadeTo(1, 400),

                lbl_ProjectName.TranslateTo(0, 10, 400)
                );

            // Put your code here but leaving empty works just fine
        }
        public CheckSheetConditionPage()
        {
            InitializeComponent();
        }

        public CheckSheetConditionPage(ProjectModel param, string checksheet)
        {
            InitializeComponent();
            selectedProject = param;
            switch (checksheet)
            {
                case "D":
                    lbl_ProjectName.Text = "Dynamic Check";

                    break;
                case "S":
                    lbl_ProjectName.Text = "Static Check";

                    break;
                default:
                    break;
            }


        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new FPRPage(selectedProject)));
        }
    }
}
