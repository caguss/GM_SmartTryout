using System.ComponentModel;

namespace GM_SmartTryout
{
    public class FPRModel : INotifyPropertyChanged
    {
        /// <summary>
        /// DistributePage Class
        /// </summary>
        private bool yeschecked = false;
        private bool nochecked = false;
        public bool IsYesChecked
        {
            get { return yeschecked; }
            set
            {
                if (yeschecked != value)
                {
                    yeschecked = value;
                    OnPropertyChanged("IsYesChecked");
                }
            }
        }
        public bool IsNoChecked
        {
            get { return nochecked; }
            set
            {
                if (nochecked != value)
                {
                    nochecked = value;
                    OnPropertyChanged("IsNoChecked");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       
        public string CheckContent { set; get; } //질문

        public string Comment { set; get; } //comment
        public string Category { set; get; } //카테고리? Die Function, Fixture Interface, Dimensional, surface,, 
    }
}
