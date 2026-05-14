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

namespace ISIP323_Khachatryan_WPF.Pages.Master
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class EditMasterServicesPage : Page
    {
        public class ServiceViewModel
        {
            public int ServiceId { get; set; }
            public string ServiceName { get; set; }
            public bool IsSelected { get; set; }
        }


        public EditMasterServicesPage()
        {
            InitializeComponent();
            LoadServices();
        }
        private void LoadServices()
        {
            var allServices = Core.DB.Services.ToList();

            var currentMaster = Core.DB.Users.FirstOrDefault(u => u.Id == Core.AuthUser.Id);

            if (currentMaster != null)
            {
                var masterServiceIds = currentMaster.Services.Select(s => s.Id).ToList();

                var viewModel = allServices.Select(s => new ServiceViewModel
                {
                    ServiceId = s.Id,
                    ServiceName = s.Name,
                    IsSelected = masterServiceIds.Contains(s.Id)
                }).ToList();

                LBoxAllServices.ItemsSource = viewModel;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LBoxAllServices.ItemsSource as List<ServiceViewModel>;

            var master = Core.DB.Users.FirstOrDefault(u => u.Id == Core.AuthUser.Id);

            if (master != null)
            {
                master.Services.Clear();

                foreach (var item in selectedItems.Where(i => i.IsSelected))
                {
                    var serviceToAdd = Core.DB.Services.FirstOrDefault(s => s.Id == item.ServiceId);
                    if (serviceToAdd != null)
                    {
                        master.Services.Add(serviceToAdd);
                    }
                }

                try
                {
                    Core.DB.SaveChanges();
                    MessageBox.Show("Список услуг обновлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}
