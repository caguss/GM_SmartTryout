using System.ComponentModel;

namespace GM_SmartTryout
{
    public class CheckModel : INotifyPropertyChanged
    {
        /// <summary>
        /// DistributePage Class
        /// </summary>
     
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string CheckContent { set; get; } //질문

        public string Num { set; get; } //항목 ex) 1.15.1 ,,,
        public string Comment { set; get; } //comment
        public string CheckValue { get; set; } // P,O,V,K
    }
}
