
using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccaptFullyVersion.App.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly ApiCallServies _callApiServies;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _callApiServies = new ApiCallServies();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSingUp_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RegisterPage registerPage = new RegisterPage(_mainWindow);
            registerPage.ShowDialog();
        }
    }
}
