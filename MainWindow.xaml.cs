using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ISIP323_Khachatryan_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.Salon.MainPage());
        }

        private void Navigating(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string command)
            {
                switch (command)
                {
                    case "Main":
                        MainFrame.Navigate(new Pages.Salon.MainPage());
                        break;
                    case "Shop":
                        MainFrame.Navigate(new Pages.Shop.ProductsPage());
                        break;
                    case "Cart":
                        MainFrame.Navigate(new Pages.Shop.CartPage());
                        break;
                    case "Account":
                        MainFrame.Navigate(new Pages.Client.AccountPage());
                        break;
                    case "Master":
                        MainFrame.Navigate(new Pages.Master.MasterPage());
                        break;
                    case "Manager":
                        MainFrame.Navigate(new Pages.Manager.ManagerPage());
                        break;
                    case "Admin":
                        MainFrame.Navigate(new Pages.Admin.AdminPage());
                        break;
                }
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            BtnBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;

            if (MainFrame.Content is Page page)
            {
                TxtPageTitle.Text = page.Title;
            }
        }

        public void CheckRolePermissions()
        {
            BtnMyRecords.Visibility = Visibility.Collapsed;
            BtnMasterRecords.Visibility = Visibility.Collapsed;
            BtnManagerPanel.Visibility = Visibility.Collapsed;
            BtnAdminPanel.Visibility = Visibility.Collapsed;

            if (Core.AuthUser == null)
            {
                TxtCurrentUser.Text = "Вы вошли как: Гость";
                BtnLogin.Content = "Войти в аккаунт";
                BtnLogin.Visibility = Visibility.Visible;
                BtnCart.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtCurrentUser.Text = $"Пользователь: {Core.AuthUser.FullName}";
                TxtUserRole.Text = $"Ваши права доступа: {Core.AuthUser.Role.Name}";
                BtnLogin.Content = "Выйти из аккаунта";
                BtnLogin.Visibility = Visibility.Visible;
                BtnCart.Visibility = Visibility.Visible;
 
                string role = Core.AuthUser.Role.Name;
                switch (role)
                {
                    case "Клиент":
                        BtnMyRecords.Visibility = Visibility.Visible;
                        break;
                    case "Мастер":
                        BtnMasterRecords.Visibility = Visibility.Visible;
                        break;
                    case "Менеджер":
                        BtnManagerPanel.Visibility = Visibility.Visible;
                        break;
                    case "Администратор":
                        BtnAdminPanel.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (Core.AuthUser == null)
            {
                MainFrame.Navigate(new Pages.Client.LoginPage());
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Core.AuthUser = null;
                    CheckRolePermissions();
                    TxtUserRole.Text = string.Empty;

                    while (MainFrame.CanGoBack) { MainFrame.RemoveBackEntry(); }

                    MainFrame.Navigate(new Pages.Salon.MainPage());
                }
            }
        }
        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:Help/help.chm");
        }
    }
}
