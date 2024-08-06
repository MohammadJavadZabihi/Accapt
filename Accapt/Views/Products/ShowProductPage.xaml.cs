using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accapt.Views.Products
{
    /// <summary>
    /// Interaction logic for ShowProductPage.xaml
    /// </summary>
    public partial class ShowProductPage : Page
    {
        private readonly CallApi _calApiServies;
        private int _pageSize = 10;
        private int _currentPage = 1;
        private int _totalPages;
        public ShowProductPage()
        {
            InitializeComponent();
            _calApiServies = new CallApi();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts(_currentPage);
        }

        private async Task<bool> LoadProducts(int pageNumber)
        {
            try
            {
                var responseMessgae = await _calApiServies.SendGetRequest<ShowProductDTO>($"https://localhost:7146/api/MangeProduct(V1)/GTAP(V1)?pageNumber={pageNumber}&pageSize={_pageSize}&filter={""}&userId={UserSession.Instance.UserId}",
                    data: null, jwt: UserSession.Instance.JwtToken);

                if (responseMessgae.IsSuccess)
                {
                    var data = responseMessgae.Data;
                    int pCount = data.Products.Count();
                    if (pCount != 0)
                    {
                        _totalPages = pCount / _pageSize;
                        productDataGrid.ItemsSource = data.Products;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private async void PageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                _currentPage = int.Parse(button.Content.ToString());
                await LoadProducts(_currentPage);
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            var statuce = await LoadProducts(_currentPage);
            if (!statuce)
            {
                _currentPage--;
                LoadProducts(_currentPage);
            }
        }

        private void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            if (_currentPage > 0)
            {
                LoadProducts(_currentPage);
            }
            else
            {
                _currentPage++;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem is Product product)
                {
                    int productId = product.ProductId;

                    SingleProductNameDTO dtoP = new SingleProductNameDTO
                    {
                        ProductId = productId
                    };

                    if (productId != 0)
                    {
                        if(MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var responseMessage = await _calApiServies.SendDeletRequest<object>("https://localhost:7146/api/MangeProduct(V1)/DLP(V1)", dtoP, UserSession.Instance.JwtToken);
                            if(responseMessage.IsSuccess)
                            {
                                MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadProducts(_currentPage);
                            }
                            else
                            {
                                MessageBox.Show($"Error Message : {responseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("لطفا اول ایتمی را انتخواب کنید و بعد اقدام به اجرای عملیات بر روی آن بکنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem is Product product)
                {
                    int productId = product.ProductId;

                    if(productId != 0)
                    {
                        AddOrEditeProducts addOrEditeProducts = new AddOrEditeProducts();
                        addOrEditeProducts.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
