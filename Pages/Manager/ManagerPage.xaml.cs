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

namespace ISIP323_Khachatryan_WPF.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RefreshData();
            }
        }

        private void RefreshData()
        {
            var db = Core.DB;
            DGridProducts.ItemsSource = db.Products.ToList();
            DGridAppointments.ItemsSource = db.Appointments.ToList();
            DGridOrders.ItemsSource = db.Orders.ToList();
            DGridServiceTypes.ItemsSource = db.ServiceTypes.ToList();
            DGridProductTypes.ItemsSource = db.ProductType.ToList();
            DGridManufacturers.ItemsSource = db.Manufacturers.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EditProductPage(null));

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Product selected)
                NavigationService.Navigate(new EditProductPage(selected));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Product product)
            {
                if (MessageBox.Show($"Удалить {product.Name}?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool hasOrders = Core.DB.OrderItems.Any(oi => oi.ProductId == product.Id);
                        if (hasOrders)
                        {
                            product.IsFrozen = true;
                            MessageBox.Show("Товар есть в заказах — он заморожен вместо удаления.");
                        }
                        else
                        {
                            Core.DB.Products.Remove(product);
                        }
                        Core.DB.SaveChanges();
                        RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении: " + ex.Message);
                    }
                }
            }
        }

        private void TbxSearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = TbxSearchClient.Text.ToLower();
            DGridAppointments.ItemsSource = Core.DB.Appointments
                .Where(a => a.User.FullName.ToLower().Contains(search) || a.User.Phone.Contains(search))
                .ToList();
        }

        private void BtnReschedule_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Appointment selectedApp)
            {
                NavigationService.Navigate(new ReschedulePage(selectedApp));
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Appointment selectedApp)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите отменить запись клиента {selectedApp.User.FullName}?",
                                             "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedApp.Status = "Отменена";
                        Core.DB.SaveChanges();

                        DGridAppointments.ItemsSource = Core.DB.Appointments.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при отмене: " + ex.Message);
                    }
                }
            }
        }

        private void BtnDeliverOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Order order)
            {
                order.Status = "Выдан";
                Core.DB.SaveChanges();
                RefreshData();
            }
        }

      
        private void BtnAddServiceType_Click(object sender, RoutedEventArgs e)
        {
            string newServiceTypeName = "Новый тип " + DateTime.Now.ToString("HH:mm:ss");

            if (Core.DB.ServiceTypes.Any(pt => pt.Name == newServiceTypeName)) return;

            var newEntry = new ServiceType { Name = newServiceTypeName };
            Core.DB.ServiceTypes.Add(newEntry);

            try
            {
                Core.DB.SaveChanges();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Manager.ReschedulePage());
        }

        private void DGridServiceTypes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    try
                    {
                        Core.DB.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Такое название уже существует!");
                        RefreshData(); 
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void BtnDeleteType_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is ServiceType type)
            {
                try
                {
                    Core.DB.ServiceTypes.Remove(type);
                    Core.DB.SaveChanges();
                    RefreshData();
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить тип, используемый в товарах!");
                }
            }
        }

        private void BtnAddProductType_Click(object sender, RoutedEventArgs e)
        {
            string newProductTypeName = "Новый тип " + DateTime.Now.ToString("HH:mm:ss");

            if (Core.DB.ProductType.Any(pt => pt.Name == newProductTypeName)) return;

            var newEntry = new ProductType { Name = newProductTypeName };
            Core.DB.ProductType.Add(newEntry);

            try
            {
                Core.DB.SaveChanges();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void DGridProductTypes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    try
                    {
                        Core.DB.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Такое название уже существует!");
                        RefreshData();
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }
        private void BtnDeleteProductType_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is ProductType type)
            {
                try
                {
                    Core.DB.ProductType.Remove(type);
                    Core.DB.SaveChanges();
                    RefreshData();
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить тип, используемый в товарах!");
                }
            }
        }
        private void BtnAddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            string newManufacturer = "Новый тип " + DateTime.Now.ToString("HH:mm:ss");

            if (Core.DB.Manufacturers.Any(pt => pt.Name == newManufacturer)) return;

            var newEntry = new Manufacturer { Name = newManufacturer };
            Core.DB.Manufacturers.Add(newEntry);

            try
            {
                Core.DB.SaveChanges();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void DGridManufacturers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    try
                    {
                        Core.DB.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Такое название уже существует!");
                        RefreshData();
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void BtnDeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Manufacturer type)
            {
                try
                {
                    Core.DB.Manufacturers.Remove(type);
                    Core.DB.SaveChanges();
                    RefreshData();
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить тип, используемый в товарах!");
                }
            }
        }
    }
}
