using System;
using System.Windows;
namespace Laba6
{
    /// <summary>
    /// Логика взаимодействия для ParamsDialog.xaml
    /// </summary>
    public partial class ParamsDialog : Window
    {
        private Parameters parameters;

        public ParamsDialog(Parameters parameters)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            this.parameters = parameters;
            DataContext = this.parameters;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public Parameters GetParameters()
        {
            return parameters;
        }
    }
}
