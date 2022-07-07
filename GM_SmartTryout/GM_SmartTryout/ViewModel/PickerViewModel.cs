using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GM_SmartTryout
{
    public class PickerViewModel : BaseViewModel
    {
        private ObservableCollection<CheckModel> checkList;
        private List<string> _PickerData;

        public List<string> PickerData
        {
            get => _PickerData;
            set
            {
                _PickerData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CheckModel> CheckList { get => checkList; set => checkList = value; }
    }

    //public class PickerX
    //{
    //    public PickerX(String name)
    //    {
    //        PickerName = name;
    //    }
    //    public String PickerName { get; set; }
    //}

}
