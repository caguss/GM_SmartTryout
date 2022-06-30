using System.ComponentModel;

namespace GM_SmartTryout
{
    public class ConditionModel : INotifyPropertyChanged
    {
        /// <summary>
        /// DistributePage Class
        /// </summary>
     
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string IF { set; get; } //Initial or Final

        public string CS { set; get; } //Customer or Supplier
        public string CheckValue { set; get; } // 10,20,30,40,50
    }
}
