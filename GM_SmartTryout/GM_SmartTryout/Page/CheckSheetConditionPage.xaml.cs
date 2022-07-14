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
        public bool LoadingFinish = false;
        public bool isDynamic = false;

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

        public CheckSheetConditionPage(ProjectModel param, bool isdynamic)
        {
            InitializeComponent();
            selectedProject = param;
            if (isdynamic)
            {
                lbl_ProjectName.Text = "Dynamic Check";
                isDynamic = true;
            }
            else
            {
                lbl_ProjectName.Text = "Static Check";
                isDynamic = false;

            }

            LoadingFinish = true;

        }

        CheckSheetPage ChecklistPage = null;
        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            if (RadioButtonGroup.GetSelectedValue(grid_IF) == null || RadioButtonGroup.GetSelectedValue(grid_CS) == null || RadioButtonGroup.GetSelectedValue(grid_TIMES) == null)
            {
                await DisplayAlert("알림","작업하실 분류를 선택해 주세요.","확인");
                return;
            }


            ConditionModel cm = new ConditionModel();
            cm.IF = RadioButtonGroup.GetSelectedValue(grid_IF).ToString();
            cm.CS = RadioButtonGroup.GetSelectedValue(grid_CS).ToString();
            cm.CheckValue = RadioButtonGroup.GetSelectedValue(grid_TIMES).ToString();

            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (ChecklistPage == null)
                {
                    //Provider.SaveVenderSurvey(selectedProject, nowdata);
                    ChecklistPage = new CheckSheetPage(selectedProject, cm, isDynamic);
                    return true; // True = Repeat again, False = Stop the timer

                }
                else
                {
                    //Condition 데리고 이동
                    if (ChecklistPage.LoadingFinish)
                    {
                        popupLoadingView.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        Navigation.PushAsync(ChecklistPage);
                        ChecklistPage = null;
                        return false;
                    }
                    else
                    {
                        return true; // True = Repeat again, False = Stop the timer
                    }
                }
            });

        }
    }
}
