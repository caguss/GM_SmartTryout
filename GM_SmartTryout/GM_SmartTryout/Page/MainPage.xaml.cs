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
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;
        private IPopupNavigation Popup_ { get; set; }
        private ProjectAddPopupPage _modalPage; 
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel(Navigation);
            BindingContext = viewModel;
            Popup_ = PopupNavigation.Instance;
            _modalPage = new ProjectAddPopupPage();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Popup_.Popped += Popup_Popped;

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
        }
        private void ProjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void Deleted_Tapped(object sender, EventArgs e)
        {
            if (await DisplayAlert("확인","체크된 항목을 삭제하시겠습니까?","확인","취소"))
            {
                for (int i = 0; i < viewModel.ProjectModels.Count; i++)
                {
                    if (viewModel.ProjectModels[0].IsChecked == true)
                    {
                        viewModel.ProjectModels.Remove(viewModel.ProjectModels[0]);
                        i--;
                        //폴더에서 삭제
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

        private void Setting_Tapped(object sender, EventArgs e)
        {

        }
    }
}
