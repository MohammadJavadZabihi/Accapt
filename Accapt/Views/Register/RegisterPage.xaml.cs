using Accapt.Core.DTOs;
using Accapt.Core.Servies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AccaptFullyVersion.App.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly CallApi _callApiServies;
        public RegisterPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _callApiServies = new CallApi();
        }

        private void btnSingin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(_mainWindow);
            loginPage.Visibility = Visibility.Visible;
            this.Close();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            var data = new
            {
                Name = txtName.Text,
                Family = txtFamily.Text,
                UserName = txtUserName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Password,
                RePassword = txtRePassword.Password,
                PhoneNumber = txtPhoneNumber.Text
            };

            var responeMessage = await _callApiServies.SendPostRequest<object>("https://localhost:7146/api/ManageUsers(V1)/RGU(V1)", data);

            if (responeMessage.IsSuccess)
            {
                MessageBox.Show("ثبت نام با موفقیت انجام شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginPage loginPage = new LoginPage(_mainWindow);
                loginPage.Visibility = Visibility.Visible;
                this.Close();
            }

            MessageBox.Show(responeMessage.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
