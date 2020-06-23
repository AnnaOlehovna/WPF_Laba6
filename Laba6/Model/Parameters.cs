using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    public class Parameters : INotifyPropertyChanged
    {
        private int _N;
        private double _a;
        private double _b;
        private double _progressStep;

        public Parameters()
        {
            N = 100;
            A = 0;
            B = 3.14;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int N
        {
            get => _N;
            set
            {
                _N = value;
                ProgressStep = value;
                OnPropertyChanged("Step");
            }

        }

        public double A
        {
            get => _a;
            set
            {
                _a = value;
                OnPropertyChanged("LowLimit");
            }
        }

        public double B
        {
            get => _b;
            set
            {
                _b = value;
                OnPropertyChanged("UpLimit");
            }
        }

        public double ProgressStep
        {
            get => _progressStep;
            set
            {
                _progressStep = 100.0 / value;
            }
        }
    }
}
