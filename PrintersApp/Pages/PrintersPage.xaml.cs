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
        public BindingList<PrinterInRoom> Printers { get; set; } = new BindingList<PrinterInRoom>();
        public PrintersPage(ContextDataBase ctx)
        {
            InitializeComponent();
            this.ctx = ctx;
            Printers = new BindingList<PrinterInRoom>(ctx.PrinterInRooms.ToList());
            DataGridPrinters.ItemsSource = Printers;
            ComboBoxLocation.ItemsSource = Enum.GetValues(typeof(VarLocation)).Cast<VarLocation>();
            var Cartridges = ctx.Cartridges.Select(c => new CartridgeWithBool { cartridge = c, isSelected = false }).ToList();
            ComboBoxCompabilityCartridges.ItemsSource = Cartridges;
            var test = ctx.Printers.ToList();
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

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            
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
            Printers.Remove(deletePrinter);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "Поиск")
            {
                DataGridPrinters.ItemsSource = Printers;
                return;
            }
            string paramSearch = ((TextBox)sender).Text.ToLower();
            var searchPrinters = Printers;
            DataGridPrinters.ItemsSource = searchPrinters.Where(p => p.PrinterObject.Id.ToString() == paramSearch ||  
                                                                     p.Room.ToString() == paramSearch || 
                                                                     p.PrinterObject.InventoryNumber.ToString().ToLower().Contains(paramSearch)|| 
                                                                     p.PrinterObject.Name.ToLower().Contains(paramSearch) || 
                                                                     p.PrinterObject.Location.ToString().ToLower().Contains(paramSearch)).ToList();
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text == "" ||  TextBoxInventoryNumber.Text == "" || TextBoxRoom.Text == "" || ComboBoxLocation.Text == "Расположение")
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
                    updatePrinter.InventoryNumber = Convert.ToInt64(TextBoxInventoryNumber.Text);
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
                    Printers.ResetItem(Printers.IndexOf(pickPrinter));
                    DataGridPrinters.ItemsSource = Printers;
                    GridAddEditElement.Visibility = Visibility.Hidden;
                    return;
                }
                MessageBox.Show("error update object");
                return;
            }
            var newPrinter = new Printer
            {
                Name = TextBoxName.Text,
                InventoryNumber = Convert.ToInt64(TextBoxInventoryNumber.Text),
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
            Printers.Add(newPrinterInRoom);
            GridAddEditElement.Visibility = Visibility.Hidden;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DataGridPrinters.ItemsSource = Printers = new BindingList<PrinterInRoom>(ctx.PrinterInRooms.ToList());
                TextBoxSearch.Text = null;
                MessageBox.Show("Обновлено");
            }
        }
    }
}
