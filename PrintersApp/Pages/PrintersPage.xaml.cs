using PrintersApp.Windows;
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
            public required Cartridge cartridge { get; set; }
            public bool isSelected { get; set; }
        }
        ContextDataBase ctx;
        PrinterInRoom? pickPrinter;
        public List<PrinterInRoom> Printers { get; set; } = new List<PrinterInRoom>();
        public VarLocation currentLocation;
        public PrintersPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            Printers = new List<PrinterInRoom>(ctx.PrinterInRooms.ToList());
            DataGridPrinters.ItemsSource = Printers;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            ComboBoxFilterLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            var Cartridges = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = false }).ToList();
            ComboBoxCompabilityCartridges.ItemsSource = Cartridges;
        }

        private void ButtonAddElement_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = null;
            pickPrinter = null;
            TextBoxInventoryNumber.Text = null;
            TextBoxRoom.Text = null;
            ComboBoxLocation.Text = "Расположение";
            ComboBoxCompabilityCartridges.Text = "Совместимые картриджи";
            var Cartridges = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = false }).ToList();
            ComboBoxCompabilityCartridges.ItemsSource = Cartridges;
            if (GridAddEditElement.Visibility == Visibility.Hidden)
            {
                GridAddEditElement.Visibility = Visibility.Visible;
            }
            else if (GridAddEditElement.Visibility == Visibility.Visible)
            {
                GridAddEditElement.Visibility = Visibility.Hidden;
            }
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            GridAddEditElement.Visibility = Visibility.Visible;
            pickPrinter = (PrinterInRoom)DataGridPrinters.SelectedItem;
            TextBoxName.Text = pickPrinter.PrinterObject.Name;
            TextBoxInventoryNumber.Text = pickPrinter.PrinterObject.InventoryNumber.ToString();
            TextBoxRoom.Text = pickPrinter.Room;
            ComboBoxLocation.SelectedItem = pickPrinter.PrinterObject.Location;
            ComboBoxCompabilityCartridges.ItemsSource = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = ctx.PrinterCartridges.Any(x => x.Cartridge == c && x.Printer == pickPrinter.PrinterObject) }).ToList();
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
            DataGridPrinters.ItemsSource = Printers = new List<PrinterInRoom>(ctx.PrinterInRooms.ToList());
            searchPrinters();
        }

        private void searchPrinters()
        {
            string param = TextBoxSearch.Text.ToLower();
            if (param == "" || param == "поиск")
            {
                DataGridPrinters.ItemsSource = currentLocation == VarLocation.Общий ? Printers: Printers.Where(c => c.PrinterObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
                return;
            }
            DataGridPrinters.ItemsSource = currentLocation == VarLocation.Общий ?
                                                    Printers.Where(c => c.PrinterObject.Id.ToString() == param ||
                                                                    c.PrinterObject.Name.ToLower().Contains(param))
                                                        :
                                                    Printers.Where(c => c.PrinterObject.Id.ToString().Contains(param) &&
                                                                    c.PrinterObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.PrinterObject.Name.ToString().ToLower().Contains(param) &&
                                                                    c.PrinterObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.Room.ToString().ToLower().Contains(param) &&
                                                                    c.PrinterObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()) ||
                                                                    c.PrinterObject.InventoryNumber.ToString().ToLower().Contains(param) &&
                                                                    c.PrinterObject.Location.ToString().ToLower().Contains(currentLocation.ToString().ToLower()));
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchPrinters();
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = false;
            foreach (CartridgeWithBool item in ComboBoxCompabilityCartridges.Items)
            {
                if (item.isSelected == true)
                {
                    isChecked = true;
                }
            }

            if (TextBoxName.Text == "" ||  TextBoxInventoryNumber.Text == "" || TextBoxRoom.Text == "" || ComboBoxLocation.Text == "Расположение" || !isChecked)
            {
                MessageBox.Show("Неверное заполнение данных");
                return;
            }
            if (pickPrinter != null)
            {
                var updateRoom = ctx.PrinterInRooms.FirstOrDefault(p => p == pickPrinter);
                var updatePrinter = ctx.Printers.FirstOrDefault(p => p == pickPrinter.PrinterObject);
                ctx.PrinterCartridges.RemoveRange(ctx.PrinterCartridges.Where(p => p.Printer == pickPrinter.PrinterObject).ToList());
                if (updatePrinter != null && updateRoom != null)
                {
                    updatePrinter.Name = TextBoxName.Text;
                    updatePrinter.InventoryNumber = TextBoxInventoryNumber.Text;
                    updatePrinter.Location = (VarLocation)ComboBoxLocation.SelectedItem;
                    foreach (CartridgeWithBool item in ComboBoxCompabilityCartridges.Items)
                    {
                        if (item.isSelected == true)
                        {
                            ctx.PrinterCartridges.Add(new PrinterCartridge
                            {
                                PrinterId = updatePrinter.Id,
                                Printer = updatePrinter,
                                CartridgeId = item.cartridge.Id,
                                Cartridge = item.cartridge,
                            });
                        }
                    }
                    await ctx.SaveChangesAsync();
                    DataGridPrinters.ItemsSource = Printers = new List<PrinterInRoom>(ctx.PrinterInRooms.ToList());
                    searchPrinters();
                    GridAddEditElement.Visibility = Visibility.Hidden;
                    return;
                }
                MessageBox.Show("error update object");
                return;
            }
            var newPrinter = new Printer
            {
                Name = TextBoxName.Text,
                InventoryNumber = TextBoxInventoryNumber.Text,
                Location = (VarLocation)ComboBoxLocation.SelectedItem,
            };
            var newPrinterInRoom = new PrinterInRoom
            {
                Room = TextBoxRoom.Text,
                PrinterId = newPrinter.Id,
                PrinterObject = newPrinter,
            };
            ctx.Printers.Add(newPrinter);
            ctx.PrinterInRooms.Add(newPrinterInRoom);
            await ctx.SaveChangesAsync();

            var tempPrinter = ctx.Entry(newPrinter).Entity;

            foreach (CartridgeWithBool item in ComboBoxCompabilityCartridges.Items)
            {
                if (item.isSelected == true)
                {
                    ctx.PrinterCartridges.Add(new PrinterCartridge
                    {
                        PrinterId = newPrinter.Id,
                        Printer = newPrinter,
                        CartridgeId = item.cartridge.Id,
                        Cartridge = item.cartridge,
                    });
                }
            }
            await ctx.SaveChangesAsync();
            DataGridPrinters.ItemsSource = Printers = new List<PrinterInRoom>(ctx.PrinterInRooms.ToList());
            searchPrinters();
            GridAddEditElement.Visibility = Visibility.Hidden;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DataGridPrinters.ItemsSource = Printers = new List<PrinterInRoom>(ctx.PrinterInRooms.ToList());
                searchPrinters();
                MessageBox.Show("Обновлено");
            }
        }

        private void ComboBoxFilterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxFilterLocation.SelectedValue.ToString())
            {
                case "Первый":
                    currentLocation = VarLocation.Первый;
                    searchPrinters();
                    break;
                case "Второй":
                    currentLocation = VarLocation.Второй;
                    searchPrinters();
                    break;
                case "ККМТ":
                    currentLocation = VarLocation.ККМТ;
                    searchPrinters();
                    break;
                case "ТТД":
                    currentLocation = VarLocation.ТТД;
                    searchPrinters();
                    break;
                case "Общий":
                    currentLocation = VarLocation.Общий;
                    searchPrinters();
                    break;
                default:
                    currentLocation = VarLocation.Первый;
                    searchPrinters();
                    break;
            }
        }
    }
}
