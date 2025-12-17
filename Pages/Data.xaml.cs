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
    /// Логика взаимодействия для Data.xaml
    /// </summary>
    public partial class Data : Page
    {
        public Data()
        {
            InitializeComponent();
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsDigit(ch))
                {
                    e.Handled = true;
                    return;
                }
            }
        }


        private void FinalBtn_Click(object sender, RoutedEventArgs e)
        {
            Info.Show();
            Application.Current.Shutdown();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Info.all_complited())
            {
                if (Name.Text.Length > 0 && Phone.Text.Length > 0 && Email.Text.Length > 0)
                {
                    Info.Email = Email.Text;
                    Info.Phone = Phone.Text;
                    Info.FIO = Name.Text;
                    FinalBtn.IsEnabled = true;
                }
                else
                {
                    FinalBtn.IsEnabled = false;
                }
            }
        }
    }
}
