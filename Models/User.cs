using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManager.Models
{
    public class User : ObservableObject
    {
        public string Id { get; set; } = "";
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name !=  value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Mail { get; set; } = "";

        public User(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
