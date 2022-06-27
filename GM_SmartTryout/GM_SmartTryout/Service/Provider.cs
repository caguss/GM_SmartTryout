using Syncfusion.XlsIO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public class Provider
    {
        static IFilesManager ifm = DependencyService.Get<IFilesManager>();
        public static ExcelDetail SelectedExcelDetails = new ExcelDetail();
    
        public static ProjectModel CreateProject(string projName, string templatePath)
        {
            try
            {
                return ifm.FileCreate(projName, templatePath);
            }
            catch (Exception ex)
            {
                return new ProjectModel()
                {
                    ProjectName = "Error",
                    GenerateDate = ex.Message
                };
            }
        }
        public static ObservableCollection<ProjectModel> ProjectList()
        {
                return ifm.ProjectList();
          
        }
        public static void SaveFPR(ProjectModel selectedProject ,ObservableCollection<FPRModel> checkdata)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2016;

            string resourcePath = selectedProject.folderPath;
            string excelname = selectedProject.ProjectName + ".xlsx";
            //"App" is the class of Portable project.
            string excelPath = resourcePath + "/" + excelname;
            FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader r = new StreamReader(fs);
            Stream fileStream = r.BaseStream;

            //Opens the workbook 
            IWorkbook workbook = application.Workbooks.Open(fileStream);

            //Access first worksheet from the workbook.
            IWorksheet worksheet = workbook.Worksheets[1];



            //Set Text

            for (int i = 13; i < 57; i++)
            {
                if (checkdata[i - 13].IsYesChecked)
                    worksheet.Range[$"G{i}"].Text = "Y";
                if (checkdata[i - 13].IsNoChecked)
                    worksheet.Range[$"H{i}"].Text = "N";

            }
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);

            workbook.Close();
            excelEngine.Dispose();

            Xamarin.Forms.DependencyService.Get<IFilesManager>().SaveFPR(excelname, "application/msexcel", stream);



        }

        public static  ObservableCollection<FPRModel> ContentList(ProjectModel selectedProject)
        {
            ObservableCollection<FPRModel> result = new ObservableCollection<FPRModel>();

            try
            {
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                string resourcePath = selectedProject.folderPath;
                string excelname = selectedProject.ProjectName + ".xlsx";
                //"App" is the class of Portable project.
                string excelPath = resourcePath + "/" + excelname;
                FileStream fs = new FileStream (excelPath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader r = new StreamReader(fs);
                Stream fileStream = r.BaseStream;

                //Opens the workbook 
                IWorkbook workbook = application.Workbooks.Open(fileStream);

                //Access first worksheet from the workbook.
                IWorksheet worksheet = workbook.Worksheets[1];


                //FPR Page 내 체크박스 삭제

                for (int i = 0; i < worksheet.CheckBoxes.Count; i++)
                {
                    worksheet.CheckBoxes[i].Remove();
                }


                //ExcelDetail에 데이터 삽입
                SelectedExcelDetails = new ExcelDetail()
                { 
                    CSPC = worksheet.Range["B2"].Text,
                    PartName = worksheet.Range["G2"].Text,
                    Part_No = worksheet.Range["B3"].Text,
                    Date = worksheet.Range["L2"].Text,
                    Plant = worksheet.Range["K3"].Text
                };
                
                //FPR 리스트 목록화

                for (int i = 13; i < 57; i++)
                {
                    result.Add(new FPRModel 
                    {
                        CheckContent = worksheet.Range[$"A{i}"].Text,
                        Comment = worksheet.Range[$"I{i}"].Text,
                        IsYesChecked = worksheet.Range[$"G{i}"].Text == "Y"? true : false,
                        IsNoChecked = worksheet.Range[$"H{i}"].Text == "N" ? true : false
                    });
                }

                //Set Text in cell A3.
                //worksheet.Range["H13"].Text = "O";
                //worksheet.CheckBoxes["Check Box 9"].Parent

                //MemoryStream stream = new MemoryStream();
                //workbook.SaveAs(stream);

                workbook.Close();
                excelEngine.Dispose();

            }
            catch (Exception ex)
            {

            }
        



            return result;
        }

        public static bool DeleteProject(string filename)
        {
            try
            {
                ifm.DeleteProject(filename);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string ZipProject(string foldername)
        {
            try
            {
                return ifm.ZipProject(foldername);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetFileUri (string zippath)
        {
            try
            {
                return ifm.GetFileUri(zippath);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
