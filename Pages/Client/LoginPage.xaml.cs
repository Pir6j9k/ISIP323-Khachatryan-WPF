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

namespace ISIP323_Khachatryan_WPF.Pages.Client
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TbxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            string digits = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (digits.StartsWith("7"))
                digits = digits.Substring(1);
            else if (digits.StartsWith("8"))
                digits = digits.Substring(1);

            if (digits.Length > 10)
                digits = digits.Substring(0, 10);

            string formatted = "+7 ";

            if (digits.Length > 0)
            {
                formatted += "(";

                if (digits.Length >= 3)
                    formatted += digits.Substring(0, 3) + ")";
                else
                    formatted += digits;
            }

            if (digits.Length > 3)
            {
                formatted += " ";

                if (digits.Length >= 6)
                    formatted += digits.Substring(3, 3);
                else
                    formatted += digits.Substring(3);
            }

            if (digits.Length > 6)
            {
                formatted += "-" + digits.Substring(6, Math.Min(2, digits.Length - 6));
            }

            if (digits.Length > 8)
            {
                formatted += "-" + digits.Substring(8, Math.Min(2, digits.Length - 8));
            }

            tb.TextChanged -= TbxPhone_TextChanged;
            tb.Text = formatted;
            tb.SelectionStart = tb.Text.Length;
            tb.TextChanged += TbxPhone_TextChanged;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string phoneRaw = new string(TbxPhone.Text.Where(char.IsDigit).ToArray());

            string pass = PBPassword.Password.Trim();

            if (string.IsNullOrEmpty(phoneRaw) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            var user = Core.DB.Users
                .Include("Role")
                .AsEnumerable()
                .FirstOrDefault(u => new string(u.Phone.Where(char.IsDigit).ToArray()) == phoneRaw && u.Password == pass);

            if (user != null)
            {
                Core.AuthUser = user;
                (Application.Current.MainWindow as MainWindow)?.CheckRolePermissions();

                MessageBox.Show($"Добро пожаловать, {user.FullName}!");

                switch (user.Role.Name)
                {
                    case "Администратор":
                        NavigationService.Navigate(new Admin.AdminPage());
                        break;
                    case "Менеджер":
                        NavigationService.Navigate(new Manager.ManagerPage());
                        break;
                    case "Мастер":
                        NavigationService.Navigate(new Master.MasterPage());
                        break;
                    case "Клиент":
                        NavigationService.Navigate(new Salon.MainPage());
                        break;
                    default:
                        NavigationService.Navigate(new Salon.MainPage());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный номер телефона или пароль!");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
