using Laba6.Utils;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Laba6.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private readonly BackgroundWorker backgroundWorker;

        private Parameters _parameters;
        public Parameters Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                OnPropertyChanged("Parameters");
            }
        }

        private bool _isButtonsEnable = true;
        public bool IsButtonEnable
        {
            get => _isButtonsEnable;
            set
            {
                _isButtonsEnable = value;
                OnPropertyChanged("IsButtonEnable");
            }
        }

        private int _progressPercent;
        public int ProgressPercent
        {
            get => _progressPercent;
            set
            {
                _progressPercent = value;
                OnPropertyChanged("ProgressPercent");
            }
        }

        private double _result;
        public double Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }


        public MainWindowViewModel()
        {
            Parameters = new Parameters();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
        }

        #region ChangeParams
        private DelegateCommand _changeParamsCommand;
        public DelegateCommand ChangeParamsCommand
        {
            get
            {
                return _changeParamsCommand ?? (_changeParamsCommand = new DelegateCommand(ChangeParamsDialog));
            }
        }

        private void ChangeParamsDialog(object arg)
        {
            ParamsDialog inputDialog = new ParamsDialog(Parameters);
            if (inputDialog.ShowDialog() == true)
            {
                Parameters = inputDialog.GetParameters();
            }
        }
        #endregion

        #region CalculateDispatcher
        private DelegateCommand _calculateDispatcherCommand;
        public DelegateCommand CalculateDispatcherCommand
        {
            get
            {
                return _calculateDispatcherCommand ?? (_calculateDispatcherCommand = new DelegateCommand(CalculateDispatcher));
            }
        }

        private async void CalculateDispatcher(object arg)
        {
            IsButtonEnable = false;
            await CalculateAsync();
        }
        #endregion

        #region CalculateWorker
        private DelegateCommand _calculateWorkerCommand;
        public DelegateCommand CalculateWorkerCommand
        {
            get
            {
                return _calculateWorkerCommand ?? (_calculateWorkerCommand = new DelegateCommand(CalculateWorker));
            }
        }


        private void CalculateWorker(object arg)
        {
            IsButtonEnable = false;
            backgroundWorker.RunWorkerAsync();
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyChanged)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double h = (Parameters.B - Parameters.A) / Parameters.N;
            double sum = 0;
            for (int i = 0; i <= Parameters.N; i++)
            {
                Thread.Sleep(100);

                sum += Math.Sin((Parameters.A + h * i) * Math.PI / 180.0);

                if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                {
                    backgroundWorker.ReportProgress((int)(i * Parameters.ProgressStep));

                }
            }
            Result = h * sum;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsButtonEnable = true;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressPercent = e.ProgressPercentage;
        }

        private Task CalculateAsync()
        {
            double h = (Parameters.B - Parameters.A) / Parameters.N;
            double sum = 0;
            return Task.Run(() =>
            {
                for (int i = 0; i <= Parameters.N; i++)
                {
                    Thread.Sleep(100);

                    sum += Math.Sin((Parameters.A + h * i) * Math.PI / 180.0);

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action(
                            () => ProgressPercent = (int)(i * Parameters.ProgressStep))
                        );
                }

                Result = h * sum;
                IsButtonEnable = true;

            });
        }

        private void BtnCalculateWorker_Click(object sender, RoutedEventArgs e)
        {
            IsButtonEnable = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void Calculate(bool isWorker)
        {
            IsButtonEnable = false;
            double h = (Parameters.B - Parameters.A) / Parameters.N;
            double sum = 0;
            for (int i = 0; i <= Parameters.N; i++)
            {
                Thread.Sleep(100);

                sum += Math.Sin((Parameters.A + h * i) * Math.PI / 180.0);

                if (isWorker)
                {
                    if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                    {
                        backgroundWorker.ReportProgress((int)(i * Parameters.ProgressStep));
                    }
                }
                else
                {

                    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action(() => ProgressPercent = (int)(i * Parameters.ProgressStep)));
                }

            }

            Result = h * sum;
        }
    }
}
