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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            LoadShop();
        }
        private void LoadShop()
        {

            if (!Core.DB.Products.Any())
            {
                List<Products> products = new List<Products>
                {
                    new Products {Title = "Книжный вор", Price = 111, ImagePath = "/Images/img.jpg"},
                    new Products {Title = "В конце они оба умрут", Price = 222, ImagePath = "/Images/img1.jpg"}
                };

                Core.DB.Products.AddRange(products);
                Core.DB.SaveChanges();

            }
            ProductList.ItemsSource = Core.DB.Products.ToList();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products selected = button.DataContext as Products;

            if (selected != null)
            {
                Carts.products.Add(selected);
            }
            else { MessageBox.Show("Հահահա"); return; }

            MessageBox.Show($"'{selected.Title}' добавлен в корзину!");
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.CartPage());
        }
    }
}