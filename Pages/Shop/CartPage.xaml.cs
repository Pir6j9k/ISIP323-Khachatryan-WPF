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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public class CartItemModel
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice => Product.Price * Quantity;
        }
        public CartPage()
        {
            InitializeComponent();
            RefreshCart();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCart();
        }
        private void RefreshCart()
        {
            var groupedCart = Core.SelectedProducts
                .GroupBy(p => p.Id)
                .Select(g => new CartItemModel
                {
                    Product = g.First(),
                    Quantity = g.Count()
                }).ToList();

            LBCart.ItemsSource = groupedCart;

            decimal total = Core.SelectedProducts.Sum(p => p.Price);
            decimal discount = Core.SelectedProducts.Sum(p => p.Price * (p.DiscountPercentage / 100m));

            TxtTotalCount.Text = $"{Core.SelectedProducts.Count} шт.";
            TxtTotalDiscount.Text = $"{discount:N0} ₽";
            TxtFinalPrice.Text = $"{total - discount:N0} ₽";
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Product prod)
            {
                Core.SelectedProducts.Add(prod);
                RefreshCart();
            }
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Product prod)
            {
                var itemToRemove = Core.SelectedProducts.FirstOrDefault(p => p.Id == prod.Id);
                if (itemToRemove != null)
                {
                    Core.SelectedProducts.Remove(itemToRemove);
                }
                RefreshCart();
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Product prod)
            {
                Core.SelectedProducts.RemoveAll(p => p.Id == prod.Id);
                RefreshCart();
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Core.SelectedProducts.Count == 0) return;

            NavigationService.Navigate(new OrderPage());
        }
    }
}
