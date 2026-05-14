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
    /// Логика взаимодействия для ReschedulePage.xaml
    /// </summary>
    public partial class ReschedulePage : Page
    {
        private Appointment _currentAppointment;
        private User _selectedClient;

        // Редактирование существующей записи
        public ReschedulePage(Appointment appointment)
        {
            InitializeComponent();
            LoadComboBoxes();
            _currentAppointment = appointment;
            TblClientName.Text = appointment.User.FullName;
            CmbService.SelectedValue = appointment.ServiceId;
            CmbMaster.SelectedValue = appointment.MasterId;
            DpNewDate.SelectedDate = appointment.AppointmentDateTime.Date;
            string currentTime = appointment.AppointmentDateTime.ToString("HH:mm");
            foreach (ComboBoxItem item in CmbTime.Items)
            {
                if (item.Content.ToString() == currentTime)
                {
                    CmbTime.SelectedItem = item;
                    break;
                }
            }
        }

        public ReschedulePage()
        {
            InitializeComponent();
            LoadComboBoxes();
            _currentAppointment = null;
            TblClientName.Text = "Выберите клиента";
            PanelSearchClient.Visibility = Visibility.Visible;
        }

        private void LoadComboBoxes()
        {
            CmbService.ItemsSource = Core.DB.Services.ToList();
            CmbMaster.ItemsSource = Core.DB.Users
                .Where(u => u.RoleId == 2 || u.RoleId == 3).ToList();
            CmbClient.ItemsSource = Core.DB.Users
                .Where(u => u.RoleId == 4).ToList();
        }

        private void CmbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbClient.SelectedItem is User selected)
            {
                _selectedClient = selected;
                TblClientName.Text = selected.FullName;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DpNewDate.SelectedDate == null || CmbTime.SelectedItem == null ||
                CmbService.SelectedItem == null || CmbMaster.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            string timeStr = (CmbTime.SelectedItem as ComboBoxItem).Content.ToString();
            DateTime dateTime = DpNewDate.SelectedDate.Value.Add(TimeSpan.Parse(timeStr));

            try
            {
                if (_currentAppointment == null)
                {
                    if (_selectedClient == null)
                    {
                        MessageBox.Show("Выберите клиента!");
                        return;
                    }
                    var newApp = new Appointment
                    {
                        ClientId = _selectedClient.Id,
                        ServiceId = (int)CmbService.SelectedValue,
                        MasterId = (int)CmbMaster.SelectedValue,
                        AppointmentDateTime = dateTime,
                        Status = "Новая",
                        PaymentMethod = (CmbPayment.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Наличные"
                    };
                    Core.DB.Appointments.Add(newApp);
                    MessageBox.Show("Запись успешно создана!");
                }
                else
                {
                    _currentAppointment.ServiceId = (int)CmbService.SelectedValue;
                    _currentAppointment.MasterId = (int)CmbMaster.SelectedValue;
                    _currentAppointment.AppointmentDateTime = dateTime;
                    _currentAppointment.Status = "Редактирована";
                    MessageBox.Show("Запись успешно обновлена!");
                }

                Core.DB.SaveChanges();
                NavigationService.GoBack();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errors = string.Join("\n", ex.EntityValidationErrors
                    .SelectMany(ce => ce.ValidationErrors)
                    .Select(ce => ce.PropertyName + ": " + ce.ErrorMessage));
                MessageBox.Show("Ошибка валидации:\n" + errors);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void CmbService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbService.SelectedItem is Service selectedService)
            {
                var masters = Core.DB.Users
                    .Where(u => (u.RoleId == 2 || u.RoleId == 3)
                             && u.Services.Any(s => s.Id == selectedService.Id))
                    .ToList();

                CmbMaster.ItemsSource = masters;
                CmbMaster.SelectedIndex = -1;
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

    }
}

