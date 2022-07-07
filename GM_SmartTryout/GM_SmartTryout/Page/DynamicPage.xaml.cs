using FormsControls.Base;
using Syncfusion.XlsIO;
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
    public partial class DynamicPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromRight };
        private ProjectModel SelectedProject;
        public bool LoadingFinish = false;

        /// <summary>
        /// INITIAL - SUPPLIER
        /// E, G, I, K, M
        /// 
        /// FINAL - SUPPLIER
        /// R, T, V, X, Z
        /// 
        /// INITIAL - CUSTOMER
        /// F, H, J, L, N
        /// 
        /// FINAL - CUSTOMER
        /// S, U, W, Y, AA
        /// 
        /// </summary>
        private string workColumn; // 작업할 컬럼 지정
        //public ObservableCollection<CheckModel> CheckModels { get; set; }
        PickerViewModel _pvm;
        public ExcelDetailModel ExcelDetails { get; set; }

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
        public DynamicPage()
        {
            InitializeComponent();

        }

        public DynamicPage(ProjectModel param, ConditionModel cm)
        {
            InitializeComponent();
            _pvm = new PickerViewModel();

            SelectedProject = param;

            if (cm.IF == "Initial")
            {
                if (cm.CS == "Customer")
                {
                    //INITIAL - CUSTOMER
                    // F, H, J, L, N
                    switch (cm.CheckValue)
                    {
                        case "10":
                            workColumn = "F";
                            break;
                        case "20":
                            workColumn = "H";
                            break;
                        case "30":
                            workColumn = "J";
                            break;
                        case "40":
                            workColumn = "L";
                            break;
                        case "50":
                            workColumn = "N";
                            break;
                    }
                }
                else
                {
                    //INITIAL - SUPPLIER
                    // E, G, I, K, M
                    switch (cm.CheckValue)
                    {
                        case "10":
                            workColumn = "E";
                            break;
                        case "20":
                            workColumn = "G";
                            break;
                        case "30":
                            workColumn = "I";
                            break;
                        case "40":
                            workColumn = "K";
                            break;
                        case "50":
                            workColumn = "M";
                            break;
                    }
                }
            }
            else
            {
                if (cm.CS == "Customer")
                {
                    //FINAL - CUSTOMER
                    // S, U, W, Y, AA
                    switch (cm.CheckValue)
                    {
                        case "10":
                            workColumn = "S";
                            break;
                        case "20":
                            workColumn = "U";
                            break;
                        case "30":
                            workColumn = "W";
                            break;
                        case "40":
                            workColumn = "Y";
                            break;
                        case "50":
                            workColumn = "AA";
                            break;
                    }
                }
                else
                {
                    //FINAL - SUPPLIER
                    // R, T, V, X, Z
                    switch (cm.CheckValue)
                    {
                        case "10":
                            workColumn = "R";
                            break;
                        case "20":
                            workColumn = "T";
                            break;
                        case "30":
                            workColumn = "V";
                            break;
                        case "40":
                            workColumn = "X";
                            break;
                        case "50":
                            workColumn = "Z";
                            break;
                    }
                }
            }




            _pvm.CheckList = Provider.CheckContentList(SelectedProject, true, workColumn);
          

            Contentlist.BindingContext = _pvm;

            lbl_CSPC.Text = Provider.SelectedExcelDetails.CSPC;
            lbl_partname.Text = Provider.SelectedExcelDetails.PartName;
            lbl_partnum.Text = Provider.SelectedExcelDetails.Part_No;
            LoadingFinish = true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("확인","저장하시겠습니까?","저장","취소"))
            {
                ExcelEngine excelEngine = new ExcelEngine();

                try
                {
                    Provider.SaveCheckList(SelectedProject, _pvm.CheckList, excelEngine, true, workColumn);
                    await DisplayAlert("완료", "저장이 완료되었습니다.", "확인");

                }
                catch (Exception ex)
                {
                    excelEngine.Dispose();
                    await DisplayAlert("오류", $"오류가 발생했습니다. 관리자에게 문의해주세요\n{ex.Message}", "확인");
                }
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Help", "√ = OK (P)\v\nΧ = not OK (O)\v\nⓍ= Re-checked OK (V)\v\nØ= not applicable (X)", "확인");
        }
    }
}
