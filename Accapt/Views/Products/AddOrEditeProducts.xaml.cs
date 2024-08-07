using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Accapt.Views.Products
{
    public partial class AddOrEditeProducts : Window
    {
        private int producId = 0;
        private Product _product;
        private CallApi _callApi;
        public AddOrEditeProducts()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        public void Product(int productId, Product product)
        {
            producId = productId;
            _product = product;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (producId != 0)
                {
                    if (_product != null)
                    {
                        txtDescriptions.Text = _product.Description;
                        txtProductCatergory.Text = "0";
                        txtProductDiscount.Text = "0";
                        txtProductName.Text = _product.ProductName;
                        txtproductPrice.Text = _product.Price.ToString();
                        txtProductCount.Text = _product.ProductCount.ToString();
                        btnSubmit.Content = "ذخیره تغیرات";
                    }
                }
                else
                {
                    btnSubmit.Content = "افزودن محصول";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (producId != 0)
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text) ||
                        !string.IsNullOrEmpty(txtproductPrice.Text) ||
                        !string.IsNullOrEmpty(txtProductCatergory.Text) || !string.IsNullOrEmpty(txtProductCount.Text))
                    {
                        var data = new[]
                        {
                        new { op = "replace", path = "ProductName", value = txtProductName.Text },
                        new { op = "replace", path = "Description", value = txtDescriptions.Text },
                        new { op = "replace", path = "Price", value = txtproductPrice.Text },
                        new { op = "replace", path = "ProductCount", value = txtProductCount.Text },
                        new { op = "replace", path = "CatrgoryId", value = txtProductCatergory.Text },
                    };

                        var respinseMesage = await _callApi.SendPatchRequest<Product?>
                            ($"https://localhost:7146/api/MangeProduct(V1)/UPP(V1)/{producId}", data, UserSession.Instance.JwtToken);

                        if (respinseMesage.IsSuccess)
                        {
                            MessageBox.Show("تغیرات با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(respinseMesage.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text) ||
                        !string.IsNullOrEmpty(txtproductPrice.Text) ||
                        !string.IsNullOrEmpty(txtProductCatergory.Text) || !string.IsNullOrEmpty(txtProductCount.Text))
                    {
                        var data = new
                        {
                            UserName = UserSession.Instance.Username,
                            Productname = txtProductName.Text,
                            Price = Convert.ToDecimal(txtproductPrice.Text),
                            CatrgoryId = Convert.ToInt32(txtProductCatergory.Text),
                            ProductCount = Convert.ToInt32(txtProductCount.Text),
                            Description = txtDescriptions.Text
                        };

                        var responseMessge = await _callApi.SendPostRequest<AddProductDTO?>
                            ("https://localhost:7146/api/MangeProduct(V1)/ANP(V1)", data, UserSession.Instance.JwtToken);

                        if (responseMessge.IsSuccess)
                        {
                            MessageBox.Show("کالا با موفقیت ثبت شد", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtDescriptions.Text = "";
                            txtProductName.Text = "";
                            txtproductPrice.Text = "";
                            txtProductCatergory.Text = "";
                            txtProductCount.Text = "";
                            txtProductDiscount.Text = "";
                        }
                        else
                        {
                            MessageBox.Show($"Error Message is :{responseMessge.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is :{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
