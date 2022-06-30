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
    public partial class CheckSheetConditionPage : ContentPage, IAnimationPage
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
        public CheckSheetConditionPage()
        {
            InitializeComponent();
        }

        public CheckSheetConditionPage(ProjectModel param, string checksheet)
        {
            InitializeComponent();
            selectedProject = param;
            switch (checksheet)
            {
                case "D":
                    lbl_ProjectName.Text = "Dynamic Check";

                    break;
                case "S":
                    lbl_ProjectName.Text = "Static Check";
                    break;
                default:
                    break;
            }
            nowdata = Provider.LoadVenderSurvey(selectedProject);
            txt_PartName.Text = nowdata.PartName;
            txt_PartNumber.Text = nowdata.PartNumber;
            txt_Plant.Text = nowdata.StampingPlant;
            txt_CSPC.Text = nowdata.CSPC;
            txt_Program.Text = nowdata.Program;
            txt_Supplier.Text = nowdata.Supplier;
            LoadingFinish = true;

        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            nowdata.PartName = txt_PartName.Text;
            nowdata.PartNumber = txt_PartNumber.Text;
            nowdata.StampingPlant = txt_Plant.Text;
            nowdata.CSPC = txt_CSPC.Text;
            nowdata.Program = txt_Program.Text;
            nowdata.Supplier = txt_Supplier.Text;

            

            ConditionModel cm = new ConditionModel();
            cm.IF = RadioButtonGroup.GetSelectedValue(grid_IF).ToString();
            cm.CS = RadioButtonGroup.GetSelectedValue(grid_CS).ToString();
            cm.CheckValue = RadioButtonGroup.GetSelectedValue(grid_TIMES).ToString();

            //Vender Survey 저장
            Provider.SaveVenderSurvey(selectedProject, nowdata);
            //Condition 데리고 이동
            await Navigation.PushAsync((new DynamicPage(selectedProject, cm)));
        }
    }
}
