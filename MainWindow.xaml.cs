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

namespace ISIP323_Khachatryan_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Films
        {
            public string Name { get; set; }
            public double Rating { get; set; }
            public int Data { get; set; }
            public string AgeRating { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            List<Films> films = new List<Films>
            {
                new Films
                {
                    Name = "ffff",
                    Rating = 4.2,
                    AgeRating = "13+",
                }
            };
            FilmsListBox.ItemsSource = films;
        }
    }
}
