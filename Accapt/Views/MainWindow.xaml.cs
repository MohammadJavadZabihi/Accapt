using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.Views.Account;
using AccaptFullyVersion.App.Views;
using ApiRequest.Net.CallApi;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccaptFullyVersion.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CallApi _callApiServies;
        public bool IsLogin = false;
        public int formId = -1;
        public MainWindow()
        {
            InitializeComponent();
            _callApiServies = new CallApi();
            if (formId == -1 && !IsLogin)
            {
                LoginPage loginPage = new LoginPage(this);
                loginPage.ShowDialog();
            }
        }

        private void btnAccountDetails_Click(object sender, RoutedEventArgs e)
        {
            UserMangeAccount manageAccountPage = new UserMangeAccount();
            fContainer.Navigate(manageAccountPage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}