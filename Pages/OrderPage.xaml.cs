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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            LoadCart();
            UpdateSum();
        }
        private void LoadCart()
        {
            if (Carts.products != null)
            {
                ProductList.ItemsSource = Carts.products;
            }
        }

        private void MainPrevButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.CartPage());
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(TbxFullName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя");
                return;
            }

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                MessageBox.Show("Пожалуйста, введите почту");
                return;
            }

            if (string.IsNullOrEmpty(TbxAddress.Text))
            {
                MessageBox.Show("Пожалуйста, введите адрес");
                return;
            }
            MessageBox.Show($"Спасибо за заказ, {TbxFullName.Text},\n" +
                $"Почта: {TbxEmail.Text},\n" +
                $"Адрес доставки: {TbxAddress.Text}");
            Orders order = new Orders
            {
                FullName = TbxFullName.Text,
                Email = TbxEmail.Text,
                Address = TbxAddress.Text,
                TotalPrice = Carts.products.Sum(p=>p.Price)
            };

            Core.DB.Orders.Add(order);
            Core.DB.SaveChanges();

            Carts.products.Clear();
            Application.Current.Shutdown();

        }

        private void UpdateSum()
        {
            int total = Carts.products.Sum(p => p.Price);
            TxtTotalSum.Text = $"Итого к оплате: {total} ₽";
        }

    }
}
