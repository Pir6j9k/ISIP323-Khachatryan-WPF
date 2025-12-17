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
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                TitleTextBlock.Text = page.Title;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
            Reduce();
        }
        public void Add()
        {
            Progress.Value += 1;
        }
        public void Reduce()
        {
            Progress.Value -= 1;
        }
        private void MainFrame_NavigationStopped(object sender, NavigationEventArgs e)
        {
            Add();
        }

    }
}
