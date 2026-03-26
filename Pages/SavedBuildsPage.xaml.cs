using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Data.Entity;

namespace ISIP323_Khachatryan_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для SavedBuildsPage.xaml
    /// </summary>
    public partial class SavedBuildsPage : Page
    {
        public SavedBuildsPage()
        {
            InitializeComponent();
            LoadSaveBuilds();
        }

        private void LoadSaveBuilds()
        {
            using (var db = new PCEntities())
            {
                LvSaved.ItemsSource = db.Set<assembly>().ToList();
            }
        }

        private void SaveBuilds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvSaved.SelectedItem is assembly selected)
            {
                GbParts.Header = $"Сборка автора: {selected.author}";

                using (var db = new PCEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                    var links = db.Set<partassembly>()
                                  .Where(pa => pa.assemblyid == selected.id)
                                  .Include(pa => pa.basepart)
                                  .ToList();

                    var finalParts = new List<basepart>();

                    foreach (var link in links)
                    {
                        var part = link.basepart;

                        db.Entry(part).Reference(p => p.cpu).Load();
                        db.Entry(part).Reference(p => p.gpu).Load();
                        db.Entry(part).Reference(p => p.motherboard).Load();
                        db.Entry(part).Reference(p => p.ram).Load();
                        db.Entry(part).Reference(p => p.powersupply).Load();
                        db.Entry(part).Reference(p => p.storagedevice).Load();
                        db.Entry(part).Reference(p => p.processorcooler).Load();

                        finalParts.Add(part);
                    }

                    LvParts.ItemsSource = finalParts;
                }
            }
        }
    }
}

