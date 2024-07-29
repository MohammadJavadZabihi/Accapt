using AccaptFullyVersion.App;
using ApiRequest.Net.CallApi;
using System.Windows;

namespace Accapt.Views.Account
{
    /// <summary>
    /// Interaction logic for UserMabgeAccountEditeOrDeletPage.xaml
    /// </summary>
    public partial class UserMabgeAccountEditeOrDeletPage : Window
    {
        private CallApi _callApi;
        private string _prop;
        public UserMabgeAccountEditeOrDeletPage(CallApi callApi)
        {
            InitializeComponent();
            _callApi = callApi ?? throw new ArgumentException(nameof(callApi));
        }

        public void SetProperty(string prop)
        {
            _prop = prop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = _prop;
        }

        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RestartApplication()
        {

            Application.Current.Exit += new ExitEventHandler(App_Exit);

            Application.Current.Shutdown();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private async void btnSubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
                MessageBox.Show("لطفا کادر مورد نظر را پرنمایید");

            var data = new[]
            {
                new { op = "replace", path = "UserName", value = txtUserName.Text },
            };

            try
            {
                var responeMessage = await _callApi.SendPatchRequest<DataLayer.Entities.Users>($"https://localhost:7146/api/ManageUsers(V1)/UPD(V1)/{_prop}", data);
                if(responeMessage.IsSuccess)
                {
                    MessageBox.Show("تغیرات با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                    RestartApplication();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("نا موفق");
            }
        }
    }
}
