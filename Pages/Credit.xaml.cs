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

namespace ISIP323_Khachatryan_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Credit.xaml
    /// </summary>
    public partial class Credit : Page
    {
        public Credit()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Data());
            Info.MW.Add();

        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Info.Show();
        }

        private void CreditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(InitialPercentBox.Text) > 100)
            {
                MessageBox.Show("Введен некорректный процент", "Ошибка!");
                return;
            }
            if (int.Parse(MonthsBox.Text)>96 || int.Parse(MonthsBox.Text) < 12)
            {
                MessageBox.Show("Введен некорректный срок", "Ошибка!");
                return;
            }
            Price.n = int.Parse(MonthsBox.Text);
            Price.procent = int.Parse(InitialPercentBox.Text);
            Price.credit(ResultText);
        }

        private void InitialPercentBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
}
