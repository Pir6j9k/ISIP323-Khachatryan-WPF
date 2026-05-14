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

            DpDeliveryDate.DisplayDateStart = DateTime.Today;
            DpDeliveryDate.DisplayDateEnd = DateTime.Today.AddDays(7);
            DpDeliveryDate.SelectedDate = DateTime.Today; 
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (DpDeliveryDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату доставки!");
                return;
            }

            try
            {
                Order newOrder = new Order
                {
                    ClientId = Core.AuthUser.Id, 
                    OrderDate = DateTime.Now,        
                    DeliveryDate = DpDeliveryDate.SelectedDate.Value,
                    PaymentMethod = (CmbPayment.SelectedItem as ComboBoxItem).Content.ToString(),
                    Status = "Новый"
                };

                Core.DB.Orders.Add(newOrder);
                Core.DB.SaveChanges();

                var groupedProducts = Core.SelectedProducts.GroupBy(p => p.Id);

                foreach (var group in groupedProducts)
                {
                    OrderItem op = new OrderItem
                    {
                        OrderId = newOrder.Id,
                        ProductId = group.Key,
                        Quantity = group.Count()
                    };
                    Core.DB.OrderItems.Add(op);
                }

                Core.DB.SaveChanges();

                MessageBox.Show("Заказ успешно оформлен!");
                Core.SelectedProducts.Clear(); 
                NavigationService.GoBack();    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в БД: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
