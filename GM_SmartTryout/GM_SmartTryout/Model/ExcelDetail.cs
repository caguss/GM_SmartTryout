using System.ComponentModel;

namespace GM_SmartTryout
{
    /// <summary>
    /// 엑셀 탭 내부에 있는 데이터들.
    /// 예를 들어, PLANT, NAME, DATE 의 항목들
    /// </summary>
    public class ExcelDetail
    {
       
        public string Plant { set; get; } 
        public string Date { set; get; } 
        public string PartName { set; get; } 
        public string CSPC { set; get; }
        public string Part_No { set; get; }
    }
}
