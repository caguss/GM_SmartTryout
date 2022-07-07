using System.Collections.Generic;
using System.ComponentModel;

namespace GM_SmartTryout
{
    public class CheckModel : INotifyPropertyChanged
    {
        /// <summary>
        /// DistributePage Class
        /// </summary>

        public string _checkValue;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string CheckContent { set; get; } //질문

        public string Num { set; get; } //항목 ex) 1.15.1 ,,,
        public string Comment { set; get; } //comment
        public string CheckValue 
        { 
            get { return _checkValue; }
            set
            {
                _checkValue = value;
                OnPropertyChanged("CheckValue");
            } 
        } // P,O,V,K

    }
}
