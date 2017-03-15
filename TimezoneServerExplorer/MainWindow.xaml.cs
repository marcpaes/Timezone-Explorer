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

namespace TimezoneServerExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<TimeZoneInfo> _zones;

        public MainWindow()
        {
            InitializeComponent();
            LoadTimeZones();

            txtTimezone.TextChanged += TextChanged;
            txtOffset.TextChanged += TextChanged;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            search();
        }

        private void LoadTimeZones()
        {
            _zones = TimeZoneInfo.GetSystemTimeZones().OrderBy(x => x.BaseUtcOffset.ToString());
            ListZones.ItemsSource = _zones;    
        }

        private void search()
        {
            var filteredZones = _zones;

            if (!String.IsNullOrWhiteSpace(txtOffset.Text))
            {
                filteredZones = _zones.Where(x => x.BaseUtcOffset.ToString().Contains(txtOffset.Text));
            }

            if (!String.IsNullOrWhiteSpace(txtTimezone.Text))
            {
                filteredZones = _zones.Where(x => x.StandardName.ToUpper().Contains(txtTimezone.Text.ToUpper())
                || x.Id.ToUpper().Contains(txtTimezone.Text.ToUpper()));
            }

            ListZones.ItemsSource = filteredZones;
        }
    }
}
