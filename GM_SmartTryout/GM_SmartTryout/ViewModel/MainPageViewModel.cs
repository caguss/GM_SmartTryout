using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            GetTest();
        }
        public Command NavigateToDetailPageCommand { get; }
        public ObservableCollection<ProjectModel> ProjectModels {get;set;}

        void GetTest()
        {
            ProjectModels = new ObservableCollection<ProjectModel>(Provider.Testdata());
        }

    }
}
