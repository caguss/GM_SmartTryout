using Syncfusion.XlsIO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public class Provider
    {
        static IFilesManager ifm = DependencyService.Get<IFilesManager>();
        public static ExcelDetailModel SelectedExcelDetails = new ExcelDetailModel();
        public static bool working = false;
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
        public static void SaveFPR(ProjectModel selectedProject, ObservableCollection<FPRModel> checkdata, ExcelEngine excelEngine)
        {

            using (excelEngine)
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                application.UseFastRecordParsing = true;

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
                IWorksheet worksheet = workbook.Worksheets[6];


                //FPR Page 내 체크박스 삭제

                for (int i = 0; i < worksheet.CheckBoxes.Count; i++)
                {
                    worksheet.CheckBoxes[i].Remove();
                }

     

                //Set Text

                for (int i = 13; i < checkdata.Count; i++)
                {
                    if (checkdata[i - 13].IsYesChecked)
                        worksheet.Range[$"G{i}"].Text = "Y";
                    if (checkdata[i - 13].IsNoChecked)
                        worksheet.Range[$"H{i}"].Text = "N";

                }
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                fileStream.Close();
                fs.Close();

                workbook.Close();
                excelEngine.Dispose();

                Xamarin.Forms.DependencyService.Get<IFilesManager>().SaveExcel(excelPath, "application/msexcel", stream);
            }
        }
        public static void SaveCheckList(ProjectModel selectedProject, ObservableCollection<CheckModel> checkdata, ExcelEngine excelEngine, bool isDynamic, string workcolumn)
        {

            using (excelEngine)
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                application.UseFastRecordParsing = true;

                string resourcePath = selectedProject.folderPath;
                string excelname = selectedProject.ProjectName + ".xlsx";
                //"App" is the class of Portable project.
                string excelPath = resourcePath + "/" + excelname;
                FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader r = new StreamReader(fs);
                Stream fileStream = r.BaseStream;

                //Opens the workbook 
                IWorkbook workbook = application.Workbooks.Open(fileStream);
                IWorksheet worksheet = null;
                //Access first worksheet from the workbook.

                if (isDynamic)
                    worksheet = workbook.Worksheets[9];
                else
                    worksheet = workbook.Worksheets[10];

           
                //Set Text
                for (int i = 13; i < checkdata.Count; i++)
                {
                    worksheet.Range[$"{workcolumn}{i}"].Text = checkdata[i - 13].CheckValue;

                }
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                fileStream.Close();
                fs.Close();

                workbook.Close();
                excelEngine.Dispose();

                Xamarin.Forms.DependencyService.Get<IFilesManager>().SaveExcel(excelPath, "application/msexcel", stream);
            }
        }
        public static ObservableCollection<FPRModel> FPRContentList(ProjectModel selectedProject)
        {
            ObservableCollection<FPRModel> result = new ObservableCollection<FPRModel>();
            ExcelEngine excelEngine = new ExcelEngine();
            using (excelEngine)
            {
                try
                {
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
                    IWorksheet worksheet = workbook.Worksheets[6];


                    //ExcelDetail에 데이터 삽입
                    SelectedExcelDetails = new ExcelDetailModel()
                    {
                        CSPC = worksheet.Range["B2"].DisplayText,
                        PartName = worksheet.Range["G2"].DisplayText,
                        Part_No = worksheet.Range["B3"].DisplayText,
                        Date = worksheet.Range["L2"].DisplayText,
                        Plant = worksheet.Range["K3"].DisplayText
                    };

                    //FPR 리스트 목록화

                    for (int i = 11; i < 56; i++)
                    {
                        if (worksheet.Range[$"A{i}"].DisplayText != "" && worksheet.Range[$"A{i}"].CellStyle.Font.RGBColor.Name == "ff000000")
                        {
                            result.Add(new FPRModel
                            {
                                CheckContent = worksheet.Range[$"A{i}"].DisplayText,
                                Comment = worksheet.Range[$"I{i}"].DisplayText,
                                IsYesChecked = worksheet.Range[$"G{i}"].DisplayText == "Y" ? true : false,
                                IsNoChecked = worksheet.Range[$"H{i}"].DisplayText == "N" ? true : false
                            });
                        }
                    }

                    fileStream.Close();
                    fs.Close();
                    workbook.Close();
                    excelEngine.Dispose();

                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        public static ObservableCollection<CheckModel> CheckContentList(ProjectModel selectedProject, bool isdynamic, string workingcolumn)
        {
            ObservableCollection<CheckModel> result = new ObservableCollection<CheckModel>();
            ExcelEngine excelEngine = new ExcelEngine();
            using (excelEngine)
            {
                try
                {
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

                    IWorksheet worksheet;
                    if (isdynamic)
                        worksheet = workbook.Worksheets[9];
                    else
                        worksheet = workbook.Worksheets[10];


                 

                    //ExcelDetail에 데이터 삽입
                    SelectedExcelDetails = new ExcelDetailModel()
                    {
                        CSPC = worksheet.Range["X2"].DisplayText,
                        PartName = worksheet.Range["Q3"].DisplayText,
                        Part_No = worksheet.Range["Q2"].DisplayText,
                    };

                    //dynamic 리스트 목록화

                    for (int i = 11; i < 57; i++)
                    {
                        if (worksheet.Range[$"A{i}"].DisplayText != "" && !worksheet.Range[$"A{i}"].Cells[0].IsMerged)
                        {
                            result.Add(new CheckModel
                            {
                                CheckContent = worksheet.Range[$"B{i}"].DisplayText,
                                Comment = worksheet.Range[$"AB{i}"].DisplayText,
                                Num = worksheet.Range[$"A{i}"].DisplayText,
                                CheckValue = worksheet.Range[$"{workingcolumn}{i}"].DisplayText
                            });
                        }
                    }

                    fileStream.Close();
                    fs.Close();
                    workbook.Close();
                    excelEngine.Dispose();

                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        public static VenderSurveyModel LoadVenderSurvey(ProjectModel selectedProject)
        {
            VenderSurveyModel result = new VenderSurveyModel();
            ExcelEngine excelEngine = new ExcelEngine();
            using (excelEngine)
            {
                try
                {
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
                    IWorksheet worksheet = workbook.Worksheets[5];

                    result = new VenderSurveyModel
                    {
                        Program = worksheet.Range["E3"].DisplayText,
                        PartNumber = worksheet.Range["L3"].DisplayText,
                        PartName = worksheet.Range["T3"].DisplayText,
                        CSPC = worksheet.Range["AG3"].DisplayText,
                        StampingPlant = worksheet.Range["P5"].DisplayText,
                        Supplier = worksheet.Range["E5"].DisplayText

                    };
                    fileStream.Close();
                    fs.Close();
                    workbook.Close();
                    excelEngine.Dispose();

                }
                catch (Exception ex)
                {

                    excelEngine.Dispose();
                }
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

        public static string GetFilePath(string foldername)
        {
            try
            {
                return ifm.GetFilePath(foldername);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static bool ViewExcel(string excelpath)
        {
            try
            {
                ifm.ViewExcel(excelpath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static async Task SaveVenderSurvey(ProjectModel selectedProject, VenderSurveyModel nowdata)
        {
            working = true;
            ExcelEngine excelEngine = new ExcelEngine();
            string excelname = selectedProject.ProjectName + ".xlsx";

            using (excelEngine)
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                string resourcePath = selectedProject.folderPath;
                //"App" is the class of Portable project.
                string excelPath = resourcePath + "/" + excelname;
                FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader r = new StreamReader(fs);
                Stream fileStream = r.BaseStream;

                //Opens the workbook 
                IWorkbook workbook = application.Workbooks.Open(fileStream);

                //Access first worksheet from the workbook.
                IWorksheet worksheet = workbook.Worksheets[5];

                worksheet.Range["E3"].Text = nowdata.Program;
                worksheet.Range["L3"].Text = nowdata.PartNumber;
                worksheet.Range["T3"].Text = nowdata.PartName;
                worksheet.Range["AG3"].Text = nowdata.CSPC;
                worksheet.Range["E5"].Text = nowdata.Supplier;
                worksheet.Range["P5"].Text = nowdata.StampingPlant;


                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                fileStream.Close();
                fs.Close();

                workbook.Close();
                excelEngine.Dispose();


                Xamarin.Forms.DependencyService.Get<IFilesManager>().SaveExcel(excelPath, "application/msexcel", stream);

            }

            working = false;

        }

        public static async Task<bool> ZipFile(string zippath, string projname)
        {
            try
            {
                ifm.ZipFile(zippath, projname);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
