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
    public partial class VenderSurveyPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };
        private VenderSurveyModel nowdata;
        private ProjectModel selectedProject;
        public bool LoadingFinish = false;

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
        public VenderSurveyPage()
        {
            InitializeComponent();
        }

        public VenderSurveyPage(ProjectModel param)
        {
            InitializeComponent();
            selectedProject = param;
            lbl_ProjectName.Text = "Vendor Survey";

            nowdata = Provider.LoadVenderSurvey(selectedProject);
            txt_PartName.Text = nowdata.PartName;
            txt_PartNumber.Text = nowdata.PartNumber;
            txt_Plant.Text = nowdata.StampingPlant;
            txt_CSPC.Text = nowdata.CSPC;
            txt_Program.Text = nowdata.Program;
            txt_Supplier.Text = nowdata.Supplier;
            LoadingFinish = true;

            popupLoadingView.IsVisible = false;
            activityIndicator.IsRunning = false;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            LoadingFinish = false;
            nowdata.PartName = txt_PartName.Text;
            nowdata.PartNumber = txt_PartNumber.Text;
            nowdata.StampingPlant = txt_Plant.Text;
            nowdata.CSPC = txt_CSPC.Text;
            nowdata.Program = txt_Program.Text;
            nowdata.Supplier = txt_Supplier.Text;

            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!LoadingFinish)
                {
                    if (!Provider.working)
                    {
                        //Vender Survey 저장
                        Provider.SaveVenderSurvey(selectedProject, nowdata);
                        LoadingFinish = true;
                    }
                    return true; // True = Repeat again, False = Stop the timer

                }
                else
                {

                    popupLoadingView.IsVisible = false;
                    activityIndicator.IsRunning = false;
                    Navigation.PopAsync();
                    return false;
                }
            });
            await DisplayAlert("저장", "저장이 완료되었습니다.", "확인");

        }
    }
}
