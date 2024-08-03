
using Accapt.Core.DTOs;
using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.Views.Account;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Newtonsoft.Json;
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
        private readonly CallApi _callApiServies;
        public string userName;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _callApiServies = new CallApi();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var data = new
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Password
            };

            var responesMessage = await _callApiServies.SendPostRequest<LoginResponseDTO>("https://localhost:7146/api/ManageUsers(V1)/LGU(V1)", data);

            if(responesMessage.IsSuccess)
            {
                var token = responesMessage.Data.Token;

                var userName = JwtHelper.GetUsernameFromToken(token);
                var userId = JwtHelper.GetUserIdFromToken(token);

                UserSession.Instance.JwtToken = token;
                UserSession.Instance.Username = userName;
                UserSession.Instance.UserId = userId;

                MessageBox.Show($"خوش آمدید {txtUserName.Text}", "خوش آمد گویی", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainWindow.Visibility = Visibility.Visible;
                _mainWindow.formId = 0;
                _mainWindow.IsLogin = true;
                this.Close();
                return;
            }

            MessageBox.Show(responesMessage.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnSingUp_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RegisterPage registerPage = new RegisterPage(_mainWindow);
            registerPage.ShowDialog();
        }
    }
}
