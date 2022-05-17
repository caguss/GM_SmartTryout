using FormsControls.Base;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GM_SmartTryout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class OptionPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
        public OptionPage()
        {
            InitializeComponent();

            // 세팅 불러오기
            ResourceManager rm = new ResourceManager("GM_SmartTryout.Properties.Resource", Assembly.GetExecutingAssembly());

            txt_ExcelName.Text = Application.Current.Properties.ContainsKey("TemplateName") ? Application.Current.Properties["TemplateName"].ToString() : rm.GetString("TemplateName");
            txt_version.Text = Application.Current.Properties.ContainsKey("Version") ? Application.Current.Properties["Version"].ToString() : rm.GetString("Version");
        }

   

        private async void Change_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("경고","변경 시 기존 생성된 프로젝트에는 적용되지 않으며,\n기존 프로젝트 또한 열 수 없을 수 있습니다.\n계속하시겠습니까?","예","아니오"))
            {
                if (await PickAndShow(PickOptions.Default) != null)
                {
                    await DisplayAlert("완료", "변경이 완료되었습니다.", "확인");
                }
            }
          

        }
        async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    txt_ExcelName.Text = result.FileName;
                    Application.Current.Properties["TemplateName"] = result.FileName;
                    Application.Current.Properties["TemplateFullPath"] = result.FullPath;
                    await Application.Current.SavePropertiesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", $"에러가 발생했습니다. 관리자에게 문의하여 주세요.\nError: {ex.Message}","확인");

                return null;

                // The user canceled or something went wrong
            }

        }
    }
}
