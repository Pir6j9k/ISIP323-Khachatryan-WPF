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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISIP323_Khachatryan_WPF.Pages.Shop
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();

            var types = Core.DB.ProductType.ToList();
            types.Insert(0, new ProductType { Name = "Все типы" });
            CbProductType.ItemsSource = types;
            CbProductType.SelectedIndex = 0;

            var manufacturers = Core.DB.Manufacturers.ToList();
            manufacturers.Insert(0, new Manufacturer { Name = "Все производители" });
            CBManufacturer.ItemsSource = manufacturers;
            CBManufacturer.SelectedIndex = 0;

            CBSort.SelectedIndex = 0;
            UpdateProducts();
        }

        public void UpdateProducts()
        {
            var list = Core.DB.Products.ToList();

            if (CbProductType.SelectedIndex > 0)
            {
                var selectedType = CbProductType.SelectedItem as ProductType;
                list = list.Where(p => p.ProductTypeId == selectedType.Id).ToList();
            }

            if (CBManufacturer.SelectedIndex > 0)
            {
                var selectedMan = CBManufacturer.SelectedItem as Manufacturer;
                list = list.Where(p => p.ManufacturerId == selectedMan.Id).ToList();
            }

            if (!string.IsNullOrWhiteSpace(TbxSearch.Text))
                list = list.Where(p => p.Name.ToLower().Contains(TbxSearch.Text.ToLower())).ToList();

            if (CBSort.SelectedIndex == 1) list = list.OrderBy(p => p.Price).ToList();
            else if (CBSort.SelectedIndex == 2) list = list.OrderByDescending(p => p.Price).ToList();

            LBProducts.ItemsSource = list;
            TxtCount.Text = $"Товаров: {list.Count}";
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            UpdateProducts();
        }

        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (Core.AuthUser == null)
            {
                MessageBox.Show("Для оформления заказа необходимо авторизоваться в системе!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService.Navigate(new Pages.Client.LoginPage());
                return;
            }
            if ((sender as Button).Tag is Product product)
            {
                Core.SelectedProducts.Add(product);
                MessageBox.Show($"Товар '{product.Name}' добавлен!");
                NavigationService.Navigate(new CartPage());
            }
        }
        private void LBoxProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LBProducts.SelectedItem is Product selectedProduct)
            {
                NavigationService.Navigate(new Pages.Shop.ProductFullInfoPage(selectedProduct));
            }
        }
    }
}
