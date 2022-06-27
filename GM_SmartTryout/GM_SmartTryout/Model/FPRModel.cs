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
                    if (yeschecked == true && yeschecked == nochecked)
                    {
                        nochecked = !nochecked;
                        OnPropertyChanged("IsNoChecked");
                    }
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
                    if (yeschecked == true && yeschecked == nochecked)
                    {
                        yeschecked = !yeschecked;
                        OnPropertyChanged("IsYesChecked");
                    }

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
    }
}
