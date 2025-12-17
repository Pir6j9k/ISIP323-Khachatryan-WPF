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
    /// Логика взаимодействия для Model.xaml
    /// </summary>
    public partial class Model : Page
    {
        public Model()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Window.GetWindow(this);
            Info.MW = mW;
            Info.MW.Add();
            Frame MF = Info.MW.MainFrame;
            Price.price();
            if (MF.CanGoForward)
            {
                MF.GoForward();
                return;
            }
            NavigationService.Navigate(new Color());
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Info.Show();
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Info.ModelComboBox = (ModelComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

        }

        private void EngineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Info.EngineComboBox = (EngineComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

        }
    }
}
