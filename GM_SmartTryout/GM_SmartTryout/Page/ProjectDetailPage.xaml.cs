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
    public partial class ProjectDetailPage : ContentPage, IAnimationPage
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


        private async void Zip_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("확인", "현재까지의 프로젝트를 공유하시겠습니까?", "예", "아니오"))
                {

                    string projpath = Provider.ZipProject(selectedProject.foldername);

                    await ExportUtilties.CompressAndExportFolder(projpath, selectedProject.ProjectName);
                    await DisplayAlert("완료", "압축이 완료되었습니다.", "확인");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", $"압축 중 오류를 발견했습니다.\n관리자에게 문의하세요.\n{ex.Message}", "확인");
            }

        }
        FPRPage FPRlistPage = null;
        private async void FPR_Tapped(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (FPRlistPage == null)
                    {
                        FPRlistPage = new FPRPage(selectedProject);
                        return true; // True = Repeat again, False = Stop the timer

                    }
                    else
                    {
                        if (FPRlistPage.LoadingFinish)
                        {
                            popupLoadingView.IsVisible = false;
                            activityIndicator.IsRunning = false;
                            Navigation.PushAsync(FPRlistPage);
                            return false;
                        }
                        else
                        {
                            return true; // True = Repeat again, False = Stop the timer
                        }
                    }

                  

                });

        }


        private async void Dynamic_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new CheckSheetConditionPage(selectedProject, "D")));

        }
        private async void Static_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new CheckSheetConditionPage(selectedProject, "S")));

        }
    }
}
