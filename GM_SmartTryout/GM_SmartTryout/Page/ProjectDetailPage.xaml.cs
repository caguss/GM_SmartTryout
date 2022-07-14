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
                string zippath;
                switch (await DisplayActionSheet("어떤 파일을 공유하십니까?","취소", "", "사진", "엑셀"))
                {
                    case "사진":
                        zippath = Provider.GetFilePath(selectedProject.foldername + "/Image");

                        await ExportUtilties.CompressAndExportFolder(zippath, selectedProject.ProjectName+"_Image");
                        await ExportUtilties.ShareFile(zippath, selectedProject.ProjectName + "_Image");

                        break;
                    case "엑셀":
                        zippath = Provider.GetFilePath(selectedProject.folderPath + "/" + selectedProject.ProjectName + ".xlsx");

                        await Provider.ZipFile(zippath, selectedProject.ProjectName+"_Excel");
                        await ExportUtilties.ShareFile(zippath, selectedProject.ProjectName+"_Excel");

                        break;
                    default:
                        return;
                        break;
                }


                await DisplayAlert("완료", "압축이 완료되었습니다.", "확인");
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", $"압축 중 오류를 발견했습니다.\n관리자에게 문의하세요.\n{ex.Message}", "확인");
            }

        }
        FPRPage FPRlistPage = null;
        CheckSheetConditionPage ConditionLoadingPage = null;
        VenderSurveyPage surveyloadingpage = null;
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
                        FPRlistPage = null;
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
            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (ConditionLoadingPage == null)
                {
                    ConditionLoadingPage = new CheckSheetConditionPage(selectedProject, true);
                    return true; // True = Repeat again, False = Stop the timer

                }
                else
                {
                    if (ConditionLoadingPage.LoadingFinish)
                    {
                        popupLoadingView.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        Navigation.PushAsync(ConditionLoadingPage);
                        ConditionLoadingPage = null;
                        return false;
                    }
                    else
                    {
                        return true; // True = Repeat again, False = Stop the timer
                    }
                }
            });
        }
        private async void Static_Tapped(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (ConditionLoadingPage == null)
                {
                    ConditionLoadingPage = new CheckSheetConditionPage(selectedProject, false);
                    return true; // True = Repeat again, False = Stop the timer

                }
                else
                {
                    if (ConditionLoadingPage.LoadingFinish)
                    {
                        popupLoadingView.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        Navigation.PushAsync(ConditionLoadingPage);
                        ConditionLoadingPage = null;
                        return false;
                    }
                    else
                    {
                        return true; // True = Repeat again, False = Stop the timer
                    }
                }
            });
        }

        private void ViewExcel_Tapped(object sender, EventArgs e)
        {
            Provider.ViewExcel(selectedProject.folderPath + "/" + selectedProject.ProjectName + ".xlsx");
        }

        private void VendorSurvay_Tapped(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (surveyloadingpage == null)
                {
                    surveyloadingpage = new VenderSurveyPage(selectedProject);
                    return true; // True = Repeat again, False = Stop the timer

                }
                else
                {
                    if (surveyloadingpage.LoadingFinish)
                    {
                        popupLoadingView.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        Navigation.PushAsync(surveyloadingpage);
                        surveyloadingpage = null;
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
