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
    /// Логика взаимодействия для Color.xaml
    /// </summary>
    public partial class Color : Page
    {
        public Color()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            Price.PriceList = new List<CheckBox> { LetherCheckBox, HeatedSeatsCheckBox, SecuritySystemCheckBox, MultiMediaCheckBox, CameraCheckBox };

            Info.MW.Add();
            Frame MF = Info.MW.MainFrame;
            Info.options = Price.options();
            if (MF.CanGoForward)
            {
                MF.GoForward();
                return;
            }
            NavigationService.Navigate(new Total());
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Info.Show();
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Info.ColorComboBox = (ColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

        }
    }
}
