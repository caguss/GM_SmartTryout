using FormsControls.Base;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GM_SmartTryout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };
        public ObservableCollection<ProjectModel> ProjectModels { get; set; }

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {

            lbl_title.FadeTo(1, 500);
            //lbl_title.TranslateTo(0, -20, 500);
            // Put your code here but leaving empty works just fine
        }
        private IPopupNavigation Popup_ { get; set; }
        private ProjectAddPopupPage _modalPage;
        public MainPage()
        {
            InitializeComponent();
            ProjectModels = Provider.ProjectList();
            Projectlist.ItemsSource = ProjectModels;
            Popup_ = PopupNavigation.Instance;
            _modalPage = new ProjectAddPopupPage();
            //lbl_title.TranslationY = lbl_title.TranslationY + 20;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Popup_.Popped += Popup_Popped;
            lbl_title.FadeTo(1, 500);
            //lbl_title.TranslateTo(0, -20, 500);
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();
            Popup_.Popped -= Popup_Popped;
        }

        //팝업이 닫힐 때 이벤트
        private void Popup_Popped(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            IsEnabled = true;
            if (ProjectAddPopupPage.createdmodel != null)
            {
                ProjectModels.Add(ProjectAddPopupPage.createdmodel);
            }

        }


        private async void Deleted_Tapped(object sender, EventArgs e)
        {

            if (await DisplayAlert("확인", "체크된 항목을 삭제하시겠습니까?", "확인", "취소"))
            {
                for (int i = 0; i < ProjectModels.Count; i++)
                {
                    if (ProjectModels[i].IsChecked == true)
                    {
                        Provider.DeleteProject(ProjectModels[i].foldername);
                        ProjectModels.Remove(ProjectModels[i]);
                        i--;
                    }
                }
            }
        }

        private async void ProjectAdd_Tapped(object sender, EventArgs e)
        {
            IsEnabled = false;
            _modalPage = new ProjectAddPopupPage();
            await Popup_.PushAsync(_modalPage);
        }

        private async void ProjectSelect_Tapped(object sender, EventArgs e)
        {
            var param = (((TappedEventArgs)e).Parameter) as ProjectModel;
            await Navigation.PushAsync((new ProjectDetailPage(param)));
        }

        private async void Setting_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionPage());
        }
    }
}
