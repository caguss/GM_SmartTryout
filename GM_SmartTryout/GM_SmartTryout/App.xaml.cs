using FormsControls.Base;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GM_SmartTryout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AnimationNavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
