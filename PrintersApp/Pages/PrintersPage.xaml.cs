using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static PrintersApp.ContextDataBase;

namespace PrintersApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintersPage.xaml
    /// </summary>
    public partial class PrintersPage : Page
    {
        public class CartridgeWithBool
        {
            public Cartridge cartridge { get; set; }
            public bool isSelected { get; set; }
        }
        ContextDataBase ctx;
        PrinterInRoom pickPrinter;
        public BindingList<PrinterInRoom> Printers { get; set; } = new BindingList<PrinterInRoom>();
        public PrintersPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            Printers = new BindingList<PrinterInRoom>(ctx.PrinterInRooms.ToList());
            DataGridPrinters.ItemsSource = Printers;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            var Cartridges = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = false }).ToList();
            ComboBoxCompabilityPrinters.ItemsSource = Cartridges;
            var test = ctx.Printers.ToList();
        }

        private void ButtonAddElement_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = null;
            TextBoxInventoryNumber.Text = null;
            ComboBoxLocation.Text = "Расположение";
            ComboBoxCompabilityPrinters.Text = "Совместимые картриджи";
            var Cartridges = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = false }).ToList();
            ComboBoxCompabilityPrinters.ItemsSource = Cartridges;
            if (GridAddEditElement.Visibility == Visibility.Hidden)
            {
                GridAddEditElement.Visibility = Visibility.Visible;
            }
            else if (GridAddEditElement.Visibility == Visibility.Visible)
            {
                GridAddEditElement.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            pickPrinter = (PrinterInRoom)DataGridPrinters.SelectedItem;
            TextBoxName.Text = pickPrinter.PrinterObject.Name;
            TextBoxInventoryNumber.Text = pickPrinter.PrinterObject.InventoryNumber.ToString();
            ComboBoxLocation.SelectedItem = pickPrinter.PrinterObject.Location;
            ComboBoxCompabilityPrinters.ItemsSource = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = ctx.PrinterCartridges.Any(x => x.Cartridge == c) }).ToList();
        }

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            PrinterInRoom deletePrinter = (PrinterInRoom)DataGridPrinters.SelectedItem;
            if (deletePrinter == null)
            {
                return;
            }
            ctx.PrinterCartridges.RemoveRange(ctx.PrinterCartridges.Where(x => x.PrinterId == deletePrinter.PrinterId));
            ctx.PrinterInRooms.Remove(deletePrinter);
            ctx.Printers.Remove(ctx.Printers.FirstOrDefault(x => x.Id == deletePrinter.PrinterId));
            await ctx.SaveChangesAsync();
            Printers.Remove(deletePrinter);
        }
    }
}
