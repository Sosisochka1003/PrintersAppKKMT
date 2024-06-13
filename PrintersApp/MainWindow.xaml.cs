using Microsoft.EntityFrameworkCore;
using PrintersApp.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ContextDataBase ctx;
        public MainWindow()
        {
            InitializeComponent();
            //ctx = new ContextDataBase();
            //ctx.Database.Migrate();
        }

        private void ButtonPrintes_Click(object sender, RoutedEventArgs e)
        {
            //var PrintersWindow = new Printers(ctx);
            //PrintersWindow.Show();
            //var newMenu = new NewMainWindow();
            //newMenu.Show();
        }

        private void ButtonCartridges_Click(object sender, RoutedEventArgs e)
        {
            //var CartridgesWindow = new Cartridges(ctx);
            //CartridgesWindow.Show();
            //var cartridge = new StartWindow();
            //cartridge.Show();
        }

        private void ButtonComming_Click(object sender, RoutedEventArgs e)
        {
            //var CartridgeCommingsWindow = new CartridgeComming(ctx);
            //CartridgeCommingsWindow.Show();
        }

        private void ButtonShipment_Click(object sender, RoutedEventArgs e)
        {
            //var CartridgeShipmentWindow = new CartridgeShipment(ctx);
            //CartridgeShipmentWindow.Show();
        }

        private void ShipmentReport_Click(object sender, RoutedEventArgs e)
        {
            //ExcelReports.ShipmentReport(ctx);
        }

        private void CartridgeCountReport_Click(object sender, RoutedEventArgs e)
        {
            //ExcelReports.CartridgeCount(ctx);
        }

        private void CartridgeCommignReport_Click(object sender, RoutedEventArgs e)
        {
            //ExcelReports.CartridgeComming(ctx);
        }
    }
}