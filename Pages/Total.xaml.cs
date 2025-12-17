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
    /// Логика взаимодействия для Total.xaml
    /// </summary>
    public partial class Total : Page
    {
        public List<Totals> totals { get; set; }
        public class Totals
        {
            public string Model { get; set; }
            public string Engine { get; set; }
            public string Color { get; set; }
            public string Options { get; set; }
            public string Price { get; set; }
        }

        public Total()
        {
            InitializeComponent();
            totals = new List<Totals>
            {
                new Totals
                {
                    Model = Info.ModelComboBox,
                    Engine = Info.EngineComboBox,
                    Color = Info.ColorComboBox,
                    Options = Info.options,
                    Price = Price.TotalPrice.ToString()
                }
            };
            TotalListBox.ItemsSource = totals;
        }


        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Credit());
        }

    }
}
