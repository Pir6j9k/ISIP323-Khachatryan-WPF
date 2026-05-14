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

namespace ISIP323_Khachatryan_WPF.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private User _currentUser = new User();
        public EditUserPage(User selectedUser)
        {
            InitializeComponent();

            CBRole.ItemsSource = Core.DB.Roles.ToList();

            if (selectedUser != null)
            {
                _currentUser = selectedUser;
                TxtTitle.Text = "Редактирование";
            }
            else
            {
                TxtTitle.Text = "Новый пользователь";
            }

            DataContext = _currentUser;
            TbxName.Text = _currentUser.FullName;
            TbxLogin.Text = _currentUser.Phone;
            TbxPassword.Text = _currentUser.Password;
            CBRole.SelectedItem = _currentUser.Role;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TbxPassword.Text) && _currentUser.Id == 0)
                {
                    MessageBox.Show("Пароль не может быть пустым для нового пользователя!");
                    return;
                }

                _currentUser.FullName = TbxName.Text;
                _currentUser.Phone = TbxLogin.Text;

                if (!string.IsNullOrWhiteSpace(TbxPassword.Text))
                {
                    _currentUser.Password = TbxPassword.Text;
                }

                _currentUser.Role = CBRole.SelectedItem as Role;

                if (_currentUser.Id == 0)
                    Core.DB.Users.Add(_currentUser);

                Core.DB.SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}
