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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            if (Core.AuthUser == null) return;

            var masterId = Core.AuthUser.Id;
            DGridSchedule.ItemsSource = Core.DB.Appointments
                .Where(a => a.MasterId == masterId)
                .ToList();
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var rowData = button.DataContext;

            if (rowData is Appointment selectedAppointment)
            {
                NavigationService.Navigate(new AppointmentDetailsPage(selectedAppointment));
            }
            else
            {
                string typeName = rowData != null ? rowData.GetType().Name : "ПУСТО (NULL)";
                MessageBox.Show($"Данные не передались!\nМы ждали Appointments, а таблица выдала: {typeName}");
            }
        }

        private void BtnEditServices_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Master.EditMasterServicesPage());
        }
    }
}
