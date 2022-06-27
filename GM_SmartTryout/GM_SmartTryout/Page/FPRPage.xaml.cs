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
        private ProjectModel SelectedProject;
        public bool LoadingFinish = false;
        public ObservableCollection<FPRModel> FPRModels { get; set; }
        public ExcelDetail ExcelDetails { get; set; }

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
            SelectedProject = param;

            FPRModels = Provider.ContentList(SelectedProject);
            Contentlist.ItemsSource = FPRModels;
            lbl_plant.Text = Provider.SelectedExcelDetails.Plant;
            lbl_partname.Text = Provider.SelectedExcelDetails.PartName;
            lbl_date.Text = Provider.SelectedExcelDetails.Date;
            LoadingFinish = true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("확인","저장하시겠습니까?","저장","취소"))
            {
                try
                {
                    Provider.SaveFPR(SelectedProject, FPRModels);
                    await DisplayAlert("완료", "저장이 완료되었습니다.", "확인");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("오류", $"오류가 발생했습니다. 관리자에게 문의해주세요\n{ex.Message}", "확인");
                }
            }
        }
    }
}
