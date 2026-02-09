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

namespace ISIP323_Khachatryan_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            if (Carts.products != null)
            {
                ProductList.ItemsSource = Carts.products;
            }
            else MessageBox.Show("Հահահա");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products selected = button.DataContext as Products;

            Carts.products.Remove(selected);
            ProductList.ItemsSource = null;

            ProductList.ItemsSource = Carts.products;
        }

        private void MainPrevButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProductPage());
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.OrderPage());
        }
    }
}
