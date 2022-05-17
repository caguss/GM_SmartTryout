using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public partial class ProjectAddPopupPage : global::Rg.Plugins.Popup.Pages.PopupPage
    {
        public static ProjectModel createdmodel = null;
        public ProjectAddPopupPage()
        {
            InitializeComponent();
        }
        private async Task<bool> RequestStroageWritePermission()
        {
            var resultstroage = await Permissions.RequestAsync<Permissions.StorageWrite>();
            // Check result from permission request. If it is allowed by the user, connect to scanner
            if (resultstroage == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
                {
                    await DisplayAlert("알림", "프로젝트 삭제 및 생성을 위한 권한 수락이 필요합니다", "확인");
                    RequestStroageWritePermission();
                }

                return true;
            }
        }
        private async Task<bool> RequestStroageReadPermission()
        {
            var resultstroage = await Permissions.RequestAsync<Permissions.StorageRead>();
            // Check result from permission request. If it is allowed by the user, connect to scanner
            if (resultstroage == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                {
                    await DisplayAlert("알림", "프로젝트 삭제 및 생성을 위한 권한 수락이 필요합니다", "확인");
                    RequestStroageReadPermission();
                }

                return true;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RequestStroageWritePermission();
            await RequestStroageReadPermission();
            createdmodel = null;
        }



        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txt_projname.Text == "")
                {
                    await DisplayAlert("오류", "프로젝트 이름을 정해주세요.", "확인");
                    return;
                }
                createdmodel = Provider.CreateProject(txt_projname.Text, Application.Current.Properties["TemplateFullPath"].ToString());
                if (createdmodel != null)
                {
                    await DisplayAlert("완료", "프로젝트 생성이 완료되었습니다.", "확인");
                    await PopupNavigation.Instance.PopAsync(true);
                }
                else
                {
                    await DisplayAlert("오류", "같은 이름의 프로젝트가 존재합니다.\n같은 날짜의 같은 프로젝트명일 수 없습니다.", "확인");
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("오류", "기본 템플릿 엑셀이 지정되지 않았습니다.\n설정 화면에서 엑셀 템플릿을 확인해주세요.", "확인");

            }
        }
    }
}
