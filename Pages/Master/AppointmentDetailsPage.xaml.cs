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
    /// Логика взаимодействия для AppointmentDetailsPage.xaml
    /// </summary>
    public partial class AppointmentDetailsPage : Page
    {
        private Appointment _currentAppointment;
        public AppointmentDetailsPage(Appointment selectedAppointment)
        {
            InitializeComponent();
            this.DataContext = selectedAppointment;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Данные о записи не найдены");
                return;
            }
            _currentAppointment = selectedAppointment;
            FillDetails();
        }

        private void FillDetails()
        {
            TxtDateTime.Text = _currentAppointment.AppointmentDateTime.ToString("dd MMMM yyyy HH:mm");

            if (_currentAppointment.User != null)
            {
                TxtClient.Text = _currentAppointment.User.FullName;
            }
            else
            {
                TxtClient.Text = "ID Клиента: " + _currentAppointment.ClientId;
            }

            if (_currentAppointment.Service != null)
            {
                TxtService.Text = _currentAppointment.Service.Name;
            }
            else
            {
                TxtService.Text = "ID Услуги: " + _currentAppointment.ServiceId;
            }

            TxtStatus.Text = _currentAppointment.Status ?? "Запланировано";
            TxtComment.Text = _currentAppointment.Comment ?? "Нет комментария";
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentAppointment.Status = "Выполнена";

                Core.DB.SaveChanges();

                MessageBox.Show("Запись успешно завершена!");

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
