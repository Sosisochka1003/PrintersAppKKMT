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
using System.Windows.Shapes;
using static PrintersApp.ContextDataBase;

namespace PrintersApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для CartridgeShipment.xaml
    /// </summary>
    public partial class CartridgeShipment : Window
    {
        ContextDataBase ctx;
        public CartridgeShipment(ContextDataBase globalctx)
        {
            InitializeComponent();
            ctx = globalctx;
            ComboBoxCartridge.ItemsSource = ctx.Cartridges.ToList();
        }

        private void TextBoxRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ComboBoxPrinter.IsEnabled = false;
                return;
            }
            ComboBoxPrinter.IsEnabled = true;
            //var Printers = ctx.PrinterInRooms.Where(p => p.Room == ((TextBox)sender).Text).Join(ctx.Printers,
            //                pr => pr.PrinterId,
            //                p => p.Id,
            //                (pr,p) => new Printer
            //                {
            //                    Id = p.Id,
            //                    Name = p.Name,
            //                    CartridgeId = p.CartridgeId,
            //                    CartridgeObject = p.CartridgeObject,
            //                }).ToList();
            //ComboBoxPrinter.ItemsSource = Printers;
            //ComboBoxPrinter.SelectedItem = Printers.FirstOrDefault();
        }

        private void ComboBoxPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxCartridge.Items.Clear();
            //ComboBoxCartridge.Items.Add(((Printer)ComboBoxPrinter.SelectedItem).CartridgeObject);
            //ComboBoxCartridge.SelectedItem = ((Printer)ComboBoxPrinter.SelectedItem).CartridgeObject;
        }

        private void CheckBoxAutoSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            ComboBoxCartridge.IsEnabled = true;
        }

        private void CheckBoxAutoSelect_Checked(object sender, RoutedEventArgs e)
        {
            if (ComboBoxCartridge == null)
            {
                return;
            }
            //ComboBoxCartridge.SelectedItem = ((Printer)ComboBoxPrinter.SelectedItem).CartridgeObject;
            //ComboBoxCartridge.IsEnabled = false;
        }

        private void ButtonShipment_Click(object sender, RoutedEventArgs e)
        {
            //хз почему оно не воспринимает объект из комбобокса 84 и 87 строчки
            //var printer = (Printer)ComboBoxPrinter.SelectedItem;
            //var cartridge = (Cartridge)ComboBoxCartridge.SelectedItem;

            //ctx.Shipments.Add(new Shipment
            //{
            //    PrinterId = ((Printer)ComboBoxPrinter.SelectedItem).Id,
            //    PrinterObject = ctx.Printers.FirstOrDefault(p => p.Id == ((Printer)ComboBoxPrinter.SelectedItem).Id),
            //    Room = TextBoxRoom.Text,
            //    CartridgeId = ((Cartridge)ComboBoxCartridge.SelectedItem).Id,
            //    CartridgeObject = ctx.Cartridges.FirstOrDefault(p => p.Id == ((Cartridge)ComboBoxCartridge.SelectedItem).Id),
            //    ShipmentDate = DateTime.UtcNow
            //});
            ctx.Cartridges.FirstOrDefault(p => p.Id == ((Cartridge)ComboBoxCartridge.SelectedItem).Id).StockCount -= 1;
            ctx.SaveChanges();
            this.Close();
        }
    }
}
