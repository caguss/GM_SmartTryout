using System.ComponentModel;

namespace GM_SmartTryout
{
    public class ProjectModel : INotifyPropertyChanged
    {
        /// <summary>
        /// DistributePage Class
        /// </summary>
        private bool pchecked = false;
        public bool IsChecked
        {
            get { return pchecked; }
            set
            {
                if (pchecked != value)
                {
                    pchecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       
        public string ProjectName { set; get; } //이름
        public string GenerateDate { set; get; } //생성날짜
    }
}
