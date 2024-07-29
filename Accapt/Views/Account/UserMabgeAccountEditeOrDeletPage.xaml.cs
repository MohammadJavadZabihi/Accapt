using Accapt.WpfServies;
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
        public int Id = 0;
        public string path = "";
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
            txt.Text = _prop;

            switch (Id)
            {
                case 1:
                    path = "UserName";
                    lbl.Text = "نام کاربری";
                    break;

                case 2:
                    path = "Email";
                    lbl.Text = "ایمیل";
                    break;

                case 3:
                    path = "PhoneNumber";
                    lbl.Text = "تلفن همراه";
                    break;

                case 4:
                    path = "RealFullName";
                    lbl.Text = ": نام و نام خوانوادگی";
                    break;

                default:
                    path = "";
                    lbl.Text = "خطا";
                    break;
            }
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
            if (string.IsNullOrEmpty(txt.Text))
                MessageBox.Show("لطفا کادر مورد نظر را پرنمایید");

            var data = new[]
            {
                new { op = "replace", path = path, value = txt.Text },
            };

            try
            {
                var responeMessage = await _callApi.SendPatchRequest<DataLayer.Entities.Users>($"https://localhost:7146/api/ManageUsers(V1)/UPD(V1)/{UserSession.Instance.Username}", data);
                if(responeMessage.IsSuccess)
                {
                    MessageBox.Show("تغیرات با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("اپلیکیشن تا چند ثانیه دیگیر بسته میشود, لطفا دوباره اپلیکیشن را باز کنید", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("نا موفق");
            }
        }
    }
}
